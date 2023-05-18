using Meow.Type;
using System.Collections.Generic;
using System.Reflection;
using SystemType = System.Type;

namespace Meow.Extension;

/// <summary>
/// 反射扩展
/// </summary>
public static class ReflectionExtensions
{
    /// <summary>
    /// 获取类型枚举
    /// </summary>
    /// <param name="type">类型</param>
    public static Meow.Type.TypeEnum? GetTypeEnum(this SystemType type)
    {
        return Meow.Helper.Reflection.GetTypeEnum(type);
    }

    /// <summary>
    /// 解析对象
    /// </summary>
    /// <param name="value">值</param>
    public static TypeItem Analyzing(this object value)
    {
        return Meow.Helper.Reflection.Analyzing(value);
    }

    /// <summary>
    /// 解析对象到字典
    /// </summary>
    /// <param name="value">值</param>
    public static Dictionary<string, object> AnalyzingToDictionary(this object value)
    {
        return Meow.Helper.Reflection.AnalyzingToDictionary(value);
    }


    #region GetPropertyValue  [获取实例上的属性值]

    /// <summary>
    /// 获取实例上的属性值
    /// </summary>
    /// <param name="member">成员信息</param>
    /// <param name="instance">成员所在的类实例</param>
    public static object GetPropertyValue(this MemberInfo member, object instance)
    {
        return Meow.Helper.Reflection.GetPropertyValue(member, instance);
    }

    #endregion
}