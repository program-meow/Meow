using System.Data;
using Meow.Extension;

namespace Meow.Data.Sql.Extension;

/// <summary>
/// Sql配置扩展
/// </summary>
public static class SqlOptionExtension
{
    #region ConnectionString  [设置数据库连接字符串]

    /// <summary>
    /// 设置数据库连接字符串
    /// </summary>
    /// <param name="option">源</param>
    /// <param name="connectionString">数据库连接字符串</param>
    public static SqlOption ConnectionString(this SqlOption option, string connectionString)
    {
        option.CheckNull(nameof(option));
        option.ConnectionString = connectionString;
        return option;
    }

    #endregion

    #region Connection  [设置数据库连接]

    /// <summary>
    /// 设置数据库连接
    /// </summary>
    /// <param name="option">源</param>
    /// <param name="connection">数据库连接</param>
    public static SqlOption Connection(this SqlOption option, IDbConnection connection)
    {
        option.CheckNull(nameof(option));
        option.Connection = connection;
        return option;
    }

    #endregion

    #region LogCategory  [设置日志类别]

    /// <summary>
    /// 设置日志类别
    /// </summary>
    /// <param name="option">源</param>
    /// <param name="logCategory">日志类别</param>
    public static SqlOption LogCategory(this SqlOption option, string logCategory)
    {
        option.CheckNull(nameof(option));
        option.LogCategory = logCategory;
        return option;
    }

    #endregion
}