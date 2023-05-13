using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using Meow.Enum;
using Meow.Extension;

namespace Meow.Helper;

/// <summary>
/// 类型转换
/// </summary>
public static class Convert
{
    #region To  [通用泛型转换]

    /// <summary>
    /// 通用泛型转换
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="value">值</param>
    public static T To<T>(object value)
    {
        if (value == null)
            return default;
        if (value is string && value.SafeString().IsEmpty())
            return default;
        System.Type type = Meow.Helper.Common.GetType<T>();
        string typeName = type.Name.ToUpperInvariant();
        try
        {
            if (typeName == Meow.Type.TypeName.String.ToLower() || typeName == Meow.Type.TypeName.Guid.ToLower())
                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(value.SafeString());
            if (type.IsEnum)
                return Meow.Helper.Enum.Parse<T>(value);
            if (value is IConvertible)
                return (T)System.Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
            if (value is JsonElement element)
            {
                JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return element.GetRawText().ToJsonObject<T>(options);
            }
            return (T)value;
        }
        catch
        {
            return default;
        }
    }

    #endregion

    #region ToList  [泛型集合转换]

    /// <summary>
    /// 通用泛型转换 - 字符串集合
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="array">集合</param>
    public static List<string> ToListString<T>(IEnumerable<T> array)
    {
        if (array.IsEmpty())
            return new List<string>();
        return array.Select(t => t.SafeString()).ToList();
    }

    /// <summary>
    /// 通用泛型转换
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="array">集合</param>
    public static List<T> ToList<T>(IEnumerable<object> array)
    {
        if (array.IsEmpty())
            return new List<T>();
        return array.Select(To<T>).ToList();
    }

    /// <summary>
    /// 泛型集合转换
    /// </summary>
    /// <typeparam name="T">目标元素类型</typeparam>
    /// <param name="value">以字符分隔的元素集合字符串</param>
    /// <param name="separator">分隔符，默认逗号作为分隔符</param>
    public static List<T> ToList<T>(string value, string separator = ",")
    {
        List<T> result = new List<T>();
        if (value.IsEmpty())
            return result;
        string[] array = value.Split(separator);
        result.AddRange(from each in array where !each.IsEmpty() select To<T>(each));
        return result;
    }

    /// <summary>
    /// 提取集合
    /// </summary>
    /// <typeparam name="TSource">集合元素类型</typeparam>
    /// <typeparam name="TKey">键元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="keySelector">选择器</param>
    public static List<TKey> ToListBy<TSource, TKey>(IEnumerable<TSource> array, Func<TSource, TKey> keySelector)
    {
        if (array.IsEmpty())
            return new List<TKey>();
        return array.Select(keySelector).ToList();
    }

    /// <summary>
    /// 提取并合并集合
    /// </summary>
    /// <typeparam name="TSource">集合元素类型</typeparam>
    /// <typeparam name="TKey">键元素类型</typeparam>
    /// <param name="array">集合</param>
    /// <param name="keySelector">选择器</param>
    public static List<TKey> ToListRangeBy<TSource, TKey>(IEnumerable<TSource> array, Func<TSource, IEnumerable<TKey>> keySelector)
    {
        List<TKey> result = new List<TKey>();
        if (array.IsEmpty())
            return result;
        List<IEnumerable<TKey>> list = ToListBy(array, keySelector);
        foreach (IEnumerable<TKey> each in list)
            result.AddRange(each);
        return result;
    }

    #endregion

    #region ToInt & ToIntOrNull  [转换为32位整型 & 可空整型]

    /// <summary>
    /// 转换为32位整型
    /// </summary>
    /// <param name="value">值</param>
    public static int ToInt(object value)
    {
        return ToIntOrNull(value) ?? 0;
    }

    /// <summary>
    /// 转换为32位可空整型
    /// </summary>
    /// <param name="value">值</param>
    public static int? ToIntOrNull(object value)
    {
        bool success = int.TryParse(value.SafeString(), out int result);
        if (success)
            return result;
        try
        {
            double? temp = ToDoubleOrNull(value, 0);
            if (temp == null)
                return null;
            return System.Convert.ToInt32(temp);
        }
        catch
        {
            return null;
        }
    }

    #endregion

    #region ToFloat & ToFloatOrNull  [转换为32位浮点型 & 可空浮点型]

    /// <summary>
    /// 转换为32位浮点型,并按指定小数位舍入
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="digits">小数位数</param>
    public static float ToFloat(object value, int? digits = null)
    {
        return ToFloatOrNull(value, digits) ?? 0;
    }

    /// <summary>
    /// 转换为32位可空浮点型,并按指定小数位舍入
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="digits">小数位数</param>
    public static float? ToFloatOrNull(object value, int? digits = null)
    {
        bool success = float.TryParse(value.SafeString(), out Single result);
        if (!success)
            return null;
        if (digits == null)
            return result;
        return (float)System.Math.Round(result, digits.Value);
    }

    #endregion

    #region ToDouble & ToDoubleOrNull  [转换为64位浮点型 & 可空浮点型]

    /// <summary>
    /// 转换为64位浮点型,并按指定小数位舍入
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="digits">小数位数</param>
    public static double ToDouble(object value, int? digits = null)
    {
        return ToDoubleOrNull(value, digits) ?? 0;
    }

    /// <summary>
    /// 转换为64位可空浮点型,并按指定小数位舍入
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="digits">小数位数</param>
    public static double? ToDoubleOrNull(object value, int? digits = null)
    {
        bool success = double.TryParse(value.SafeString(), out double result);
        if (!success)
            return null;
        if (digits == null)
            return result;
        return System.Math.Round(result, digits.Value);
    }

    #endregion

    #region ToLong & ToLongOrNull  [转换为64位整型 & 可空整型]

    /// <summary>
    /// 转换为64位整型
    /// </summary>
    /// <param name="value">值</param>
    public static long ToLong(object value)
    {
        return ToLongOrNull(value) ?? 0;
    }

    /// <summary>
    /// 转换为64位可空整型
    /// </summary>
    /// <param name="value">值</param>
    public static long? ToLongOrNull(object value)
    {
        bool success = long.TryParse(value.SafeString(), out long result);
        if (success)
            return result;
        try
        {
            decimal? temp = ToDecimalOrNull(value, 0);
            if (temp == null)
                return null;
            return System.Convert.ToInt64(temp);
        }
        catch
        {
            return null;
        }
    }

    #endregion

    #region ToDecimal & ToDecimalOrNull  [转换为128位浮点型 & 可空浮点型]

    /// <summary>
    /// 转换为128位浮点型,并按指定小数位舍入
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="digits">小数位数</param>
    public static decimal ToDecimal(object value, int? digits = null)
    {
        return ToDecimalOrNull(value, digits) ?? 0;
    }

    /// <summary>
    /// 转换为128位可空浮点型,并按指定小数位舍入
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="digits">小数位数</param>
    public static decimal? ToDecimalOrNull(object value, int? digits = null)
    {
        bool success = decimal.TryParse(value.SafeString(), out decimal result);
        if (!success)
            return null;
        if (digits == null)
            return result;
        return System.Math.Round(result, digits.Value);
    }

    #endregion

    #region ToDateTime & ToDateTimeOrNull  [转换为日期 & 可空日期]

    /// <summary>
    /// 转换为日期
    /// </summary>
    /// <param name="value">值</param>
    public static DateTime ToDateTime(object value)
    {
        return ToDateTimeOrNull(value) ?? DateTime.MinValue;
    }

    /// <summary>
    /// 转换为可空日期
    /// </summary>
    /// <param name="value">值</param>
    public static DateTime? ToDateTimeOrNull(object value)
    {
        bool success = DateTime.TryParse(value.SafeString(), out DateTime result);
        if (success == false)
            return null;
        return result;
    }

    #endregion

    #region ToBool & ToBoolOrNull  [转换为布尔值 & 可空布尔值]

    /// <summary>
    /// 转换为布尔值
    /// </summary>
    /// <param name="value">值</param>
    public static bool ToBool(object value)
    {
        return ToBoolOrNull(value) ?? false;
    }

    /// <summary>
    /// 转换为可空布尔值
    /// </summary>
    /// <param name="value">输入值</param>
    public static bool? ToBoolOrNull(object value)
    {
        string strValue = value.SafeString().ToLower();
        switch (strValue)
        {
            //false
            case "0":
                return false;
            case "否":
                return false;
            case "不":
                return false;
            case "no":
                return false;
            case "fail":
                return false;
            //true
            case "1":
                return true;
            case "是":
                return true;
            case "ok":
                return true;
            case "yes":
                return true;
        }
        return bool.TryParse(strValue, out bool result) ? result : null;
    }

    #endregion

    #region ToGuid & ToGuidOrNull  [转换为Guid & 可空Guid]

    /// <summary>
    /// 转换为Guid
    /// </summary>
    /// <param name="value">值</param>
    public static Guid ToGuid(object value)
    {
        return ToGuidOrNull(value) ?? Guid.Empty;
    }

    /// <summary>
    /// 转换为可空Guid
    /// </summary>
    /// <param name="value">值</param>
    public static Guid? ToGuidOrNull(object value)
    {
        return Guid.TryParse(value.SafeString(), out Guid result) ? result : null;
    }

    #endregion

    #region ToGuidList & ToGuidOrNullList  [转换为Guid集合 & 可空Guid集合]

    /// <summary>
    /// 转换为Guid集合
    /// </summary>
    /// <param name="value">以逗号分隔的Guid集合字符串</param>
    /// <param name="separator">分隔符，默认逗号作为分隔符</param>
    public static List<Guid> ToGuidList(string value, string separator = ",")
    {
        string[] array = value.Split(separator);
        return ToGuidList(array);
    }

    /// <summary>
    /// 转换为Guid集合
    /// </summary>
    /// <param name="array">字符串集合</param>
    public static List<Guid> ToGuidList(IEnumerable<string> array)
    {
        if (array == null)
            return new List<Guid>();
        return array.Select(ToGuid).ToList();
    }

    /// <summary>
    /// 转换为可空Guid集合
    /// </summary>
    /// <param name="value">以逗号分隔的Guid集合字符串</param>
    /// <param name="separator">分隔符，默认逗号作为分隔符</param>
    public static List<Guid?> ToGuidOrNullList(string value, string separator = ",")
    {
        string[] array = value.Split(separator);
        return ToGuidOrNullList(array);
    }

    /// <summary>
    /// 转换为可空Guid集合
    /// </summary>
    /// <param name="array">字符串集合</param>
    public static List<Guid?> ToGuidOrNullList(IEnumerable<string> array)
    {
        if (array == null)
            return new List<Guid?>();
        return array.Select(ToGuidOrNull).ToList();
    }

    #endregion

    #region ToBytes  [转换为字节数组]

    /// <summary>
    /// 转换为字节数组
    /// </summary>
    /// <param name="value">值</param>        
    public static byte[] ToBytes(string value)
    {
        return ToBytes(value, Encoding.UTF8);
    }

    /// <summary>
    /// 转换为字节数组
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="encoding">字符编码</param>
    public static byte[] ToBytes(string value, Encoding encoding)
    {
        return value.IsEmpty() ? new byte[] { } : encoding.GetBytes(value);
    }

    #endregion

    #region ToDictionary [对象转换为属性名值对]

    /// <summary>
    /// 对象转换为属性名值对
    /// </summary>
    /// <param name="data">对象</param>
    public static IDictionary<string, object> ToDictionary(object data)
    {
        if (data == null)
            return null;
        if (data is IEnumerable<KeyValuePair<string, object>> dic)
            return new Dictionary<string, object>(dic);
        Dictionary<string, object> result = new Dictionary<string, object>();
        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(data))
        {
            object value = property.GetValue(data);
            result.Add(property.Name, value);
        }
        return result;
    }

    /// <summary>
    /// 对象转换为属性名值对
    /// </summary>
    /// <typeparam name="TValue">值元素类型</typeparam>
    /// <param name="data">对象</param>
    public static IDictionary<string, TValue> ToDictionary<TValue>(object data)
    {
        Dictionary<string, TValue> result = new Dictionary<string, TValue>();
        IDictionary<string, object> dictionary = ToDictionary(data);
        if (dictionary.IsEmpty())
            return result;
        foreach (KeyValuePair<string, object> each in dictionary)
            result.Add(each.Key, To<TValue>(each.Value));
        return result;
    }

    #endregion

    #region ToRmbByCn  [转换为大写人民币]

    /// <summary>
    /// 转换为大写人民币
    /// </summary>
    /// <param name="money">金额</param>
    /// <param name="isIgnoreSgn">是否忽略正负，忽略时正数省略</param>
    public static string ToMoneyByRmbCn(decimal? money, bool isIgnoreSgn = true)
    {
        return ToMoneyByRmbCn(money.SafeValue(), isIgnoreSgn);
    }

    /// <summary>
    /// 转换为大写人民币
    /// </summary>
    /// <param name="money">金额</param>
    /// <param name="isIgnoreSgn">是否忽略正负，忽略时正数省略</param>
    public static string ToMoneyByRmbCn(decimal money, bool isIgnoreSgn = true)
    {
        if (money == 0)
            return "零元整";
        string head = isIgnoreSgn ? "" : money > 0 ? "正 " : "负 ";
        string format = money.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A").Replace("0B0A", "@");
        string simplify = System.Text.RegularExpressions.Regex.Replace(format, @"((?<=-|^)[^1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
        string result = System.Text.RegularExpressions.Regex.Replace(simplify, ".", match => "负元空零壹贰叁肆伍陆柒捌玖空空空空空空整分角拾佰仟万亿兆京垓秭穰"[match.Value[0] - '-'].ToString());
        return $"{head}{result}";
    }

    #endregion

    #region ToMoneyByNum  [转换为数字货币]

    /// <summary>
    /// 转换为数字货币
    /// </summary>
    /// <param name="money">金额</param>
    /// <param name="moneyType">币种。不设置则无货币符号前缀</param>
    /// <param name="isIgnoreSgn">是否忽略正负，忽略时正数省略</param>
    public static string ToMoneyByNum(decimal? money, MoneyEnum? moneyType = null, bool isIgnoreSgn = false)
    {
        return ToMoneyByNum(money.SafeValue(), moneyType, isIgnoreSgn);
    }

    /// <summary>
    /// 转换为数字货币
    /// </summary>
    /// <param name="money">金额</param>
    /// <param name="moneyType">币种。不设置则无货币符号前缀</param>
    /// <param name="isIgnoreSgn">是否忽略正负，忽略时正数省略</param>
    public static string ToMoneyByNum(decimal money, MoneyEnum? moneyType = null, bool isIgnoreSgn = false)
    {
        string moneyDescription = moneyType == null ? "" : $"{moneyType.GetDescription()} ";
        if (money == 0)
            return $"{moneyDescription}{money.SafeString()}";
        string sign = isIgnoreSgn ? "" : money > 0 ? "+" : "-";
        string moneyStr = money.SafeString().RemoveStart("+").RemoveStart("-");
        string[] moneyArray = moneyStr.Split('.');
        char[] moneyIntegerChar = moneyArray[0].ToCharArray();
        StringBuilder stringBuilder = new StringBuilder();
        int count = 0;
        for (int i = moneyIntegerChar.Length; i > 0; i--)
        {
            count += 1;
            stringBuilder.Append(moneyIntegerChar[i - 1]);
            if (count != 3)
                continue;
            count = 0;
            stringBuilder.Append(",");
        }
        string result = stringBuilder.ToString().RemoveEnd(",").Reverse();
        string resultEnd = moneyArray.Length > 1 ? $".{moneyArray[1]}" : "";
        return $"{moneyDescription}{sign}{result}{resultEnd}";
    }

    #endregion
}