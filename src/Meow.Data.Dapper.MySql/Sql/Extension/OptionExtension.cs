using System;
using Meow.Data.Sql;
using Meow.Data.Sql.Extension;
using MeowOption = Meow.Config.Option;

namespace Meow.Data.Dapper.MySql.Sql.Extension;

/// <summary>
/// MySql数据库操作扩展
/// </summary>
public static class OptionExtension
{
    #region UseMySqlQuery  [配置MySql Sql查询对象]

    /// <summary>
    /// 配置MySql Sql查询对象
    /// </summary>
    /// <param name="option">配置项</param>
    public static MeowOption UseMySqlQuery(this MeowOption option)
    {
        return option.UseMySqlQuery("");
    }

    /// <summary>
    /// 配置MySql Sql查询对象
    /// </summary>
    /// <param name="option">配置项</param>
    /// <param name="connection">数据库连接字符串</param>
    public static MeowOption UseMySqlQuery(this MeowOption option, string connection)
    {
        return option.UseMySqlQuery<ISqlQuery, MySqlQuery>(connection);
    }

    /// <summary>
    /// 配置MySql Sql查询对象
    /// </summary>
    /// <param name="option">配置项</param>
    /// <param name="setupAction">配置操作</param>
    public static MeowOption UseMySqlQuery(this MeowOption option, Action<SqlOption> setupAction)
    {
        return option.UseMySqlQuery<ISqlQuery, MySqlQuery>(setupAction);
    }

    /// <summary>
    /// 配置MySql Sql查询对象
    /// </summary>
    /// <param name="option">配置项</param>
    /// <param name="connection">数据库连接字符串</param>
    public static MeowOption UseMySqlQuery<TService, TImplementation>(this MeowOption option, string connection)
        where TService : ISqlQuery
        where TImplementation : MySqlQueryBase, TService
    {
        return option.UseMySqlQuery<TService, TImplementation>(t => t.ConnectionString(connection));
    }

    /// <summary>
    /// 配置MySql Sql查询对象
    /// </summary>
    /// <param name="option">配置项</param>
    /// <param name="setupAction">配置操作</param>
    public static MeowOption UseMySqlQuery<TService, TImplementation>(this MeowOption option, Action<SqlOption> setupAction)
        where TService : ISqlQuery
        where TImplementation : MySqlQueryBase, TService
    {
        var sqlOption = new SqlOption<TImplementation>();
        setupAction?.Invoke(sqlOption);
        option.AddExtension(new MySqlQueryOptionExtension<TService, TImplementation>(sqlOption));
        return option;
    }

    #endregion

    #region UseMySqlExecutor  [配置MySql Sql执行器]

    /// <summary>
    /// 配置MySql Sql执行器
    /// </summary>
    /// <param name="option">配置项</param>
    public static MeowOption UseMySqlExecutor(this MeowOption option)
    {
        return option.UseMySqlExecutor("");
    }

    /// <summary>
    /// 配置MySql Sql执行器
    /// </summary>
    /// <param name="option">配置项</param>
    /// <param name="connection">数据库连接字符串</param>
    public static MeowOption UseMySqlExecutor(this MeowOption option, string connection)
    {
        return option.UseMySqlExecutor<ISqlExecutor, MySqlExecutor>(connection);
    }

    /// <summary>
    /// 配置MySql Sql执行器
    /// </summary>
    /// <param name="option">配置项</param>
    /// <param name="setupAction">配置操作</param>
    public static MeowOption UseMySqlExecutor(this MeowOption option, Action<SqlOption> setupAction)
    {
        return option.UseMySqlExecutor<ISqlExecutor, MySqlExecutor>(setupAction);
    }

    /// <summary>
    /// 配置MySql Sql执行器
    /// </summary>
    /// <param name="option">配置项</param>
    /// <param name="connection">数据库连接字符串</param>
    public static MeowOption UseMySqlExecutor<TService, TImplementation>(this MeowOption option, string connection)
        where TService : ISqlExecutor
        where TImplementation : MySqlExecutorBase, TService
    {
        return option.UseMySqlExecutor<TService, TImplementation>(t => t.ConnectionString(connection));
    }

    /// <summary>
    /// 配置MySql Sql执行器
    /// </summary>
    /// <param name="option">配置项</param>
    /// <param name="setupAction">配置操作</param>
    public static MeowOption UseMySqlExecutor<TService, TImplementation>(this MeowOption option, Action<SqlOption> setupAction)
        where TService : ISqlExecutor
        where TImplementation : MySqlExecutorBase, TService
    {
        var sqlOption = new SqlOption<TImplementation>();
        setupAction?.Invoke(sqlOption);
        option.AddExtension(new MySqlExecutorOptionExtension<TService, TImplementation>(sqlOption));
        return option;
    }

    #endregion
}