using Meow.Data.Sql;
using Meow.Data.Sql.Extension;
using MeowOption = Meow.Config.Option;

namespace Meow.Data.Dapper.SqlServer.Sql.Extension;

/// <summary>
/// Sql Server数据库操作扩展
/// </summary>
public static class OptionExtension
{
    #region UseSqlServerSqlQuery  [配置Sql Server Sql查询对象]

    /// <summary>
    /// 配置Sql Server Sql查询对象
    /// </summary>
    /// <param name="option">配置项</param>
    public static MeowOption UseSqlServerSqlQuery(this MeowOption option)
    {
        return option.UseSqlServerSqlQuery("");
    }

    /// <summary>
    /// 配置Sql Server Sql查询对象
    /// </summary>
    /// <param name="option">配置项</param>
    /// <param name="connection">数据库连接字符串</param>
    public static MeowOption UseSqlServerSqlQuery(this MeowOption option, string connection)
    {
        return option.UseSqlServerSqlQuery<ISqlQuery, SqlServerSqlQuery>(connection);
    }

    /// <summary>
    /// 配置Sql Server Sql查询对象
    /// </summary>
    /// <param name="option">配置项</param>
    /// <param name="setupAction">配置操作</param>
    public static MeowOption UseSqlServerSqlQuery(this MeowOption option, System.Action<SqlOption> setupAction)
    {
        return option.UseSqlServerSqlQuery<ISqlQuery, SqlServerSqlQuery>(setupAction);
    }

    /// <summary>
    /// 配置Sql Server Sql查询对象
    /// </summary>
    /// <param name="option">配置项</param>
    /// <param name="connection">数据库连接字符串</param>
    public static MeowOption UseSqlServerSqlQuery<TService, TImplementation>(this MeowOption option, string connection)
        where TService : ISqlQuery
        where TImplementation : SqlServerSqlQueryBase, TService
    {
        return option.UseSqlServerSqlQuery<TService, TImplementation>(t => t.ConnectionString(connection));
    }

    /// <summary>
    /// 配置Sql Server Sql查询对象
    /// </summary>
    /// <param name="option">配置项</param>
    /// <param name="setupAction">配置操作</param>
    public static MeowOption UseSqlServerSqlQuery<TService, TImplementation>(this MeowOption option, System.Action<SqlOption> setupAction)
        where TService : ISqlQuery
        where TImplementation : SqlServerSqlQueryBase, TService
    {
        var sqlOption = new SqlOption<TImplementation>();
        setupAction?.Invoke(sqlOption);
        option.AddExtension(new SqlServerSqlQueryOptionExtension<TService, TImplementation>(sqlOption));
        return option;
    }

    #endregion

    #region UseSqlServerSqlExecutor  [配置Sql Server Sql执行器]

    /// <summary>
    /// 配置Sql Server Sql执行器
    /// </summary>
    /// <param name="option">配置项</param>
    public static MeowOption UseSqlServerSqlExecutor(this MeowOption option)
    {
        return option.UseSqlServerSqlExecutor("");
    }

    /// <summary>
    /// 配置Sql Server Sql执行器
    /// </summary>
    /// <param name="option">配置项</param>
    /// <param name="connection">数据库连接字符串</param>
    public static MeowOption UseSqlServerSqlExecutor(this MeowOption option, string connection)
    {
        return option.UseSqlServerSqlExecutor<ISqlExecutor, SqlServerSqlExecutor>(connection);
    }

    /// <summary>
    /// 配置Sql Server Sql执行器
    /// </summary>
    /// <param name="option">配置项</param>
    /// <param name="setupAction">配置操作</param>
    public static MeowOption UseSqlServerSqlExecutor(this MeowOption option, System.Action<SqlOption> setupAction)
    {
        return option.UseSqlServerSqlExecutor<ISqlExecutor, SqlServerSqlExecutor>(setupAction);
    }

    /// <summary>
    /// 配置Sql Server Sql执行器
    /// </summary>
    /// <param name="option">配置项</param>
    /// <param name="connection">数据库连接字符串</param>
    public static MeowOption UseSqlServerSqlExecutor<TService, TImplementation>(this MeowOption option, string connection)
        where TService : ISqlExecutor
        where TImplementation : SqlServerSqlExecutorBase, TService
    {
        return option.UseSqlServerSqlExecutor<TService, TImplementation>(t => t.ConnectionString(connection));
    }

    /// <summary>
    /// 配置Sql Server Sql执行器
    /// </summary>
    /// <param name="option">配置项</param>
    /// <param name="setupAction">配置操作</param>
    public static MeowOption UseSqlServerSqlExecutor<TService, TImplementation>(this MeowOption option, System.Action<SqlOption> setupAction)
        where TService : ISqlExecutor
        where TImplementation : SqlServerSqlExecutorBase, TService
    {
        var sqlOption = new SqlOption<TImplementation>();
        setupAction?.Invoke(sqlOption);
        option.AddExtension(new SqlServerSqlExecutorOptionExtension<TService, TImplementation>(sqlOption));
        return option;
    }

    #endregion
}