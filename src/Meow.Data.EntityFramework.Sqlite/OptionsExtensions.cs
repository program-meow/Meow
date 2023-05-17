﻿using System;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MeowOption = Meow.Config.Option;

namespace Meow.Data.EntityFramework.Sqlite;

/// <summary>
/// Sqlite工作单元操作扩展
/// </summary>
public static class OptionsExtensions
{
    /// <summary>
    /// 配置Sqlite工作单元
    /// </summary>
    /// <param name="options">配置项</param>
    /// <param name="connection">数据库连接字符串</param>
    /// <param name="setupAction">工作单元配置操作</param>
    /// <param name="sqliteSetupAction">Sqlite配置操作</param>
    /// <param name="condition">条件,设置为false,跳过配置</param>
    public static MeowOption UseSqliteUnitOfWork<TService, TImplementation>(this MeowOption options, string connection, Action<DbContextOptionsBuilder> setupAction = null,
        Action<SqliteDbContextOptionsBuilder> sqliteSetupAction = null, bool? condition = null)
        where TService : class, IUnitOfWork
        where TImplementation : UnitOfWorkBase, TService
    {
        return options.UseSqliteUnitOfWork<TService, TImplementation>(connection, null, setupAction, sqliteSetupAction, condition);
    }

    /// <summary>
    /// 配置Sqlite工作单元
    /// </summary>
    /// <param name="options">配置项</param>
    /// <param name="connection">数据库连接</param>
    /// <param name="setupAction">工作单元配置操作</param>
    /// <param name="sqliteSetupAction">Sqlite配置操作</param>
    /// <param name="condition">条件,设置为false,跳过配置</param>
    public static MeowOption UseSqliteUnitOfWork<TService, TImplementation>(this MeowOption options, DbConnection connection, Action<DbContextOptionsBuilder> setupAction = null,
        Action<SqliteDbContextOptionsBuilder> sqliteSetupAction = null, bool? condition = null)
        where TService : class, IUnitOfWork
        where TImplementation : UnitOfWorkBase, TService
    {
        return options.UseSqliteUnitOfWork<TService, TImplementation>(null, connection, setupAction, sqliteSetupAction, condition);
    }

    /// <summary>
    /// 配置Sqlite工作单元
    /// </summary>
    private static MeowOption UseSqliteUnitOfWork<TService, TImplementation>(this MeowOption options, string connectionString, DbConnection connection, Action<DbContextOptionsBuilder> setupAction,
        Action<SqliteDbContextOptionsBuilder> sqliteSetupAction, bool? condition)
        where TService : class, IUnitOfWork
        where TImplementation : UnitOfWorkBase, TService
    {
        options.AddExtension(new SqliteOptionsExtension<TService, TImplementation>(connectionString, connection, setupAction, sqliteSetupAction, condition));
        return options;
    }
}