using System.Data;

namespace Meow.Data.Sql;

/// <summary>
/// Sql配置
/// </summary>
public class SqlOption<T> : SqlOption where T : class
{
}

/// <summary>
/// Sql配置
/// </summary>
public class SqlOption
{
    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    public string ConnectionString { get; set; }
    /// <summary>
    /// 数据库连接
    /// </summary>
    public IDbConnection Connection { get; set; }
    /// <summary>
    /// 日志类别
    /// </summary>
    public string LogCategory { get; set; } = "Meow.Data.Sql";
}