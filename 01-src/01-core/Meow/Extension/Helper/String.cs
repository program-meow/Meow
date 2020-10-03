using System.Collections.Generic;
using Meow.Helper;

namespace Meow.Extension.Helper
{
    /// <summary>
    /// JSON扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// 泛型集合转换
        /// </summary>
        /// <typeparam name="T">目标元素类型</typeparam>
        /// <param name="value">以逗号分隔的元素集合字符串，范例:83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A</param>
        public static List<T> ToList<T>(this string value)
        {
            return Meow.Helper.String.ToList<T>(value);
        }

        /// <summary>
        /// 获取汉字的拼音简码，即首字母缩写,范例：中国,返回zg
        /// </summary>
        /// <param name="value">汉字文本,范例： 中国</param>
        public static string PinYin(this string value)
        {
            return Meow.Helper.String.PinYin(value);
        }

        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="value">值</param>
        public static string FirstLowerCase(this string value)
        {
            return Meow.Helper.String.FirstLowerCase(value);
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="value">值</param>
        public static string FirstUpperCase(this string value)
        {
            return Meow.Helper.String.FirstUpperCase(value);
        }

        /// <summary>
        /// 移除末尾字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="removeValue">要移除的值</param>
        public static string RemoveEnd(this string value, string removeValue)
        {
            return Meow.Helper.String.RemoveEnd(value, removeValue);
        }

        /// <summary>
        /// 分隔词组s
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="separator">分隔符，默认使用"-"分隔</param>
        public static string SplitWordGroup(this string value, char separator = '-')
        {
            return Meow.Helper.String.SplitWordGroup(value, separator);
        }

        #region 判断

        /// <summary>
        /// 自定义验证
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="regexString">正则表达式字符串</param>
        public static bool IsMatch(this string value, string regexString)
        {
            return String.IsMatch(value, regexString);
        }

        /// <summary>
        /// 是否数字
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsNumeric(this string value)
        {
            return String.IsNumeric(value);
        }

        /// <summary>
        /// 是否数字（带正负号）
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsNumberSign(this string value)
        {
            return String.IsNumberSign(value);
        }

        /// <summary>
        /// 是否是小数
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsDecimal(this string value)
        {
            return String.IsDecimal(value);
        }

        /// <summary>
        /// 是否是小数（带正负号）
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsDecimalSign(this string value)
        {
            return String.IsDecimalSign(value);
        }

        /// <summary>
        /// 是否中文
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsChinese(this string value)
        {
            return String.IsChinese(value);
        }

        /// <summary>
        /// 是否是邮箱
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmail(this string value)
        {
            return String.IsEmail(value);
        }

        /// <summary>
        /// 是否是邮政编码
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsPostcode(this string value)
        {
            return String.IsPostcode(value);
        }

        /// <summary>
        /// 是否是URL
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsUrl(this string value)
        {
            return String.IsUrl(value);
        }

        /// <summary>
        /// 是否日期
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsDate(this string value)
        {
            return String.IsDate(value);
        }

        /// <summary>
        /// 是否日期时间
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsDatetime(this string value)
        {
            return String.IsDatetime(value);
        }

        /// <summary>
        /// 是否为合法的电话号码
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsTel(this string value)
        {
            return String.IsTel(value);
        }

        /// <summary>
        /// 是否为合法的手机号码
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsMobile(this string value)
        {
            return String.IsMobile(value);
        }

        /// <summary>
        /// 是否为合法的用户名（限中文/英文/数字/减号/下划线）
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsUserName(this string value)
        {
            return String.IsUserName(value);
        }

        /// <summary>
        /// 是否为合法的IPv4地址
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsIp(this string value)
        {
            return String.IsIp(value);
        }

        /// <summary>
        /// 是否合法身份证号
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsIdCard(this string value)
        {
            return String.IsIdCard(value);
        }

        #endregion

        /// <summary>
        /// 获取邮箱名
        /// </summary>
        /// <param name="value">值</param>
        public static string GetEmailName(this string value)
        {
            return String.GetEmailName(value);

        }

        /// <summary>
        /// 如果为空则返回覆盖值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="coverValue">覆盖值</param>
        public static string EmptyCover(this string value, string coverValue)
        {
            return value.IsEmpty() ? coverValue : value;
        }
    }
}