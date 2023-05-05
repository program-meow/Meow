using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using Meow.Extension;

namespace Meow.Helper
{
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
        /// 通用泛型转换
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="array">集合</param>
        public static List<T> ToList<T>(IEnumerable<object> array)
        {
            if (array.IsEmpty())
                return new List<T>(); ;
            return array.Select(To<T>).ToList();
        }

        /// <summary>
        /// 通用泛型转换
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
        /// 通用泛型转换
        /// </summary>
        /// <typeparam name="TSource">集合元素类型</typeparam>
        /// <typeparam name="TKey">键元素类型</typeparam>
        /// <typeparam name="TOut">返回元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="keySelector">选择器</param>
        public static List<TOut> ToListBy<TSource, TKey, TOut>(IEnumerable<TSource> array, Func<TSource, TKey> keySelector)
        {
            if (array.IsEmpty())
                return new List<TOut>();
            List<TKey> values = ToListBy(array, keySelector);
            return values.Select(t => To<TOut>(t)).ToList();
        }

        /// <summary>
        /// 泛型集合转换
        /// </summary>
        /// <typeparam name="T">目标元素类型</typeparam>
        /// <param name="value">以字符分隔的元素集合字符串，范例:83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A</param>
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
    }
}
