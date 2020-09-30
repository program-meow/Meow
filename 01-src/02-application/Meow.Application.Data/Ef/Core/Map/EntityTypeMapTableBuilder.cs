using Meow.Parameter.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Meow.Application.Data.Ef.Core.Map
{
    /// <summary>
    /// 实体类型映射表生成器
    /// </summary>
    public class EntityTypeMapTableBuilder<TEntity> where TEntity : class
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        private Database DatabaseType { get; }
        /// <summary>
        /// 实体类型生成器
        /// </summary>
        private EntityTypeBuilder<TEntity> Builder { get; }

        /// <summary>
        /// 初始化实体类型映射表生成器
        /// </summary>
        /// <param name="databaseType">数据库类型</param>
        /// <param name="builder">实体类型生成器</param>
        public EntityTypeMapTableBuilder(Database databaseType, EntityTypeBuilder<TEntity> builder)
        {
            DatabaseType = databaseType;
            Builder = builder;
        }

        /// <summary>
        /// 配置实体映射表
        /// </summary>
        /// <param name="schema">命名空间</param>
        /// <param name="name">名称</param>
        public void ToTable(string schema, string name)
        {
            switch (DatabaseType)
            {
                case Database.SqlServer:
                case Database.PgSql:
                case Database.Oracle:
                    Builder.ToTable(name, schema);
                    break;
                case Database.MySql:
                    Builder.ToTable( $"{schema}.{name}" );
                    break;
            }
        }
    }
}