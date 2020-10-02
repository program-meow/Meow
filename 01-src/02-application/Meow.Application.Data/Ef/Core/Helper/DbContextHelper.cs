using System.Linq;
using Meow.Exception;
using Meow.Parameter.Enum;
using Meow.Extension.Helper;
using Meow.Extension.Validation;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Meow.Application.Data.Ef.Core.Helper
{
    /// <summary>
    /// 数据库上下文操作
    /// </summary>
    internal static class DbContextHelper
    {
        /// <summary>
        /// 获取数据库类型
        /// </summary>
        /// <param name="options">配置项</param>
        public static Database GetDatabaseType(DbContextOptions options)
        {
            if (options.IsNull() || options.Extensions.Count() == 1)
                throw new Warning("数据库上下文配置不能为空");
            foreach (var item in options.Extensions)
            {
                var assemblyName = item.SafeString();
                var dbContextAssemblyName = GetDbContextAssemblyName();
                if (dbContextAssemblyName.ContainsKey(assemblyName))
                    return dbContextAssemblyName[assemblyName];
            }
            throw new Warning("数据库上下文配置无法识别");
        }

        /// <summary>
        /// 获取数据库上下文程序集名称
        /// </summary>
        private static Dictionary<string, Database> GetDbContextAssemblyName()
        {
            return new Dictionary<string, Database>{
               { "Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal.SqlServerOptionsExtension",Database.SqlServer},
               { "Pomelo.EntityFrameworkCore.MySql.Infrastructure.Internal.MySqlOptionsExtension",Database.MySql},
               { "Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal.NpgsqlOptionsExtension",Database.PgSql},
               { "Oracle",Database.Oracle},
             };
        }
    }
}