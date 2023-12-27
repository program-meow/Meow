﻿namespace Meow.Data.Dapper.Sql;

/// <summary>
/// Oracle Sql执行器
/// </summary>
public abstract class OracleSqlExecutorBase : SqlExecutorBase {
    /// <summary>
    /// 初始化Oracle Sql执行器
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    /// <param name="options">Sql配置</param>
    /// <param name="database">数据库信息,用于接入其它数据源,比如EF DbContext</param>
    protected OracleSqlExecutorBase( IServiceProvider serviceProvider , SqlOptions options , IDatabase database ) : base( serviceProvider , options , database ) {
    }

    /// <summary>
    /// 创建Sql生成器
    /// </summary>
    protected override ISqlBuilder CreateSqlBuilder() {
        return new OracleSqlBuilder();
    }

    /// <summary>
    /// 创建判断是否存在Sql生成器
    /// </summary>
    protected override IExistsSqlBuilder CreatExistsSqlBuilder( ISqlBuilder sqlBuilder ) {
        return new OracleExistsSqlBuilder( sqlBuilder );
    }

    /// <summary>
    /// 创建数据库工厂
    /// </summary>
    protected override IDatabaseFactory CreateDatabaseFactory() {
        return new OracleDatabaseFactory();
    }
}