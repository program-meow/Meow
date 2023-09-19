using Meow.Data.Sql.Builder;
using Meow.Data.Sql.Builder.Operation;

namespace Meow.Data.Sql;

/// <summary>
/// Sql生成器
/// </summary>
public interface ISqlBuilder : ISqlContent, ISqlOperation {
    /// <summary>
    /// 复制Sql生成器副本
    /// </summary>
    ISqlBuilder Clone();
    /// <summary>
    /// 创建一个新的Sql生成器
    /// </summary>
    ISqlBuilder New();
    /// <summary>
    /// 清理
    /// </summary>
    ISqlBuilder Clear();
    /// <summary>
    /// 获取Sql语句
    /// </summary>
    string GetSql();
}