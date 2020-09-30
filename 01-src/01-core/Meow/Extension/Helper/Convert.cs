using System.Collections.Generic;
using System.Linq;
using System.Text;
using DateTime = System.DateTime;
using Guid = System.Guid;

namespace Meow.Extension.Helper
{
    /// <summary>
    /// 类型转换扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 转换为bool
        /// </summary>
        /// <param name="value">值</param>
        public static bool ToBool(this object value)
        {
            return Meow.Helper.Bool.ToBool(value);
        }

        /// <summary>
        /// 转换为可空bool
        /// </summary>
        /// <param name="value">值</param>
        public static bool? ToBoolOrNull(this object value)
        {
            return Meow.Helper.Bool.ToBoolOrNull(value);
        }

        /// <summary>
        /// 转换为int
        /// </summary>
        /// <param name="value">值</param>
        public static int ToInt(this object value)
        {
            return Meow.Helper.Init.ToInt(value);
        }

        /// <summary>
        /// 转换为可空int
        /// </summary>
        /// <param name="value">值</param>
        public static int? ToIntOrNull(this object value)
        {
            return Meow.Helper.Init.ToIntOrNull(value);
        }

        /// <summary>
        /// 转换为long
        /// </summary>
        /// <param name="value">值</param>
        public static long ToLong(this object value)
        {
            return Meow.Helper.Long.ToLong(value);
        }

        /// <summary>
        /// 转换为可空long
        /// </summary>
        /// <param name="value">值</param>
        public static long? ToLongOrNull(this object value)
        {
            return Meow.Helper.Long.ToLongOrNull(value);
        }

        /// <summary>
        /// 转换为double
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="digits">小数位数</param>
        public static double ToDouble(this object value, int? digits = null)
        {
            return Meow.Helper.Double.ToDouble(value, digits);
        }

        /// <summary>
        /// 转换为可空double
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="digits">小数位数</param>
        public static double? ToDoubleOrNull(this object value, int? digits = null)
        {
            return Meow.Helper.Double.ToDoubleOrNull(value, digits);
        }

        /// <summary>
        /// 转换为decimal
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="digits">小数位数</param>
        public static decimal ToDecimal(this object value, int? digits = null)
        {
            return Meow.Helper.Decimal.ToDecimal(value, digits);
        }

        /// <summary>
        /// 转换为可空decimal
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="digits">小数位数</param>
        public static decimal? ToDecimalOrNull(this object value, int? digits = null)
        {
            return Meow.Helper.Decimal.ToDecimalOrNull(value, digits);
        }

        /// <summary>
        /// 转换为日期
        /// </summary>
        /// <param name="value">值</param>
        public static DateTime ToDate(this object value)
        {
            return Meow.Helper.DateTime.ToDate(value);
        }

        /// <summary>
        /// 转换为可空日期
        /// </summary>
        /// <param name="value">值</param>
        public static DateTime? ToDateOrNull(this object value)
        {
            return Meow.Helper.DateTime.ToDateOrNull(value);
        }

        /// <summary>
        /// 转换为Guid
        /// </summary>
        /// <param name="value">值</param>
        public static Guid ToGuid(this object value)
        {
            return Meow.Helper.Guid.ToGuid(value);
        }

        /// <summary>
        /// 转换为可空Guid
        /// </summary>
        /// <param name="value">值</param>
        public static Guid? ToGuidOrNull(this object value)
        {
            return Meow.Helper.Guid.ToGuidOrNull(value);
        }

        /// <summary>
        /// 转换为Guid集合
        /// </summary>
        /// <param name="value">值,范例: "83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A"</param>
        public static List<Guid> ToGuidList(this string value)
        {
            return Meow.Helper.Guid.ToGuidList(value);
        }

        /// <summary>
        /// 转换为Guid集合
        /// </summary>
        /// <param name="value">值</param>
        public static List<Guid> ToGuidList(this IEnumerable<object> value)
        {
            if (value == null)
                return new List<Guid>();
            return value.Select(t => t.ToGuid()).ToList();
        }

        /// <summary>
        /// 转换为字节数组
        /// </summary>
        /// <param name="value">值</param>
        public static byte[] ToBytes(this string value)
        {
            return Meow.Helper.Byte.ToBytes(value);
        }

        /// <summary>
        /// 转换为字节数组
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">字符编码</param>
        public static byte[] ToBytes(this string value, Encoding encoding)
        {
            return Meow.Helper.Byte.ToBytes(value, encoding);
        }
    }
}