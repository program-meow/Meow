using System.Linq;
using System.Collections.Generic;
using Meow.Exception;
using Meow.Parameter.Enum;
using Meow.Extension.Helper;
using Meow.Extension.Validation;
using Microsoft.EntityFrameworkCore;

namespace Meow.Data.Ef.Core.Helper
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
                var info = item.SafeString();
                var dbContextNamespace = GetDbContextNamespace();
                if (dbContextNamespace.ContainsKey(info))
                    return dbContextNamespace[info];
            }
            throw new Warning("数据库上下文配置无法识别");
        }

        /// <summary>
        /// 获取数据库上下文命名空间
        /// </summary>
        private static Dictionary<string, Database> GetDbContextNamespace()
        {
            return new Dictionary<string, Database>{
               { "Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal.SqlServerOptionsExtension",Database.SqlServer},
               { "MySql",Database.MySql},
               { "Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal.NpgsqlOptionsExtension",Database.PgSql},
               { "Oracle",Database.Oracle},
             };
        }
    }
}