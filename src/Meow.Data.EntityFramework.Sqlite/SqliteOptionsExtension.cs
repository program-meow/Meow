﻿using System;
using System.Data.Common;
using Meow.Config;
using Meow.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Meow.Data.EntityFramework.Sqlite;

/// <summary>
/// Sqlite工作单元配置扩展
/// </summary>
public class SqliteOptionsExtension<TService, TImplementation> : OptionExtensionBase
    where TService : class, IUnitOfWork
    where TImplementation : UnitOfWorkBase, TService
{
    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    private readonly string _connectionString;
    /// <summary>
    /// 数据库连接
    /// </summary>
    private readonly DbConnection _connection;
    /// <summary>
    /// 工作单元配置操作
    /// </summary>
    private readonly Action<DbContextOptionsBuilder> _setupAction;
    /// <summary>
    /// Sqlite配置操作
    /// </summary>
    private readonly Action<SqliteDbContextOptionsBuilder> _sqliteSetupAction;
    /// <summary>
    /// 条件
    /// </summary>
    private readonly bool? _condition;

    /// <summary>
    /// 初始化Sqlite工作单元配置扩展
    /// </summary>
    /// <param name="connectionString">数据库连接字符串</param>
    /// <param name="connection">数据库连接</param>
    /// <param name="setupAction">工作单元配置操作</param>
    /// <param name="sqliteSetupAction">Sqlite配置操作</param>
    /// <param name="condition">条件</param>
    public SqliteOptionsExtension(string connectionString, DbConnection connection, Action<DbContextOptionsBuilder> setupAction,
        Action<SqliteDbContextOptionsBuilder> sqliteSetupAction, bool? condition)
    {
        _connectionString = connectionString;
        _connection = connection;
        _setupAction = setupAction;
        _sqliteSetupAction = sqliteSetupAction;
        _condition = condition;
    }

    /// <inheritdoc />
    public override void ConfigureServices(HostBuilderContext context, IServiceCollection services)
    {
        if (_condition == false)
            return;
        services.AddDbContext<TService, TImplementation>(options =>
        {
            _setupAction?.Invoke(options);
            if (_connectionString.IsEmpty() == false)
            {
                options.UseSqlite(_connectionString, _sqliteSetupAction);
                return;
            }
            if (_connection != null)
            {
                options.UseSqlite(_connection, _sqliteSetupAction);
            }
        });
    }
}