using System;
using Meow.Data.Sql;

namespace Meow.Data.Dapper.PostgreSql.Sql;

/// <summary>
/// PostgreSql Sql执行器
/// </summary>
public class PostgreSqlExecutor : PostgreSqlExecutorBase
{
    /// <summary>
    /// 初始化PostgreSql Sql执行器
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="option">Sql配置</param>
    /// <param name="database">数据库信息,用于接入其它数据源,比如EF DbContext</param>
    public PostgreSqlExecutor(IServiceProvider serviceProvider, SqlOption<PostgreSqlExecutor> option, IDatabase database = null)
        : base(serviceProvider, option, database)
    {
    }
}