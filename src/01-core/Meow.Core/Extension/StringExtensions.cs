using System.IO;
using System.Text;

namespace Meow.Extension
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class StringExtensions
    {
        #region FirstUpperCase  [首字母大写]

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="value">值</param>
        public static string FirstUpperCase(this string value)
        {
            return Meow.Helper.String.FirstUpperCase(value);
        }

        #endregion

        #region FirstLowerCase  [首字母小写]

        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="value">值</param>
        public static string FirstLowerCase(this string value)
        {
            return Meow.Helper.String.FirstLowerCase(value);
        }

        #endregion

        #region RemoveStart  [移除起始字符串]

        /// <summary>
        /// 移除起始字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="start">要移除的值</param>
        public static string RemoveStart(this string value, string start)
        {
            return Meow.Helper.String.RemoveStart(value, start);
        }

        /// <summary>
        /// 移除起始字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="start">要移除的值</param>
        public static StringBuilder RemoveStart(this StringBuilder value, string start)
        {
            return Meow.Helper.String.RemoveStart(value, start);
        }

        /// <summary>
        /// 移除起始字符串
        /// </summary>
        /// <param name="writer">字符串写入器</param>
        /// <param name="start">要移除的值</param>
        public static StringWriter RemoveStart(this StringWriter writer, string start)
        {
            return Meow.Helper.String.RemoveStart(writer, start);
        }

        #endregion

        #region RemoveEnd  [移除末尾字符串]

        /// <summary>
        /// 移除末尾字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="end">要移除的值</param>
        public static string RemoveEnd(this string value, string end)
        {
            return Meow.Helper.String.RemoveEnd(value, end);
        }

        /// <summary>
        /// 移除末尾字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="end">要移除的值</param>
        public static StringBuilder RemoveEnd(this StringBuilder value, string end)
        {
            return Meow.Helper.String.RemoveEnd(value, end);
        }

        /// <summary>
        /// 移除末尾字符串
        /// </summary>
        /// <param name="writer">字符串写入器</param>
        /// <param name="end">要移除的值</param>
        public static StringWriter RemoveEnd(this StringWriter writer, string end)
        {
            return Meow.Helper.String.RemoveEnd(writer, end);
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
        public static string Truncate(this string value, int length, int endCharCount = 0, string endChar = ".")
        {
            return Meow.Helper.String.Truncate(value, length, endCharCount, endChar);
        }

        #endregion

        #region Copy  [复制]

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="count">复制次数</param>
        /// <returns>复制好的字符串</returns>
        public static string Copy(this string value, int count)
        {
            return Meow.Helper.String.Copy(value, count);
        }

        #endregion

        #region Distinct  [去除重复]

        /// <summary>
        /// 去除重复
        /// </summary>
        /// <param name="value">值，范例1："5555",返回"5",范例2："4545",返回"45"</param>
        public static string Distinct(this string value)
        {
            return Meow.Helper.String.Distinct(value);
        }

        #endregion

        #region GetSimilarityRate  [计算匹配率/相似度]

        /// <summary>
        /// 计算相似度
        /// </summary>
        /// <param name="firstValue">第一个值</param>
        /// <param name="secondValue">第二个值</param>
        public static Meow.Helper.String.SimilarityRateResult GetSimilarityRate(this string firstValue, string secondValue)
        {
            return Meow.Helper.String.GetSimilarityRate(firstValue, secondValue);
        }

        #endregion

        #region HideSensitiveInfo  [隐藏敏感信息]

        /// <summary>
        /// 隐藏敏感信息
        /// </summary>
        /// <param name="info">信息实体</param>
        /// <param name="left">左边保留的字符数</param>
        /// <param name="right">右边保留的字符数</param>
        /// <param name="placeholderCount">占位符数量</param>
        /// <param name="placeholder">占位符。默认：使用 * 符号</param>
        /// <param name="basedOnLeft">当长度异常时，是否显示左边 ，true显示左边，false显示右边 </param>
        public static string HideSensitiveInfo(this string info, int left, int right, int placeholderCount = 4, char placeholder = '*', bool basedOnLeft = true)
        {
            return Meow.Helper.String.HideSensitiveInfo(info, left, right, placeholderCount, placeholder, basedOnLeft);
        }

        /// <summary>
        /// 隐藏手机号细节
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="left">左边保留字符数</param>
        /// <param name="right">右边保留字符数</param>
        public static string HidePhoneDetail(this string phone, int left = 3, int right = 4)
        {
            return Meow.Helper.String.HidePhoneDetail(phone, left, right);
        }

        /// <summary>
        /// 隐藏邮箱地址细节
        /// </summary>
        /// <param name="email">邮箱地址</param>
        /// <param name="left">邮箱地址头保留字符个数，默认值设置为3</param>
        public static string HideEmailDetail(this string email, int left = 3)
        {
            return Meow.Helper.String.HidePhoneDetail(email, left);
        }

        #endregion

        #region ToSbcCase & ToDbcCase  [全角 & 半角]

        /// <summary>
        /// 转全角
        /// </summary>
        /// <param name="value">值</param>
        public static string ToSbcCase(this string value)
        {
            return Meow.Helper.String.ToSbcCase(value);
        }

        /// <summary>
        /// 转半角
        /// </summary>
        /// <param name="value">值</param>
        public static string ToDbcCase(this string value)
        {
            return Meow.Helper.String.ToDbcCase(value);
        }

        #endregion

        #region GetRepeatCount  [获取重复次数]

        /// <summary>
        /// 获取重复次数
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="repeatValue">重复值</param>
        /// <param name="isFuzzy">是否模糊大小写</param>
        public static int GetRepeatCount(string value, string repeatValue, bool isFuzzy = false)
        {
            return Meow.Helper.String.GetRepeatCount(value, repeatValue, isFuzzy);
        }

        #endregion

    }
}
