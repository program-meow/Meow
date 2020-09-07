using Meow.Data.Ef.Core;
using Meow.Data.Ef.Core.Base;
using Meow.Domain.Core.Model;
using Meow.Parameter.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Meow.Data.Ef.Mapping
{
    /// <summary>
    /// 聚合根映射配置
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class AggregateRootMap<TEntity> : MapBase<TEntity>, IMap where TEntity : class, IVersion
    {
        /// <summary>
        /// 映射乐观离线锁
        /// </summary>
        /// <param name="databaseType">数据库类型</param>
        /// <param name="builder">模型</param>
        protected override void MapVersion(Database databaseType, EntityTypeBuilder<TEntity> builder)
        {
            switch (databaseType)
            {
                case Database.SqlServer:
                    builder.Property(t => t.Version).IsRowVersion();
                    break;
                case Database.MySql:
                case Database.PgSql:
                case Database.Oracle:
                    builder.Property(t => t.Version).IsConcurrencyToken();
                    break;
            }
        }
    }
}