namespace Meow.Const;

/// <summary>
/// 错误多语言键
/// </summary>
public static class ErrorLKey {
    /// <summary>
    /// 类型 {0} 不是枚举
    /// </summary>
    public const string TypeNotEnum = "TypeNotEnum";
    /// <summary>
    /// 无效的身份证
    /// </summary>
    public const string InvalidIdCard = "InvalidIdCard";
    /// <summary>
    /// 标识不能为空
    /// </summary>
    public const string IdIsNotEmpty = "IdIsNotEmpty";
    /// <summary>
    /// 不允许将节点移动到自己或子节点下
    /// </summary>
    public const string NotSupportMoveToChildren = "NotSupportMoveToChildren";
    /// <summary>
    /// 当前操作的数据已被其他人修改，请刷新后重试
    /// </summary>
    public const string ConcurrencyExceptionMessage = "ConcurrencyExceptionMessage";
    /// <summary>
    /// 仅允许添加一个条件,条件：{0}
    /// </summary>
    public const string CanOnlyOneCondition = "CanOnlyOneCondition";
}