using Meow.Data.Ef.Mapping;
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
        protected override void MapTable(EntityTypeBuilder<Application> builder)
        {
            builder.ToTable("Application", "Systems");
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
