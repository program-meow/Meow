using System;
using System.Buffers;
using System.IO;
using System.Linq;
using System.Text;

namespace Meow.Helpers
{
    /// <summary>
    /// 字符串操作
    /// </summary>
    public static class String
    {
        /// <summary>
        /// 空字符串
        /// </summary>
        public static string Empty => string.Empty;

        /// <summary>
        /// 换行符
        /// </summary>
        public static string Line => System.Environment.NewLine;

        #region Unique  [全局唯一值]

        /// <summary>
        /// 全局唯一值
        /// </summary>
        public static string Unique()
        {
            return System.Guid.NewGuid().ToString("N");
        }

        #endregion

        #region 方法

        #region FirstUpperCase  [首字母大写]

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="value">值</param>
        public static string FirstUpperCase(string value)
        {
            if (Meow.Helpers.Validation.IsEmpty(value))
                return Empty;
            OperationStatus result = Rune.DecodeFromUtf16(value, out Rune rune, out int charsConsumed);
            if (result != OperationStatus.Done || Rune.IsUpper(rune))
                return value;
            return Rune.ToUpperInvariant(rune) + value[charsConsumed..];
        }

        #endregion

        #region FirstLowerCase  [首字母小写]

        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="value">值</param>
        public static string FirstLowerCase(string value)
        {
            if (Meow.Helpers.Validation.IsEmpty(value))
                return Empty;
            OperationStatus result = Rune.DecodeFromUtf16(value, out Rune rune, out int charsConsumed);
            if (result != OperationStatus.Done || Rune.IsLower(rune))
                return value;
            return Rune.ToLowerInvariant(rune) + value[charsConsumed..];
        }

        #endregion

        #region RemoveStart  [移除起始字符串]

        /// <summary>
        /// 移除起始字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="start">要移除的值</param>
        public static string RemoveStart(string value, string start)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;
            if (string.IsNullOrEmpty(start))
                return value;
            if (value.StartsWith(start, StringComparison.Ordinal) == false)
                return value;
            return value.Substring(start.Length, value.Length - start.Length);
        }

        /// <summary>
        /// 移除起始字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="start">要移除的值</param>
        public static StringBuilder RemoveStart(StringBuilder value, string start)
        {
            if (value == null || value.Length == 0)
                return null;
            if (string.IsNullOrEmpty(start))
                return value;
            if (start.Length > value.Length)
                return value;
            char[] chars = start.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (value[i] != chars[i])
                    return value;
            }
            return value.Remove(0, start.Length);
        }

        /// <summary>
        /// 移除起始字符串
        /// </summary>
        /// <param name="writer">字符串写入器</param>
        /// <param name="start">要移除的值</param>
        public static StringWriter RemoveStart(StringWriter writer, string start)
        {
            if (writer == null)
                return null;
            var builder = writer.GetStringBuilder();
            RemoveStart(builder, start);
            return writer;
        }

        #endregion

        #region RemoveEnd  [移除末尾字符串]

        /// <summary>
        /// 移除末尾字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="end">要移除的值</param>
        public static string RemoveEnd(string value, string end)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;
            if (string.IsNullOrEmpty(end))
                return value;
            if (value.EndsWith(end, StringComparison.Ordinal) == false)
                return value;
            return value.Substring(0, value.LastIndexOf(end, StringComparison.Ordinal));
        }

        /// <summary>
        /// 移除末尾字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="end">要移除的值</param>
        public static StringBuilder RemoveEnd(StringBuilder value, string end)
        {
            if (value == null || value.Length == 0)
                return null;
            if (string.IsNullOrEmpty(end))
                return value;
            if (end.Length > value.Length)
                return value;
            char[] chars = end.ToCharArray();
            for (int i = chars.Length - 1; i >= 0; i--)
            {
                int j = value.Length - (chars.Length - i);
                if (value[j] != chars[i])
                    return value;
            }
            return value.Remove(value.Length - end.Length, end.Length);
        }

        /// <summary>
        /// 移除末尾字符串
        /// </summary>
        /// <param name="writer">字符串写入器</param>
        /// <param name="end">要移除的值</param>
        public static StringWriter RemoveEnd(this StringWriter writer, string end)
        {
            if (writer == null)
                return null;
            var builder = writer.GetStringBuilder();
            RemoveEnd(builder, end);
            return writer;
        }

        #endregion

        #region Truncate  [截断字符串]

        /// <summary>
        /// 截断字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="length">返回长度</param>
        /// <param name="endCharCount">添加结束符号的个数，默认0，不添加</param>
        /// <param name="endChar">结束符号，默认为省略号</param>
        /// <returns>截断字符串</returns>
        public static string Truncate(string value, int length, int endCharCount = 0, string endChar = ".")
        {
            if (Meow.Helpers.Validation.IsEmpty(value))
                return Meow.Helpers.String.Empty;
            if (value.Length < length)
                return value;
            var result = new StringBuilder();
            result.Append(value.Substring(0, length));
            if (endCharCount < 1)
                return result.ToString();
            result.Append(Copy(endChar, endCharCount));
            return result.ToString();
        }

        #endregion

        #region Copy  [复制]

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="count">复制次数</param>
        /// <returns>复制好的字符串</returns>
        public static string Copy(string value, int count)
        {
            if (Meow.Helpers.Validation.IsEmpty(value))
                return Meow.Helpers.String.Empty;
            if (count <= 1)
                return value;
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < count; i++)
                result.Append(value);
            return result.ToString();
        }

        #endregion

        #region 获取拼音



        #endregion

        #region 全角 & 半角

        /// <summary>
        /// 转全角
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

        #region Distinct  [去除重复]

        /// <summary>
        /// 去除重复
        /// </summary>
        /// <param name="value">值，范例1："5555",返回"5",范例2："4545",返回"45"</param>
        public static string Distinct(string value)
        {
            var array = value.ToCharArray();
            return new string(array.Distinct().ToArray());
        }

        #endregion

        #endregion

        #region 判断

        /// <summary>
        /// 是否数字
        /// </summary>
        /// <param name="value">值</param>        
        public static bool IsNumber(string value)
        {
            if (Meow.Helpers.Validation.IsEmpty(value))
                return false;
            return Regex.IsMatch(value, Meow.Consts.RegexPattern.Number);
        }

        /// <summary>
        /// 是否包含数字
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsContainsNumber(string value)
        {
            if (Meow.Helpers.Validation.IsEmpty(value))
                return false;
            return Regex.IsMatch(value, Meow.Consts.RegexPattern.ContainsNumber);
        }

        /// <summary>
        /// 是否包含中文
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsContainsCn(string value)
        {
            if (Meow.Helpers.Validation.IsEmpty(value))
                return false;
            return Regex.IsMatch(value, Meow.Consts.RegexPattern.ContainsCn);
        }

        #endregion

    }
}
