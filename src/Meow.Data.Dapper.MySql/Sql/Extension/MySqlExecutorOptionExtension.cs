using Meow.Config;
using Meow.Data.Sql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Meow.Data.Dapper.MySql.Sql.Extension;

/// <summary>
/// MySql Sql执行器配置扩展
/// </summary>
public class MySqlExecutorOptionExtension<TService, TImplementation> : OptionExtensionBase
    where TService : ISqlExecutor
    where TImplementation : MySqlExecutorBase, TService
{
    /// <summary>
    /// Sql配置
    /// </summary>
    private readonly SqlOption _option;

    /// <summary>
    /// 初始化MySql Sql执行器配置扩展
    /// </summary>
    /// <param name="option">Sql配置</param>
    public MySqlExecutorOptionExtension(SqlOption option)
    {
        _option = option;
    }

    /// <inheritdoc />
    public override void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        services.TryAddTransient(typeof(TService), typeof(TImplementation));
        services.TryAddSingleton(typeof(SqlOption<TImplementation>), (sp) => _option);
    }
}