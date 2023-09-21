namespace Meow.Data.EntityFrameworkCore;

/// <summary>
/// MySql迁移设计时数据上下文工厂
/// </summary>
public abstract class MySqlMigrationDbContextFactoryBase<TUnitOfWork> : MigrationDbContextFactoryBase<TUnitOfWork> where TUnitOfWork : DbContext, IUnitOfWork {
    /// <summary>
    /// 创建DbContext
    /// </summary>
    public override TUnitOfWork CreateDbContext( string connString ) {
        IServiceCollection services = Ioc.GetServices();
        services.AddDbContext<TUnitOfWork>( optionsAction => optionsAction.UseMySql( connString , ServerVersion.AutoDetect( connString ) ) );
        return Ioc.Create<TUnitOfWork>();
    }
}