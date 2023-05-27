using System.ComponentModel;

namespace Meow.Database;

/// <summary>
/// 数据库类型
/// </summary>
public enum DatabaseEnum
{
    /// <summary>
    /// Sql Server数据库
    /// </summary>
    [Description("SqlServer")]
    SqlServer = 1,
    /// <summary>
    /// MySql数据库
    /// </summary>
    [Description("MySql")]
    MySql = 2,
    /// <summary>
    /// PgSql数据库
    /// </summary>
    [Description("PgSql")]
    PgSql = 3,
    /// <summary>
    /// Oracle数据库
    /// </summary>
    [Description("Oracle")]
    Oracle = 4,
    /// <summary>
    /// Sqlite数据库
    /// </summary>
    [Description("Sqlite")]
    Sqlite = 5,
}