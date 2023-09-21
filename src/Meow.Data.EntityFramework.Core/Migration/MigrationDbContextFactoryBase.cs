namespace Meow.Data.EntityFrameworkCore.Migration;

/// <summary>
/// 迁移设计时数据上下文工厂
/// </summary>
public abstract class MigrationDbContextFactoryBase<TUnitOfWork> : IDesignTimeDbContextFactory<TUnitOfWork> where TUnitOfWork : DbContext, IUnitOfWork {
    /// <summary>
    /// 创建数据上下文
    /// </summary>
    public virtual TUnitOfWork CreateDbContext( string[] args ) {
        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().AddCommandLine( args );
        IConfigurationRoot configuration = configurationBuilder.Build();

        configuration = configurationBuilder.SetBasePath( Path.Combine( Directory.GetCurrentDirectory() , configuration[ "basePath" ] ) )
            .AddJsonFile( "appsettings.json" )
            .AddJsonFile( $"appsettings.{configuration[ "environment" ]}.json" , true )
            .AddEnvironmentVariables()
            .AddCommandLine( args )
            .Build();

        Console.WriteLine( this.GetConnectionString( configuration ) );

        return CreateDbContext( this.GetConnectionString( configuration ) );
    }

    /// <summary>
    /// 获取连接字符串
    /// </summary>
    public virtual string GetConnectionString( IConfiguration configuration ) {
        return configuration.GetConnectionString( GetConnectionStringName() );
    }

    /// <summary>
    /// 获取连接字符串名称
    /// </summary>
    public abstract string GetConnectionStringName();

    /// <summary>
    /// 创建数据库上下文
    /// </summary>
    public abstract TUnitOfWork CreateDbContext( string connString );
}