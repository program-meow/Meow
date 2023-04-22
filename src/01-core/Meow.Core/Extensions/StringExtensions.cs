using System.IO;
using System.Text;

namespace Meow.Extensions
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class StringExtensions
    {
        #region 方法

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="value">值</param>
        public static string FirstUpperCase(this string value)
        {
            return Meow.Helpers.String.FirstUpperCase(value);
        }

        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="value">值</param>
        public static string FirstLowerCase(this string value)
        {
            return Meow.Helpers.String.FirstLowerCase(value);
        }

        /// <summary>
        /// 移除起始字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="start">要移除的值</param>
        public static string RemoveStart(this string value, string start)
        {
            return Meow.Helpers.String.RemoveStart(value, start);
        }

        /// <summary>
        /// 移除起始字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="start">要移除的值</param>
        public static StringBuilder RemoveStart(this StringBuilder value, string start)
        {
            return Meow.Helpers.String.RemoveStart(value, start);
        }

        /// <summary>
        /// 移除起始字符串
        /// </summary>
        /// <param name="writer">字符串写入器</param>
        /// <param name="start">要移除的值</param>
        public static StringWriter RemoveStart(this StringWriter writer, string start)
        {
            return Meow.Helpers.String.RemoveStart(writer, start);
        }

        /// <summary>
        /// 移除末尾字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="end">要移除的值</param>
        public static string RemoveEnd(this string value, string end)
        {
            return Meow.Helpers.String.RemoveEnd(value, end);
        }

        /// <summary>
        /// 移除末尾字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="end">要移除的值</param>
        public static StringBuilder RemoveEnd(this StringBuilder value, string end)
        {
            return Meow.Helpers.String.RemoveEnd(value, end);
        }

        /// <summary>
        /// 移除末尾字符串
        /// </summary>
        /// <param name="writer">字符串写入器</param>
        /// <param name="end">要移除的值</param>
        public static StringWriter RemoveEnd(this StringWriter writer, string end)
        {
            return Meow.Helpers.String.RemoveEnd(writer, end);
        }

        /// <summary>
        /// 截断字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="length">返回长度</param>
        /// <param name="endCharCount">添加结束符号的个数，默认0，不添加</param>
        /// <param name="endChar">结束符号，默认为省略号</param>
        /// <returns>截断字符串</returns>
        public static string Truncate(this string value, int length, int endCharCount = 0, string endChar = ".")
        {
            return Meow.Helpers.String.Truncate(value, length, endCharCount, endChar);
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="count">复制次数</param>
        /// <returns>复制好的字符串</returns>
        public static string Copy(this string value, int count)
        {
            return Meow.Helpers.String.Copy(value, count);
        }

        /// <summary>
        /// 转全角
        /// </summary>
        /// <param name="value">值</param>
        public static string ToSbcCase(this string value)
        {
            return Meow.Helpers.String.ToSbcCase(value);
        }

        /// <summary>
        /// 转半角
        /// </summary>
        /// <param name="value">值</param>
        public static string ToDbcCase(this string value)
        {
            return Meow.Helpers.String.ToDbcCase(value);
        }

        /// <summary>
        /// 去除重复
        /// </summary>
        /// <param name="value">值，范例1："5555",返回"5",范例2："4545",返回"45"</param>
        public static string Distinct(this string value)
        {
            return Meow.Helpers.String.Distinct(value);
        }

        #endregion

        #region 判断

        /// <summary>
        /// 是否数字
        /// </summary>
        /// <param name="value">值</param>        
        public static bool IsNumber(this string value)
        {
            return Meow.Helpers.String.IsNumber(value);
        }

        /// <summary>
        /// 是否包含数字
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsContainsNumber(string value)
        {
            return Meow.Helpers.String.IsContainsNumber(value);
        }

        /// <summary>
        /// 是否包含中文
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsContainsCn(this string value)
        {
            return Meow.Helpers.String.IsContainsCn(value);
        }

        #endregion

    }
}
