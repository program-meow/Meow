using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Meow.Application.Data.Core.Transaction;
using Meow.Application.Data.Core.UnitOfWork;
using Meow.Application.Data.Ef.Core.Helper;
using Meow.Application.Data.Ef.Core.Map;
using Meow.Application.Data.Sql;
using Meow.Application.Domain.Core.Initializer;
using Meow.Auth.Extension;
using Meow.Auth.Session;
using Meow.Exception;
using Meow.Extension.Helper;
using Meow.Helper;
using Meow.Parameter.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MicrosoftType = System.Type;
using Guid = System.Guid;

namespace Meow.Application.Data.Ef.Core.UnitOfWork
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public abstract class UnitOfWorkBase : DbContext, IUnitOfWork, IDatabase, IEntityMatedata
    {
        #region 字段

        /// <summary>
        /// 映射字典
        /// </summary>
        private static readonly ConcurrentDictionary<MicrosoftType, IEnumerable<IMap>> _maps;
        /// <summary>
        /// 服务提供器
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        #endregion

        #region 属性

        /// <summary>
        /// 跟踪号
        /// </summary>
        public string TraceId { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public Database DatabaseType { get; set; }
        /// <summary>
        /// 用户会话
        /// </summary>
        public ISession Session { get; set; }

        #endregion

        #region 静态构造方法

        /// <summary>
        /// 初始化Entity Framework工作单元
        /// </summary>
        static UnitOfWorkBase()
        {
            _maps = new ConcurrentDictionary<MicrosoftType, IEnumerable<IMap>>();
        }

        #endregion

        #region 构造方法

        /// <summary>
        /// 初始化Entity Framework工作单元
        /// </summary>
        /// <param name="options">配置</param>
        /// <param name="serviceProvider">服务提供器</param>
        protected UnitOfWorkBase(DbContextOptions options, IServiceProvider serviceProvider)
            : base(options)
        {
            TraceId = Guid.NewGuid().ToString();
            Session = Meow.Auth.Session.Session.Instance;
            DatabaseType = DbContextHelper.GetDatabaseType(options);
            _serviceProvider = serviceProvider ?? Ioc.Create<IServiceProvider>();
            RegisterToManager();
        }

        /// <summary>
        /// 注册到工作单元管理器
        /// </summary>
        private void RegisterToManager()
        {
            var manager = Create<IUnitOfWorkManager>();
            manager?.Register(this);
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        private T Create<T>()
        {
            var result = _serviceProvider.GetService(typeof(T));
            if (result == null)
                return default(T);
            return (T)result;
        }

        #endregion

        #region OnModelCreating(配置映射)

        /// <summary>
        /// 配置映射
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (IMap mapper in GetMaps())
                mapper.Map(DatabaseType, modelBuilder);
        }

        /// <summary>
        /// 获取映射配置列表
        /// </summary>
        private IEnumerable<IMap> GetMaps()
        {
            return _maps.GetOrAdd(GetMapType(), GetMapsFromAssemblies());
        }

        /// <summary>
        /// 获取映射接口类型
        /// </summary>
        protected virtual MicrosoftType GetMapType()
        {
            return this.GetType();
        }

        /// <summary>
        /// 从程序集获取映射配置列表
        /// </summary>
        private IEnumerable<IMap> GetMapsFromAssemblies()
        {
            var result = new List<IMap>();
            foreach (var assembly in GetAssemblies())
                result.AddRange(GetMapInstances(assembly));
            return result;
        }

        /// <summary>
        /// 获取定义映射配置的程序集列表
        /// </summary>
        protected virtual Assembly[] GetAssemblies()
        {
            return new[] { GetType().Assembly };
        }

        /// <summary>
        /// 获取映射实例列表
        /// </summary>
        /// <param name="assembly">程序集</param>
        protected virtual IEnumerable<IMap> GetMapInstances(Assembly assembly)
        {
            return Meow.Helper.Reflection.GetInstancesByInterface<IMap>(assembly);
        }

        #endregion

        #region Commit(提交)

        /// <summary>
        /// 提交,返回影响的行数
        /// </summary>
        public int Commit()
        {
            try
            {
                return SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException(ex);
            }
        }

        /// <summary>
        /// 保存更改
        /// </summary>
        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }

        #endregion

        #region CommitAsync(提交)

        /// <summary>
        /// 提交,返回影响的行数
        /// </summary>
        public async Task<int> CommitAsync()
        {
            try
            {
                return await SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException(ex);
            }
        }

        /// <summary>
        /// 异步保存更改
        /// </summary>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            SaveChangesBefore();
            var transactionActionManager = Create<ITransactionActionManager>();
            if (transactionActionManager.Count == 0)
                return await base.SaveChangesAsync(cancellationToken);
            return await TransactionCommit(transactionActionManager, cancellationToken);
        }

        /// <summary>
        /// 保存更改前操作
        /// </summary>
        protected virtual void SaveChangesBefore()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        InterceptAddedOperation(entry);
                        break;
                    case EntityState.Modified:
                        InterceptModifiedOperation(entry);
                        break;
                    case EntityState.Deleted:
                        InterceptDeletedOperation(entry);
                        break;
                }
            }
        }

        /// <summary>
        /// 拦截添加操作
        /// </summary>
        protected virtual void InterceptAddedOperation(EntityEntry entry)
        {
            InitCreationAudited(entry);
            InitModificationAudited(entry);
        }

        /// <summary>
        /// 初始化创建审计信息
        /// </summary>
        private void InitCreationAudited(EntityEntry entry)
        {
            CreationAuditedInitializer.Init(entry.Entity, GetUserId(), GetUserName());
        }

        /// <summary>
        /// 获取用户标识
        /// </summary>
        protected virtual string GetUserId()
        {
            return GetSession().UserId;
        }

        /// <summary>
        /// 获取用户会话
        /// </summary>
        protected virtual ISession GetSession()
        {
            return Session;
        }

        /// <summary>
        /// 获取用户名称
        /// </summary>
        protected virtual string GetUserName()
        {
            var name = GetSession().GetFullName();
            return string.IsNullOrEmpty(name) ? GetSession().GetUserName() : name;
        }

        /// <summary>
        /// 初始化修改审计信息
        /// </summary>
        private void InitModificationAudited(EntityEntry entry)
        {
            ModificationAuditedInitializer.Init(entry.Entity, GetUserId(), GetUserName());
        }

        /// <summary>
        /// 拦截修改操作
        /// </summary>
        protected virtual void InterceptModifiedOperation(EntityEntry entry)
        {
            InitModificationAudited(entry);
        }

        /// <summary>
        /// 拦截删除操作
        /// </summary>
        protected virtual void InterceptDeletedOperation(EntityEntry entry)
        {
        }

        /// <summary>
        /// 手工创建事务提交
        /// </summary>
        private async Task<int> TransactionCommit(ITransactionActionManager transactionActionManager, CancellationToken cancellationToken)
        {
            using (var connection = Database.GetDbConnection())
            {
                if (connection.State == ConnectionState.Closed)
                    await connection.OpenAsync(cancellationToken);
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        await transactionActionManager.CommitAsync(transaction);
                        Database.UseTransaction(transaction);
                        var result = await base.SaveChangesAsync(cancellationToken);
                        transaction.Commit();
                        return result;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        #endregion

        #region GetConnection(获取数据库连接)

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        public IDbConnection GetConnection()
        {
            return Database.GetDbConnection();
        }

        #endregion

        #region 获取元数据

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <param name="type">实体类型</param>
        public string GetTable(MicrosoftType type)
        {
            if (type == null)
                return null;
            try
            {
                var entityType = Model.FindEntityType(type);
                return entityType?.FindAnnotation("Relational:TableName")?.Value.SafeString();
            }
            catch
            {
                return type.Name;
            }
        }

        /// <summary>
        /// 获取架构
        /// </summary>
        /// <param name="type">实体类型</param>
        public string GetSchema(MicrosoftType type)
        {
            if (type == null)
                return null;
            try
            {
                var entityType = Model.FindEntityType(type);
                return entityType?.FindAnnotation("Relational:Schema")?.Value.SafeString();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取列名
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <param name="property">属性名</param>
        public string GetColumn(MicrosoftType type, string property)
        {
            if (type == null || string.IsNullOrWhiteSpace(property))
                return null;
            try
            {
                var entityType = Model.FindEntityType(type);
                var result = entityType?.GetProperty(property)?.FindAnnotation("Relational:ColumnName")?.Value
                    .SafeString();
                return string.IsNullOrWhiteSpace(result) ? property : result;
            }
            catch
            {
                return property;
            }
        }

        #endregion
    }
}