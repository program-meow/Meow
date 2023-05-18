using Meow.Config;
using Meow.Data.Sql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Meow.Data.Dapper.PostgreSql.Sql;

/// <summary>
/// PostgreSql Sql执行器配置扩展
/// </summary>
public class PostgreSqlExecutorOptionExtension<TService, TImplementation> : OptionsExtensionBase
    where TService : ISqlExecutor
    where TImplementation : PostgreSqlExecutorBase, TService
{
    /// <summary>
    /// Sql配置
    /// </summary>
    private readonly SqlOptions _options;

    /// <summary>
    /// 初始化PostgreSql Sql执行器配置扩展
    /// </summary>
    /// <param name="options">Sql配置</param>
    public PostgreSqlExecutorOptionExtension(SqlOptions options)
    {
        _options = options;
    }

    /// <inheritdoc />
    public override void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.TryAddTransient(typeof(TService), typeof(TImplementation));
        services.TryAddSingleton(typeof(SqlOptions<TImplementation>), (sp) => _options);
    }
}