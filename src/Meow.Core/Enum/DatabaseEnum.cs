using System.ComponentModel;

namespace Meow.Enum;

/// <summary>
/// 数据库类型
/// </summary>
public enum DatabaseEnum
{
    /// <summary>
    /// Sql Server数据库
    /// </summary>
    [Description("Sql Server数据库")]
    SqlServer = 1,
    /// <summary>
    /// MySql数据库
    /// </summary>
    [Description("MySql数据库")]
    MySql = 2,
    /// <summary>
    /// PostgreSQL数据库
    /// </summary>
    [Description("PostgreSQL数据库")]
    PgSql = 3,
    /// <summary>
    /// Oracle数据库
    /// </summary>
    [Description("Oracle数据库")]
    Oracle = 4,
}