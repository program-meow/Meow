using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;

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
            if (value is string && Validation.IsEmpty(Common.SafeString(value)))
                return default;
            System.Type type = Common.GetType<T>();
            string typeName = type.Name.ToUpperInvariant();
            try
            {
                if (typeName == Meow.Type.TypeName.String.ToLower() || typeName == Meow.Type.TypeName.Guid.ToLower())
                    return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(Common.SafeString(value));
                if (type.IsEnum)
                    return Enum.Parse<T>(value);
                if (value is IConvertible)
                    return (T)System.Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
                if (value is JsonElement element)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return Json.ToObject<T>(element.GetRawText(), options);
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
            List<T> result = new List<T>();
            if (Validation.IsEmpty(array))
                return result;
            result.AddRange(array.Select(To<T>));
            return result;
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
            if (Validation.IsEmpty(value))
                return result;
            string[] array = value.Split(separator);
            result.AddRange(from each in array where !Validation.IsEmpty(each) select To<T>(each));
            return result;
        }

        #endregion

        #region ToInt  [转换为32位整型]

        /// <summary>
        /// 转换为32位整型
        /// </summary>
        /// <param name="value">值</param>
        public static int ToInt(object value)
        {
            return ToIntOrNull(value) ?? 0;
        }

        #endregion

        #region ToIntOrNull  [转换为32位可空整型]

        /// <summary>
        /// 转换为32位可空整型
        /// </summary>
        /// <param name="value">值</param>
        public static int? ToIntOrNull(object value)
        {
            bool success = int.TryParse(Common.SafeString(value), out int result);
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

        #region ToFloat  [转换为32位浮点型]

        /// <summary>
        /// 转换为32位浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="digits">小数位数</param>
        public static float ToFloat(object value, int? digits = null)
        {
            return ToFloatOrNull(value, digits) ?? 0;
        }

        #endregion

        #region ToFloatOrNull  [转换为32位可空浮点型]

        /// <summary>
        /// 转换为32位可空浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="digits">小数位数</param>
        public static float? ToFloatOrNull(object value, int? digits = null)
        {
            bool success = float.TryParse(Common.SafeString(value), out Single result);
            if (!success)
                return null;
            if (digits == null)
                return result;
            return (float)System.Math.Round(result, digits.Value);
        }

        #endregion

        #region ToDouble  [转换为64位浮点型]

        /// <summary>
        /// 转换为64位浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="digits">小数位数</param>
        public static double ToDouble(object value, int? digits = null)
        {
            return ToDoubleOrNull(value, digits) ?? 0;
        }

        #endregion

        #region ToDoubleOrNull  [转换为64位可空浮点型]

        /// <summary>
        /// 转换为64位可空浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="digits">小数位数</param>
        public static double? ToDoubleOrNull(object value, int? digits = null)
        {
            bool success = double.TryParse(Common.SafeString(value), out double result);
            if (!success)
                return null;
            if (digits == null)
                return result;
            return System.Math.Round(result, digits.Value);
        }

        #endregion

        #region ToLong  [转换为64位整型]

        /// <summary>
        /// 转换为64位整型
        /// </summary>
        /// <param name="value">值</param>
        public static long ToLong(object value)
        {
            return ToLongOrNull(value) ?? 0;
        }

        #endregion

        #region ToLongOrNull  [转换为64位可空整型]

        /// <summary>
        /// 转换为64位可空整型
        /// </summary>
        /// <param name="value">值</param>
        public static long? ToLongOrNull(object value)
        {
            bool success = long.TryParse(Common.SafeString(value), out long result);
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

        #region ToDecimal  [转换为128位浮点型]

        /// <summary>
        /// 转换为128位浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="digits">小数位数</param>
        public static decimal ToDecimal(object value, int? digits = null)
        {
            return ToDecimalOrNull(value, digits) ?? 0;
        }

        #endregion

        #region ToDecimalOrNull  [转换为128位可空浮点型]

        /// <summary>
        /// 转换为128位可空浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="digits">小数位数</param>
        public static decimal? ToDecimalOrNull(object value, int? digits = null)
        {
            bool success = decimal.TryParse(Common.SafeString(value), out decimal result);
            if (!success)
                return null;
            if (digits == null)
                return result;
            return System.Math.Round(result, digits.Value);
        }

        #endregion

        #region ToDateTime  [转换为日期]

        /// <summary>
        /// 转换为日期
        /// </summary>
        /// <param name="value">值</param>
        public static DateTime ToDateTime(object value)
        {
            return ToDateTimeOrNull(value) ?? DateTime.MinValue;
        }

        #endregion

        #region ToDateTimeOrNull  [转换为可空日期]

        /// <summary>
        /// 转换为可空日期
        /// </summary>
        /// <param name="value">值</param>
        public static DateTime? ToDateTimeOrNull(object value)
        {
            bool success = DateTime.TryParse(Common.SafeString(value), out DateTime result);
            if (success == false)
                return null;
            return result;
        }

        #endregion

        #region ToBool  [转换为布尔值]

        /// <summary>
        /// 转换为布尔值
        /// </summary>
        /// <param name="value">值</param>
        public static bool ToBool(object value)
        {
            return ToBoolOrNull(value) ?? false;
        }

        #endregion

        #region ToBoolOrNull  [转换为可空布尔值]

        /// <summary>
        /// 转换为可空布尔值
        /// </summary>
        /// <param name="value">输入值</param>
        public static bool? ToBoolOrNull(object value)
        {
            string strValue = Common.SafeString(value);
            switch (strValue)
            {
                case "1":
                    return true;
                case "0":
                    return false;
            }
            return bool.TryParse(strValue, out bool result) ? result : null;
        }

        #endregion

        #region ToGuid  [转换为Guid]

        /// <summary>
        /// 转换为Guid
        /// </summary>
        /// <param name="value">值</param>
        public static Guid ToGuid(object value)
        {
            return ToGuidOrNull(value) ?? Guid.Empty;
        }

        #endregion

        #region ToGuidOrNull  [转换为可空Guid]

        /// <summary>
        /// 转换为可空Guid
        /// </summary>
        /// <param name="value">值</param>
        public static Guid? ToGuidOrNull(object value)
        {
            return Guid.TryParse(Common.SafeString(value), out Guid result) ? result : null;
        }

        #endregion

        #region ToGuidList  [转换为Guid集合]

        /// <summary>
        /// 转换为Guid集合
        /// </summary>
        /// <param name="value">以逗号分隔的Guid集合字符串，范例:83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A</param>
        /// <param name="separator">分隔符，默认逗号作为分隔符</param>
        public static List<Guid> ToGuidList(string value, string separator = ",")
        {
            return ToList<Guid>(value, separator);
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
            return Validation.IsEmpty(value) ? new byte[] { } : encoding.GetBytes(value);
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

        #endregion

        #region 全角 & 半角

        /// <summary>
        /// 转全角(SBC case)
        /// </summary>
        /// <param name="value">值</param>
        public static string ToSbcCase(string value)
        {
            char[] c = value.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }

        /// <summary>
        /// 转半角
        /// </summary>
        /// <param name="value">值</param>
        public static string ToDbcCase(string value)
        {
            char[] c = value.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        #endregion
    }
}
