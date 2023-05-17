﻿using Meow.Config;
using Meow.Data.Sql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Meow.Data.Dapper.MySql.Sql.Extension;

/// <summary>
/// MySql Sql查询对象配置扩展
/// </summary>
public class MySqlQueryOptionExtension<TService, TImplementation> : OptionExtensionBase
    where TService : ISqlQuery
    where TImplementation : MySqlQueryBase, TService
{
    /// <summary>
    /// Sql配置
    /// </summary>
    private readonly SqlOption _option;

    /// <summary>
    /// 初始化MySql Sql查询对象配置扩展
    /// </summary>
    /// <param name="option">Sql配置</param>
    public MySqlQueryOptionExtension(SqlOption option)
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