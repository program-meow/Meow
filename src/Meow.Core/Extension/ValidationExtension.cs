using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Meow.Extension;

/// <summary>
/// 验证扩展
/// </summary>
public static class ValidationExtension
{
    /// <summary>
    /// 是否为null
    /// </summary>
    /// <param name="value">值</param>
    public static bool IsNull(this object value)
    {
        return Meow.Helper.Validation.IsNull(value);
    }

    /// <summary>
    /// 检测对象是否为null,为null则抛出<see cref="ArgumentNullException"/>异常
    /// </summary>
    /// <param name="obj">对象</param>
    /// <param name="parameterName">参数名</param>
    public static void CheckNull(this object obj, string parameterName)
    {
        Meow.Helper.Validation.CheckNull(obj, parameterName);
    }

    #region IsEmpty  [是否为空]

    /// <summary>
    /// 是否为空
    /// </summary>
    /// <param name="value">值</param>
    public static bool IsEmpty([NotNullWhen(false)] this string value)
    {
        return Meow.Helper.Validation.IsEmpty(value);
    }

    /// <summary>
    /// 是否为空
    /// </summary>
    /// <param name="value">值</param>
    public static bool IsEmpty(this Guid value)
    {
        return Meow.Helper.Validation.IsEmpty(value);
    }

    /// <summary>
    /// 是否为空
    /// </summary>
    /// <param name="value">值</param>
    public static bool IsEmpty([NotNullWhen(false)] this Guid? value)
    {
        return Meow.Helper.Validation.IsEmpty(value);
    }

    /// <summary>
    /// 是否为空
    /// </summary>
    /// <param name="value">值</param>
    public static bool IsEmpty(this DateTime value)
    {
        return Meow.Helper.Validation.IsEmpty(value);
    }

    /// <summary>
    /// 是否为空
    /// </summary>
    /// <param name="value">值</param>
    public static bool IsEmpty([NotNullWhen(false)] this DateTime? value)
    {
        return Meow.Helper.Validation.IsEmpty(value);
    }

    /// <summary>
    /// 是否为空
    /// </summary>
    /// <param name="array">集合</param>
    public static bool IsEmpty<T>(this IEnumerable<T> array)
    {
        return Meow.Helper.Validation.IsEmpty<T>(array);
    }

    #endregion

    /// <summary>
    /// 是否数字
    /// </summary>
    /// <param name="value">值</param>        
    public static bool IsNumber(this string value)
    {
        return Meow.Helper.Validation.IsNumber(value);
    }

    /// <summary>
    /// 是否手机号
    /// </summary>
    /// <param name="value">值</param>        
    public static bool IsPhone(this string value)
    {
        return Meow.Helper.Validation.IsPhone(value);
    }

    /// <summary>
    /// 是否座机号码（国内）
    /// </summary>
    /// <param name="value">值</param>        
    public static bool IsLandline(this string value)
    {
        return Meow.Helper.Validation.IsLandline(value);
    }

    /// <summary>
    /// 是否邮箱
    /// </summary>
    /// <param name="value">值</param>        
    public static bool IsEmail(this string value)
    {
        return Meow.Helper.Validation.IsEmail(value);
    }

    /// <summary>
    /// 是否包含数字
    /// </summary>
    /// <param name="value">值</param>
    public static bool IsContainsNumber(this string value)
    {
        return Meow.Helper.Validation.IsContainsNumber(value);
    }

    /// <summary>
    /// 是否包含中文
    /// </summary>
    /// <param name="value">值</param>
    public static bool IsContainsCn(this string value)
    {
        return Meow.Helper.Validation.IsContainsCn(value);
    }

    /// <summary>
    /// 是否身份证号
    /// </summary>
    /// <param name="value">值</param>        
    public static bool IsIdCard(this string value)
    {
        return Meow.Helper.Validation.IsIdCard(value);
    }
}