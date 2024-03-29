﻿namespace Meow.Data.EntityFrameworkCore;

/// <summary>
/// Oracle工作单元基类
/// </summary>
public abstract class OracleUnitOfWorkBase : UnitOfWorkBase {
    /// <summary>
    /// 初始化Oracle工作单元
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="options">配置</param>
    protected OracleUnitOfWorkBase( IServiceProvider serviceProvider , DbContextOptions options )
        : base( serviceProvider , options ) {
    }

    /// <inheritdoc />
    protected override string GetConnectionString( IDbContextOptionsExtension dbContextOptionsExtension ) {
        return ( ( OracleOptionsExtension ) dbContextOptionsExtension ).ConnectionString;
    }

    /// <inheritdoc />
    protected override void ConfigTenantConnectionString( DbContextOptionsBuilder optionsBuilder , string connectionString ) {
        optionsBuilder.UseOracle( connectionString );
    }

    /// <summary>
    /// 配置扩展属性
    /// </summary>
    /// <param name="modelBuilder">模型生成器</param>
    /// <param name="entityType">实体类型</param>
    protected override void ApplyExtraProperties( ModelBuilder modelBuilder , IMutableEntityType entityType ) {
        if( typeof( IExtraProperties ).IsAssignableFrom( entityType.ClrType ) == false )
            return;
        modelBuilder.Entity( entityType.ClrType )
            .Property( "ExtraProperties" )
            .HasColumnName( "ExtraProperties" )
            .HasComment( "扩展属性" )
            .HasColumnType( "CLOB" )
            .HasConversion( new ExtraPropertiesValueConverter() )
            .Metadata.SetValueComparer( new ExtraPropertyDictionaryValueComparer() );
    }
}