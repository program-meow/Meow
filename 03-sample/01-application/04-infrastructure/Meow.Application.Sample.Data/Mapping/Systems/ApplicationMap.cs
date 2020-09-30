using Meow.Application.Data.Ef;
using Meow.Application.Data.Ef.Core.Map;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Meow.Application.Sample.Data.Mapping.Systems
{
    /// <summary>
    /// 应用程序映射配置
    /// </summary>
    public class ApplicationMap : AggregateRootMap<Domain.Model.Application>
    {
        /// <summary>
        /// 映射表
        /// </summary>
        protected override void MapTable(EntityTypeMapTableBuilder<Domain.Model.Application> builder)
        {
            builder.ToTable("Systems", "Application");
        }

        /// <summary>
        /// 映射属性
        /// </summary>
        protected override void MapProperties(EntityTypeBuilder<Domain.Model.Application> builder)
        {
            //应用程序编号
            builder.Property(t => t.Id)
                .HasColumnName("ApplicationId");
            builder.HasQueryFilter(t => t.IsDeleted == false);
        }
    }
}
