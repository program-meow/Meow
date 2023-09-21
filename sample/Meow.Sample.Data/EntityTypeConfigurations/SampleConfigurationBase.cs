using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DomainSample = Meow.Sample.Domain.Models.Sample;

namespace Meow.Sample.Data.EntityTypeConfigurations;

/// <summary>
/// 样本类型配置
/// </summary>
public abstract class SampleConfigurationBase : IEntityTypeConfiguration<DomainSample> {
    /// <summary>
    /// 配置
    /// </summary>
    /// <param name="builder">实体类型生成器</param>
    public void Configure( EntityTypeBuilder<DomainSample> builder ) {
        ConfigTable( builder );
        ConfigId( builder );
        ConfigProperties( builder );
        ConfigIndex( builder );
        ConfigData( builder );
    }

    /// <summary>
    /// 配置表
    /// </summary>
    protected abstract void ConfigTable( EntityTypeBuilder<DomainSample> builder );

    /// <summary>
    /// 配置标识
    /// </summary>
    protected virtual void ConfigId( EntityTypeBuilder<DomainSample> builder ) {
        builder.Property( t => t.Id )
            .HasColumnName( "SampleId" )
            .HasComment( "样本标识" );
    }

    /// <summary>
    /// 配置属性
    /// </summary>
    protected virtual void ConfigProperties( EntityTypeBuilder<DomainSample> builder ) {
        builder.Property( t => t.Name )
            .HasColumnName( "Name" )
            .HasComment( "名称" );
        builder.Property( t => t.IsDeleted )
            .HasColumnName( "IsDeleted" )
            .HasComment( "是否删除" );
        builder.Property( t => t.CreationTime )
            .HasColumnName( "CreationTime" )
            .HasComment( "创建时间" );
        builder.Property( t => t.CreatorId )
            .HasColumnName( "CreatorId" )
            .HasComment( "创建人标识" );
        builder.Property( t => t.LastModificationTime )
            .HasColumnName( "LastModificationTime" )
            .HasComment( "最后修改时间" );
        builder.Property( t => t.LastModifierId )
            .HasColumnName( "LastModifierId" )
            .HasComment( "最后修改人标识" );
        builder.Property( t => t.TenantId )
            .HasColumnName( "TenantId" )
            .HasComment( "租户" );
    }

    /// <summary>
    /// 配置索引
    /// </summary>
    protected virtual void ConfigIndex( EntityTypeBuilder<DomainSample> builder ) {
    }

    /// <summary>
    /// 配置默认数据
    /// </summary>
    protected virtual void ConfigData( EntityTypeBuilder<DomainSample> builder ) {
    }
}
