using System;
using Meow.Data.Sql;

namespace Meow.Data.Dapper.SqlServer.Sql;

/// <summary>
/// Sql Server Sql查询对象
/// </summary>
public class SqlServerSqlQuery : SqlServerSqlQueryBase
{
    /// <summary>
    /// 初始化Sql Server Sql查询对象
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="option">Sql配置</param>
    /// <param name="database">数据库信息,用于接入其它数据源,比如EF DbContext</param>
    public SqlServerSqlQuery(IServiceProvider serviceProvider, SqlOption<SqlServerSqlQuery> option, IDatabase database = null)
        : base(serviceProvider, option, database)
    {
    }
}