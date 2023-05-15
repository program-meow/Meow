namespace Meow.Data.Sql.Config;

/// <summary>
/// Sql配置访问器
/// </summary>
public interface ISqlOptionAccessor
{
    /// <summary>
    /// 获取Sql配置
    /// </summary>
    SqlOption GetOptions();
}