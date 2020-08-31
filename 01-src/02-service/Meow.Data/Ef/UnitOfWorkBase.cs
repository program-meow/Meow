using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Meow.Data.Ef.Interface;
using Meow.Data.UnitOfWork;
using Meow.Helper;
using Microsoft.EntityFrameworkCore;
using Guid = System.Guid;

namespace Meow.Data.Ef
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public abstract class UnitOfWorkBase : DbContext, IUnitOfWork
    {
        #region 字段

        /// <summary>
        /// 映射字典
        /// </summary>
        private static readonly ConcurrentDictionary<Type, IEnumerable<IMap>> Maps;
        /// <summary>
        /// 服务提供器
        /// </summary>
        private IServiceProvider _serviceProvider;

        #endregion

        #region 属性

        /// <summary>
        /// 跟踪号
        /// </summary>
        public string TraceId { get; set; }

        #endregion

        #region 静态构造方法

        /// <summary>
        /// 初始化Entity Framework工作单元
        /// </summary>
        static UnitOfWorkBase()
        {
            Maps = new ConcurrentDictionary<Type, IEnumerable<IMap>>();
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
                mapper.Map(modelBuilder);
        }

        /// <summary>
        /// 获取映射配置列表
        /// </summary>
        private IEnumerable<IMap> GetMaps()
        {
            return Maps.GetOrAdd(GetMapType(), GetMapsFromAssemblies());
        }

        /// <summary>
        /// 获取映射接口类型
        /// </summary>
        protected virtual Type GetMapType()
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
            throw new NotImplementedException();
        }

        #endregion

        #region CommitAsync(提交)

        /// <summary>
        /// 提交,返回影响的行数
        /// </summary>
        public async Task<int> CommitAsync()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
