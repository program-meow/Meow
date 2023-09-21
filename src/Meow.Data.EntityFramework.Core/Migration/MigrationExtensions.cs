namespace Meow.Data.EntityFrameworkCore.Migration;

/// <summary>
/// 迁移扩展
/// </summary>
public static class MigrationExtensions {
    /// <summary>
    /// 应用迁移
    /// </summary>
    public static async Task<WebApplication> MigrationAsync<TUnitOfWork>( this WebApplication app ) where TUnitOfWork : IUnitOfWork {
        app.Logger.LogInformation( "准备迁移数据库" );
        using IServiceScope scope = app.Services.CreateScope();
        TUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<TUnitOfWork>();
        await unitOfWork.MigrateAsync();
        app.Logger.LogInformation( "迁移数据库完成" );
        return app;
    }
}