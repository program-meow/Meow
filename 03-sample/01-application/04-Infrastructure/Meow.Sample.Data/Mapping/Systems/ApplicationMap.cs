using Meow.Data.Ef;
using Meow.Data.Ef.Core.Map;
using Meow.Sample.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Meow.Sample.Data.Mapping.Systems
{
    /// <summary>
    /// 应用程序映射配置
    /// </summary>
    public class ApplicationMap : AggregateRootMap<Application>
    {
        /// <summary>
        /// 映射表
        /// </summary>
        protected override void MapTable(EntityTypeMapTableBuilder<Application> builder)
        {
            builder.ToTable("Systems", "Application");
        }

        /// <summary>
        /// 映射属性
        /// </summary>
        protected override void MapProperties(EntityTypeBuilder<Application> builder)
        {
            //应用程序编号
            builder.Property(t => t.Id)
                .HasColumnName("ApplicationId");
            builder.HasQueryFilter(t => t.IsDeleted == false);
        }
    }
}
