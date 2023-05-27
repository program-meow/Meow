using Meow.Sample.Data.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Meow.Sample.Data.SqlServer.EntityTypeConfigurations
{
    /// <summary>
    /// 样本类型配置
    /// </summary>
    public class SampleConfiguration : SampleConfigurationBase
    {
        /// <summary>
        /// 配置表
        /// </summary>
        protected override void ConfigTable(EntityTypeBuilder<Domain.Models.Sample> builder)
        {
            builder.ToTable("Sample", "Samples", t => t.HasComment("样本"));
        }
    }
}