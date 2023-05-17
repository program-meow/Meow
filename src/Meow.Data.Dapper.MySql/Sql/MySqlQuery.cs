﻿using System;
using Meow.Data.Sql;

namespace Meow.Data.Dapper.MySql.Sql;

/// <summary>
/// MySql Sql查询对象
/// </summary>
public class MySqlQuery : MySqlQueryBase
{
    /// <summary>
    /// 初始化MySql Sql查询对象
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="option">Sql配置</param>
    /// <param name="database">数据库信息,用于接入其它数据源,比如EF DbContext</param>
    public MySqlQuery(IServiceProvider serviceProvider, SqlOption<MySqlQuery> option, IDatabase database = null)
        : base(serviceProvider, option, database)
    {
    }
}