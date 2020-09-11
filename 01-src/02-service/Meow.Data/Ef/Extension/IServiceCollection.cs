using System;
using Meow.Data.Core.Connection;
using Meow.Data.Ef.Core.UnitOfWork;
using Meow.Extension.Helper;
using Meow.Helper;
using Meow.Parameter.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Meow.Data.Ef.Extension
{
    /// <summary>
    /// 服务集合扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 注册工作单元服务
        /// </summary>
        /// <typeparam name="TService">工作单元接口类型</typeparam>
        /// <typeparam name="TImplementation">工作单元实现类型</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="configAction">配置操作</param>
        private static IServiceCollection AddUnitOfWork<TService, TImplementation>(this IServiceCollection services,
            Action<DbContextOptionsBuilder> configAction)
            where TService : class, IUnitOfWork
            where TImplementation : UnitOfWorkBase, TService
        {
            services.AddDbContext<TImplementation>(configAction);
            services.TryAddScoped<TService>(t => t.GetService<TImplementation>());
            services.TryAddScoped<IUnitOfWork>(t => t.GetService<TImplementation>());
            return services;
        }

        /// <summary>
        /// 注册工作单元服务
        /// </summary>
        /// <typeparam name="TService">工作单元接口类型</typeparam>
        /// <typeparam name="TImplementation">工作单元实现类型</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="key">标识</param>
        public static IServiceCollection AddUnitOfWork<TService, TImplementation>(this IServiceCollection services, string key)
            where TService : class, IUnitOfWork
            where TImplementation : UnitOfWorkBase, TService
        {
            return AddUnitOfWork<TService, TImplementation>(services, builder =>
            {
                ConfigConnection(builder, key);
            });
        }

        /// <summary>
        /// 注册工作单元服务
        /// </summary>
        /// <typeparam name="TService">工作单元接口类型</typeparam>
        /// <typeparam name="TImplementation">工作单元实现类型</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="databaseType">数据库类型</param>
        /// <param name="connection">连接字符串</param>
        public static IServiceCollection AddUnitOfWork<TService, TImplementation>(this IServiceCollection services, Database databaseType, string connection)
            where TService : class, IUnitOfWork
            where TImplementation : UnitOfWorkBase, TService
        {
            return AddUnitOfWork<TService, TImplementation>(services, builder =>
            {
                ConfigConnection(builder, databaseType, connection);
            });
        }

        /// <summary>
        /// 配置连接字符串
        /// </summary>
        /// <param name="builder">数据库上下文选项生成器</param>
        /// <param name="key">标识</param>
        private static void ConfigConnection(DbContextOptionsBuilder builder, string key)
        {
            var connectionProvider = Ioc.Create<IConnectionProvider>();
            var connection = connectionProvider.GetConnection(key);
            ConfigConnection(builder, connection.Type.SafeValue(), connection.ToString());
        }

        /// <summary>
        /// 配置连接字符串
        /// </summary>
        /// <param name="builder">数据库上下文选项生成器</param>
        /// <param name="databaseType">数据库类型</param>
        /// <param name="connection">数据库连接字符串</param>
        private static void ConfigConnection(DbContextOptionsBuilder builder, Database databaseType, string connection)
        {
            switch (databaseType)
            {
                case Database.SqlServer:
                    builder.UseSqlServer(connection);
                    return;
                case Database.MySql:
                    builder.UseMySql(connection);
                    return;
                case Database.PgSql:
                    builder.UseNpgsql(connection);
                    return;
                case Database.Oracle:
                    return;
            }
        }
    }
}