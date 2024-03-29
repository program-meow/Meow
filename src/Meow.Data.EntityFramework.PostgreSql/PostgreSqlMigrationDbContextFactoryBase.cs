namespace Meow.Data.EntityFrameworkCore;

/// <summary>
/// PostgreSql迁移设计时数据上下文工厂
/// </summary>
public abstract class PostgreSqlMigrationDbContextFactoryBase<TUnitOfWork> : MigrationDbContextFactoryBase<TUnitOfWork> where TUnitOfWork : DbContext, IUnitOfWork {
    /// <summary>
    /// 创建DbContext
    /// </summary>
    public override TUnitOfWork CreateDbContext( string connString ) {
        IServiceCollection services = Ioc.GetServices();
        services.AddDbContext<TUnitOfWork>( optionsAction => optionsAction.UseNpgsql( connString ) );
        return Ioc.Create<TUnitOfWork>();
    }
}