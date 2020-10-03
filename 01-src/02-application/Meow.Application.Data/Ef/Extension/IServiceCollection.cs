using System;
using Meow.Helper;
using Meow.Exception;
using Meow.Parameter.Enum;
using Meow.Extension.Validation;
using Microsoft.EntityFrameworkCore;
using Meow.Application.Data.Core.Connection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Meow.Application.Data.Ef.Extension
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
            where TImplementation : UnitOfWork, TService
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
            where TImplementation : UnitOfWork, TService
        {
            return AddUnitOfWork<TService, TImplementation>(services, builder =>
            {
                var connectionProvider = Ioc.Create<IConnectionProvider>();
                var connection = connectionProvider.GetConnection(key);
                ConfigConnection(builder, connection);
            });
        }

        /// <summary>
        /// 配置连接字符串
        /// </summary>
        /// <param name="builder">数据库上下文选项生成器</param>
        /// <param name="connection">链接对象</param>
        private static void ConfigConnection(DbContextOptionsBuilder builder, IConnection connection)
        {
            var connectionString = connection.ToConnectionString();
            switch (connection)
            {
                case ConnectionSqlServer _:
                    builder.UseSqlServer(connectionString);
                    break;
                case ConnectionMySql _:
                    builder.UseMySql(connectionString);
                    break;
                case ConnectionPgSql _:
                    builder.UseNpgsql(connectionString);
                    break;
                case ConnectionOracle _:
                    builder.UseOracle(connectionString);
                    break;
                default:
                    throw new Warning("不支持该数据库类型");
            }
        }

        /// <summary>
        /// 注册工作单元服务
        /// </summary>
        /// <typeparam name="TService">工作单元接口类型</typeparam>
        /// <typeparam name="TImplementation">工作单元实现类型</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="databaseType">数据库类型</param>
        /// <param name="connection">链接字符串</param>
        public static IServiceCollection AddUnitOfWork<TService, TImplementation>(this IServiceCollection services, Database databaseType, string connection)
            where TService : class, IUnitOfWork
            where TImplementation : UnitOfWork, TService
        {
            connection.CheckEmpty(nameof(connection));
            return AddUnitOfWork<TService, TImplementation>(services, builder =>
            {
                ConfigConnection(builder, databaseType, connection);
            });
        }

        /// <summary>
        /// 配置连接字符串
        /// </summary>
        /// <param name="builder">数据库上下文选项生成器</param>
        /// <param name="databaseType">数据库类型</param>
        /// <param name="connection">链接字符串</param>
        private static void ConfigConnection(DbContextOptionsBuilder builder, Database databaseType, string connection)
        {
            switch (databaseType)
            {
                case Database.SqlServer:
                    builder.UseSqlServer(connection);
                    break;
                case Database.MySql:
                    builder.UseMySql(connection);
                    break;
                case Database.PgSql:
                    builder.UseNpgsql(connection);
                    break;
                case Database.Oracle:
                    break;
                default:
                    throw new Warning("不支持该数据库类型");
            }
        }
    }
}