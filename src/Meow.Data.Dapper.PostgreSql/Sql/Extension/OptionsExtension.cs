using System;
using Meow.Data.Sql;
using Meow.Data.Sql.Extension;
using MeowOption = Meow.Config.Option;

namespace Meow.Data.Dapper.PostgreSql.Sql.Extension;

/// <summary>
/// PostgreSql数据库操作扩展
/// </summary>
public static class OptionsExtension
{

    #region UsePgSqlQuery(配置PostgreSql Sql查询对象)

    /// <summary>
    /// 配置PostgreSql Sql查询对象
    /// </summary>
    /// <param name="option">配置项</param>
    public static MeowOption UsePgSqlQuery(this MeowOption option)
    {
        return option.UsePgSqlQuery("");
    }

    /// <summary>
    /// 配置PostgreSql Sql查询对象
    /// </summary>
    /// <param name="option">配置项</param>
    /// <param name="connection">数据库连接字符串</param>
    public static MeowOption UsePgSqlQuery(this MeowOption option, string connection)
    {
        return option.UsePgSqlQuery<ISqlQuery, PostgreSqlQuery>(connection);
    }

    /// <summary>
    /// 配置PostgreSql Sql查询对象
    /// </summary>
    /// <param name="option">配置项</param>
    /// <param name="setupAction">配置操作</param>
    public static MeowOption UsePgSqlQuery(this MeowOption option, Action<SqlOption> setupAction)
    {
        return option.UsePgSqlQuery<ISqlQuery, PostgreSqlQuery>(setupAction);
    }

    /// <summary>
    /// 配置PostgreSql Sql查询对象
    /// </summary>
    /// <param name="option">配置项</param>
    /// <param name="connection">数据库连接字符串</param>
    public static MeowOption UsePgSqlQuery<TService, TImplementation>(this MeowOption option, string connection)
        where TService : ISqlQuery
        where TImplementation : PostgreSqlQueryBase, TService
    {
        return option.UsePgSqlQuery<TService, TImplementation>(t => t.ConnectionString(connection));
    }

    /// <summary>
    /// 配置PostgreSql Sql查询对象
    /// </summary>
    /// <param name="option">配置项</param>
    /// <param name="setupAction">配置操作</param>
    public static MeowOption UsePgSqlQuery<TService, TImplementation>(this MeowOption option, Action<SqlOption> setupAction)
        where TService : ISqlQuery
        where TImplementation : PostgreSqlQueryBase, TService
    {
        var sqlOption = new SqlOption<TImplementation>();
        setupAction?.Invoke(sqlOption);
        option.AddExtension(new PostgreSqlQueryOptionExtension<TService, TImplementation>(sqlOption));
        return option;
    }

    #endregion

    #region UsePgSqlExecutor(配置PostgreSql Sql执行器)

    /// <summary>
    /// 配置PostgreSql Sql执行器
    /// </summary>
    /// <param name="option">配置项</param>
    public static MeowOption UsePgSqlExecutor(this MeowOption option)
    {
        return option.UsePgSqlExecutor("");
    }

    /// <summary>
    /// 配置PostgreSql Sql执行器
    /// </summary>
    /// <param name="option">配置项</param>
    /// <param name="connection">数据库连接字符串</param>
    public static MeowOption UsePgSqlExecutor(this MeowOption option, string connection)
    {
        return option.UsePgSqlExecutor<ISqlExecutor, PostgreSqlExecutor>(connection);
    }

    /// <summary>
    /// 配置PostgreSql Sql执行器
    /// </summary>
    /// <param name="option">配置项</param>
    /// <param name="setupAction">配置操作</param>
    public static MeowOption UsePgSqlExecutor(this MeowOption option, Action<SqlOption> setupAction)
    {
        return option.UsePgSqlExecutor<ISqlExecutor, PostgreSqlExecutor>(setupAction);
    }

    /// <summary>
    /// 配置PostgreSql Sql执行器
    /// </summary>
    /// <param name="option">配置项</param>
    /// <param name="connection">数据库连接字符串</param>
    public static MeowOption UsePgSqlExecutor<TService, TImplementation>(this MeowOption option, string connection)
        where TService : ISqlExecutor
        where TImplementation : PostgreSqlExecutorBase, TService
    {
        return option.UsePgSqlExecutor<TService, TImplementation>(t => t.ConnectionString(connection));
    }

    /// <summary>
    /// 配置PostgreSql Sql执行器
    /// </summary>
    /// <param name="option">配置项</param>
    /// <param name="setupAction">配置操作</param>
    public static MeowOption UsePgSqlExecutor<TService, TImplementation>(this MeowOption option, Action<SqlOption> setupAction)
        where TService : ISqlExecutor
        where TImplementation : PostgreSqlExecutorBase, TService
    {
        SqlOption<TImplementation> sqlOption = new SqlOption<TImplementation>();
        setupAction?.Invoke(sqlOption);
        option.AddExtension(new PostgreSqlExecutorOptionExtension<TService, TImplementation>(sqlOption));
        return option;
    }

    #endregion
}