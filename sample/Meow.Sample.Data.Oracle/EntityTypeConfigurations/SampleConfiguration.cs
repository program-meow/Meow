using Meow.Sample.Data.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DomainSample = Meow.Sample.Domain.Models.Sample;

namespace Meow.Sample.Data.Oracle.EntityTypeConfigurations;

/// <summary>
/// 样本类型配置
/// </summary>
public class SampleConfiguration : SampleConfigurationBase {
    /// <summary>
    /// 配置表
    /// </summary>
    protected override void ConfigTable( EntityTypeBuilder<DomainSample> builder ) {
        builder.ToTable( "Sample" , t => t.HasComment( "样本" ) );
    }
}
