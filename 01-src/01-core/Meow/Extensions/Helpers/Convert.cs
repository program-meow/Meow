using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meow.Extensions.Helpers
{
    /// <summary>
    /// 类型转换扩展
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 转换为bool
        /// </summary>
        /// <param name="obj">数据</param>
        public static bool ToBool(this object obj)
        {
            return Meow.Helpers.Bool.ToBool(obj);
        }

        /// <summary>
        /// 转换为可空bool
        /// </summary>
        /// <param name="obj">数据</param>
        public static bool? ToBoolOrNull(this object obj)
        {
            return Meow.Helpers.Bool.ToBoolOrNull(obj);
        }

        /// <summary>
        /// 转换为int
        /// </summary>
        /// <param name="obj">数据</param>
        public static int ToInt(this object obj)
        {
            return Meow.Helpers.Init.ToInt(obj);
        }

        /// <summary>
        /// 转换为可空int
        /// </summary>
        /// <param name="obj">数据</param>
        public static int? ToIntOrNull(this object obj)
        {
            return Meow.Helpers.Init.ToIntOrNull(obj);
        }


        /// <summary>
        /// 转换为long
        /// </summary>
        /// <param name="obj">数据</param>
        public static long ToLong(this object obj)
        {
            return Meow.Helpers.Long.ToLong(obj);
        }

        /// <summary>
        /// 转换为可空long
        /// </summary>
        /// <param name="obj">数据</param>
        public static long? ToLongOrNull(this object obj)
        {
            return Meow.Helpers.Long.ToLongOrNull(obj);
        }

        /// <summary>
        /// 转换为double
        /// </summary>
        /// <param name="obj">数据</param>
        /// <param name="digits">小数位数</param>
        public static double ToDouble(this object obj, int? digits = null)
        {
            return Meow.Helpers.Double.ToDouble(obj, digits);
        }

        /// <summary>
        /// 转换为可空double
        /// </summary>
        /// <param name="obj">数据</param>
        /// <param name="digits">小数位数</param>
        public static double? ToDoubleOrNull(this object obj, int? digits = null)
        {
            return Meow.Helpers.Double.ToDoubleOrNull(obj, digits);
        }

        /// <summary>
        /// 转换为decimal
        /// </summary>
        /// <param name="obj">数据</param>
        /// <param name="digits">小数位数</param>
        public static decimal ToDecimal(this object obj, int? digits = null)
        {
            return Meow.Helpers.Decimal.ToDecimal(obj, digits);
        }

        /// <summary>
        /// 转换为可空decimal
        /// </summary>
        /// <param name="obj">数据</param>
        /// <param name="digits">小数位数</param>
        public static decimal? ToDecimalOrNull(this object obj, int? digits = null)
        {
            return Meow.Helpers.Decimal.ToDecimalOrNull(obj, digits);
        }

        /// <summary>
        /// 转换为日期
        /// </summary>
        /// <param name="obj">数据</param>
        public static DateTime ToDate(this object obj)
        {
            return Meow.Helpers.DateTime.ToDate(obj);
        }

        /// <summary>
        /// 转换为可空日期
        /// </summary>
        /// <param name="obj">数据</param>
        public static DateTime? ToDateOrNull(this object obj)
        {
            return Meow.Helpers.DateTime.ToDateOrNull(obj);
        }

        /// <summary>
        /// 转换为Guid
        /// </summary>
        /// <param name="obj">数据</param>
        public static Guid ToGuid(this object obj)
        {
            return Meow.Helpers.Guid.ToGuid(obj);
        }

        /// <summary>
        /// 转换为可空Guid
        /// </summary>
        /// <param name="obj">数据</param>
        public static Guid? ToGuidOrNull(this object obj)
        {
            return Meow.Helpers.Guid.ToGuidOrNull(obj);
        }

        /// <summary>
        /// 转换为Guid集合
        /// </summary>
        /// <param name="obj">数据,范例: "83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A"</param>
        public static List<Guid> ToGuidList(this string obj)
        {
            return Meow.Helpers.Guid.ToGuidList(obj);
        }

        /// <summary>
        /// 转换为Guid集合
        /// </summary>
        /// <param name="obj">字符串集合</param>
        public static List<Guid> ToGuidList(this IList<object> obj)
        {
            if (obj == null)
                return new List<Guid>();
            return obj.Select(t => t.ToGuid()).ToList();
        }

        /// <summary>
        /// 转换为字节数组
        /// </summary>
        /// <param name="obj">数据</param>
        public static byte[] ToBytes(this string obj)
        {
            return Meow.Helpers.Byte.ToBytes(obj);
        }

        /// <summary>
        /// 转换为字节数组
        /// </summary>
        /// <param name="obj">数据</param>
        /// <param name="encoding">字符编码</param>
        public static byte[] ToBytes(this string obj, Encoding encoding)
        {
            return Meow.Helpers.Byte.ToBytes(obj, encoding);
        }
    }
}
