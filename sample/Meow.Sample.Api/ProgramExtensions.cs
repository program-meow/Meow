namespace Meow.Sample.Api;

/// <summary>
/// 应用配置扩展
/// </summary>
public static class ProgramExtensions {
    /// <summary>
    /// Meow基础功能配置
    /// </summary>
    /// <param name="builder">Web应用生成器</param>
    public static WebApplicationBuilder AddMeow( this WebApplicationBuilder builder ) {
        builder.AsBuild()
            .AddUtc()
            .AddMeow();
        return builder;
    }

    /// <summary>
    /// 配置Identity工作单元
    /// </summary>
    public static WebApplicationBuilder AddIdentityUnitOfWork( this WebApplicationBuilder builder ) {
        var dbType = builder.GetDatabaseType();
        builder.AsBuild()
            //SqlServer 数据库
            .AddSqlServerUnitOfWork<ISampleUnitOfWork , Meow.Sample.Data.SqlServer.SampleUnitOfWork>(
                builder.GetIdentitySqlServerConnectionString() ,
                condition: dbType == DatabaseEnum.SqlServer )
            //MySql 数据库
            .AddMySqlUnitOfWork<ISampleUnitOfWork , Meow.Sample.Data.MySql.SampleUnitOfWork>(
                builder.GetIdentityMySqlConnectionString() ,
                condition: dbType == DatabaseEnum.MySql )
            //PgSql 数据库
            .AddPgSqlUnitOfWork<ISampleUnitOfWork , Meow.Sample.Data.PgSql.SampleUnitOfWork>(
                builder.GetIdentityPgSqlConnectionString() ,
                condition: dbType == DatabaseEnum.PostgreSql )
            //Oracle 数据库
            .AddOracleUnitOfWork<ISampleUnitOfWork , Meow.Sample.Data.Oracle.SampleUnitOfWork>(
                builder.GetIdentityOracleConnectionString() ,
                condition: dbType == DatabaseEnum.Oracle );
        return builder;
    }

    /// <summary>
    /// 获取数据库类型
    /// </summary>
    public static DatabaseEnum GetDatabaseType( this WebApplicationBuilder builder ) {
        try {
            var dbType = builder.Configuration[ "DatabaseType" ];
            return dbType.IsEmpty() ? DatabaseEnum.SqlServer : Meow.Helper.Enum.Parse<DatabaseEnum>( dbType );
        } catch {
            return DatabaseEnum.SqlServer;
        }
    }


    /// <summary>
    /// 获取Identity SqlServer数据库连接字符串
    /// </summary>
    public static string GetIdentitySqlServerConnectionString( this WebApplicationBuilder builder ) {
        return builder.Configuration.GetConnectionString( "SqlServer" );
    }

    /// <summary>
    /// 获取Identity MySql数据库连接字符串
    /// </summary>
    public static string GetIdentityMySqlConnectionString( this WebApplicationBuilder builder ) {
        return builder.Configuration.GetConnectionString( "MySql" );
    }

    /// <summary>
    /// 获取Identity PgSql数据库连接字符串
    /// </summary>
    public static string GetIdentityPgSqlConnectionString( this WebApplicationBuilder builder ) {
        return builder.Configuration.GetConnectionString( "PgSql" );
    }

    /// <summary>
    /// 获取Identity Oracle数据库连接字符串
    /// </summary>
    public static string GetIdentityOracleConnectionString( this WebApplicationBuilder builder ) {
        return builder.Configuration.GetConnectionString( "Oracle" );
    }

    /// <summary>
    /// 数据迁移
    /// </summary>
    public static async Task MigrateAsync( this WebApplication app ) {
        if( app.Environment.IsDevelopment() == false )
            return;
        var enabled = app.Configuration.GetValue<bool>( "Migration:Enabled" );
        if( enabled == false )
            return;
        var migrationName = app.Configuration.GetValue<string>( "Migration:Name" );
        using var scope = app.Services.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<ISampleUnitOfWork>();
        var appliedMigrations = await unitOfWork.GetAppliedMigrationsAsync();
        if( appliedMigrations.Any( t => t.Contains( migrationName ) ) )
            return;
        var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationService>();
        InstallEfTool( migrationService );
        app.Logger.LogInformation( "准备迁移数据..." );
        Migrate( app , migrationService , migrationName , "Meow.Main.Data.SqlServer" );
        //Migrate( app , migrationService , migrationName , "Util.Platform.Data.PgSql" );

        //var policy = scope.ServiceProvider.GetRequiredService<IPolicy>();
        //await policy.Retry().HandleException<System.Exception>().Forever().Wait()
        //    .OnRetry( ( exception , retry ) => {
        //        var message = "迁移数据发生异常：{Message},已重试 {retry} 次.";
        //        app.Logger.LogWarning( exception , message , exception.Message , retry );
        //    } )
        //    .ExecuteAsync( async () => {
        //        await unitOfWork.MigrateAsync();
        //    } );
        await unitOfWork.MigrateAsync();

        app.Logger.LogInformation( "迁移数据成功..." );
    }

    /// <summary>
    /// 安装和更新 dotnet-ef 工具
    /// </summary>
    private static void InstallEfTool( IMigrationService migrationService ) {
        migrationService.InstallEfTool().UpdateEfTool();
    }

    /// <summary>
    /// 迁移
    /// </summary>
    private static void Migrate( WebApplication app , IMigrationService migrationService , string migrationName , string dataProjectName ) {
        try {
            var path = Meow.Helper.Program.JoinPath( Meow.Helper.Program.GetParentDirectory() , dataProjectName );
            migrationService.AddMigration( migrationName , path ).Migrate( path );
        } catch( System.Exception exception ) {
            app.Logger.LogError( exception , "迁移数据发生异常..." );
        }
    }
}
