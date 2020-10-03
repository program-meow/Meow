using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Meow.Extension.Helper;
using Meow.Parameter.Const;
using MicrosoftSystem = System;

namespace Meow.Helper
{
    /// <summary>
    /// 字符串操作
    /// </summary>
    public static class String
    {
        /// <summary>
        /// 泛型集合转换
        /// </summary>
        /// <typeparam name="T">目标元素类型</typeparam>
        /// <param name="value">以逗号分隔的元素集合字符串，范例:83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A</param>
        public static List<T> ToList<T>(string value)
        {
            var result = new List<T>();
            if (string.IsNullOrWhiteSpace(value))
                return result;
            var array = value.Split(',');
            result.AddRange(from each in array where !string.IsNullOrWhiteSpace(each) select Common.To<T>(each));
            return result;
        }

        #region PinYin(获取汉字的拼音简码)

        /// <summary>
        /// 获取汉字的拼音简码，即首字母缩写,范例：中国,返回zg
        /// </summary>
        /// <param name="value">汉字文本,范例： 中国</param>
        public static string PinYin(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;
            var result = new StringBuilder();
            foreach (char text in value)
                result.AppendFormat("{0}", ResolvePinYin(text));
            return result.ToString().ToLower();
        }

        /// <summary>
        /// 解析单个汉字的拼音简码
        /// </summary>
        private static string ResolvePinYin(char text)
        {
            byte[] charBytes = Encoding.UTF8.GetBytes(text.ToString());
            if (charBytes[0] <= 127)
                return text.ToString();
            var unicode = (ushort)(charBytes[0] * 256 + charBytes[1]);
            string pinYin = ResolveByCode(unicode);
            if (!string.IsNullOrWhiteSpace(pinYin))
                return pinYin;
            return ResolveByConst(text.ToString());
        }

        /// <summary>
        /// 使用字符编码方式获取拼音简码
        /// </summary>
        private static string ResolveByCode(ushort unicode)
        {
            if (unicode >= '\uB0A1' && unicode <= '\uB0C4')
                return "A";
            if (unicode >= '\uB0C5' && unicode <= '\uB2C0' && unicode != 45464)
                return "B";
            if (unicode >= '\uB2C1' && unicode <= '\uB4ED')
                return "C";
            if (unicode >= '\uB4EE' && unicode <= '\uB6E9')
                return "D";
            if (unicode >= '\uB6EA' && unicode <= '\uB7A1')
                return "E";
            if (unicode >= '\uB7A2' && unicode <= '\uB8C0')
                return "F";
            if (unicode >= '\uB8C1' && unicode <= '\uB9FD')
                return "G";
            if (unicode >= '\uB9FE' && unicode <= '\uBBF6')
                return "H";
            if (unicode >= '\uBBF7' && unicode <= '\uBFA5')
                return "J";
            if (unicode >= '\uBFA6' && unicode <= '\uC0AB')
                return "K";
            if (unicode >= '\uC0AC' && unicode <= '\uC2E7')
                return "L";
            if (unicode >= '\uC2E8' && unicode <= '\uC4C2')
                return "M";
            if (unicode >= '\uC4C3' && unicode <= '\uC5B5')
                return "N";
            if (unicode >= '\uC5B6' && unicode <= '\uC5BD')
                return "O";
            if (unicode >= '\uC5BE' && unicode <= '\uC6D9')
                return "P";
            if (unicode >= '\uC6DA' && unicode <= '\uC8BA')
                return "Q";
            if (unicode >= '\uC8BB' && unicode <= '\uC8F5')
                return "R";
            if (unicode >= '\uC8F6' && unicode <= '\uCBF9')
                return "S";
            if (unicode >= '\uCBFA' && unicode <= '\uCDD9')
                return "T";
            if (unicode >= '\uCDDA' && unicode <= '\uCEF3')
                return "W";
            if (unicode >= '\uCEF4' && unicode <= '\uD188')
                return "X";
            if (unicode >= '\uD1B9' && unicode <= '\uD4D0')
                return "Y";
            if (unicode >= '\uD4D1' && unicode <= '\uD7F9')
                return "Z";
            return string.Empty;
        }

        /// <summary>
        /// 通过拼音简码常量获取
        /// </summary>
        private static string ResolveByConst(string text)
        {
            int index = Chinese.PinYin.IndexOf(text, MicrosoftSystem.StringComparison.Ordinal);
            if (index < 0)
                return string.Empty;
            return Chinese.PinYin.Substring(index + 1, 1);
        }

        #endregion

        #region FirstLowerCase(首字母小写)

        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="value">值</param>
        public static string FirstLowerCase(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;
            return $"{value.Substring(0, 1).ToLower()}{value.Substring(1)}";
        }

        #endregion

        #region FirstUpperCase(首字母大写)

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="value">值</param>
        public static string FirstUpperCase(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;
            return $"{value.Substring(0, 1).ToUpper()}{value.Substring(1)}";
        }

        #endregion

        #region RemoveEnd(移除末尾字符串)

        /// <summary>
        /// 移除末尾字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="removeValue">要移除的值</param>
        public static string RemoveEnd(string value, string removeValue)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;
            if (string.IsNullOrWhiteSpace(removeValue))
                return value.SafeString();
            if (value.ToLower().EndsWith(removeValue.ToLower()))
                return value.Remove(value.Length - removeValue.Length, removeValue.Length);
            return value;
        }

        #endregion

        #region SplitWordGroup(分隔词组)

        /// <summary>
        /// 分隔词组
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="separator">分隔符，默认使用"-"分隔</param>
        public static string SplitWordGroup(string value, char separator = '-')
        {
            var pattern = @"([A-Z])(?=[a-z])|(?<=[a-z])([A-Z]|[0-9]+)";
            return string.IsNullOrWhiteSpace(value) ? string.Empty : MicrosoftSystem.Text.RegularExpressions.Regex.Replace(value, pattern, $"{separator}$1$2").TrimStart(separator).ToLower();
        }

        #endregion

        /// <summary>
        /// 换行符
        /// </summary>
        public static string Line => MicrosoftSystem.Environment.NewLine;
        /// <summary>
        /// 空字符串
        /// </summary>
        public static string Empty => MicrosoftSystem.String.Empty;

        #region 判断

        #region 正则表达式

        /// <summary>
        /// 数字正则表达式
        /// </summary>
        private static readonly Regex RegexNumeric = new Regex("^[0-9]+$");
        /// <summary>
        /// 数字（带正负号）正则表达式
        /// </summary>
        private static readonly Regex RegexNumericSign = new Regex("^[+-]?[0-9]+$");
        /// <summary>
        /// 小数正则表达式
        /// </summary>
        private static readonly Regex RegexDecimal = new Regex("^([0-9]+[.]?[0-9]+)|([0-9]+)$");
        /// <summary>
        /// 小数（带正负号）正则表达式
        /// </summary>
        private static readonly Regex RegexDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$");
        /// <summary>
        /// 中文正则表达式
        /// </summary>
        private static readonly Regex RegexChinese = new Regex("^[\u4e00-\u9fa5]*$");
        /// <summary>
        /// Email正则表达式
        /// </summary>
        private static readonly Regex RegexEmail = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        /// <summary>
        /// 邮政编码正则表达式
        /// </summary>
        private static readonly Regex RegexPostcode = new Regex(@"^(\d{6})$");
        /// <summary>
        /// URL正则表达式
        /// </summary>
        private static readonly Regex RegexUrl = new Regex(@"^(http|https|ftp|mms)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$");
        /// <summary>
        /// 日期正则表达式
        /// </summary>
        private static readonly Regex RegexDate = new Regex(@"^\d{4}(\-|\/|\.)\d{1,2}(\-|\/|\.)\d{1,2}$");
        /// <summary>
        /// 合法的电话号码正则表达式
        /// </summary>
        private static readonly Regex RegexTel = new Regex(@"^\d{6,8}|\d{3,4}-\d{6,8}$");
        /// <summary>
        /// 合法的手机号码正则表达式
        /// </summary>
        private static readonly Regex RegexMobile = new Regex(@"^1(3|4|5|7|8|9)\d{9}$");
        /// <summary>
        /// 合法的用户名正则表达式（限中文/英文/数字/减号/下划线）
        /// </summary>
        private static readonly Regex RegexUserName = new Regex("^([\u4E00-\u9FA5a-zA-Z0-9_-]){2,16}$");
        /// <summary>
        /// 合法的IPv4地址正则表达式
        /// </summary>
        private static readonly Regex RegexIp = new Regex(@"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        /// <summary>
        /// 合法身份证号正则表达式
        /// </summary>
        private static readonly Regex RegexIdCard = new Regex(@"^(11|12|13|14|15|21|22|23|31|32|33|34|35|36|37|41|42|43|44|45|46|50|51|52|53|54|61|62|63|64|65|71|81|82|91)(\d{13}|\d{15}[\dxX])$");

        #endregion

        /// <summary>
        /// 自定义验证
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="regexString">正则表达式字符串</param>
        public static bool IsMatch(string value, string regexString)
        {
            if (value.IsEmpty())
                return false;
            var reg = new Regex(regexString);
            var m = reg.Match(value);
            return m.Success;
        }

        /// <summary>
        /// 是否数字
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsNumeric(string value)
        {
            if (value.IsEmpty())
                return false;
            var m = RegexNumeric.Match(value);
            return m.Success;
        }

        /// <summary>
        /// 是否数字（带正负号）
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsNumberSign(string value)
        {
            if (value.IsEmpty())
                return false;
            var m = RegexNumericSign.Match(value);
            return m.Success;
        }

        /// <summary>
        /// 是否是小数
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsDecimal(string value)
        {
            if (value.IsEmpty())
                return false;
            var m = RegexDecimal.Match(value);
            return m.Success;
        }

        /// <summary>
        /// 是否是小数（带正负号）
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsDecimalSign(string value)
        {
            if (value.IsEmpty())
                return false;
            var m = RegexDecimalSign.Match(value);
            return m.Success;
        }

        /// <summary>
        /// 是否中文
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsChinese(string value)
        {
            if (value.IsEmpty())
                return false;
            var m = RegexChinese.Match(value);
            return m.Success;
        }

        /// <summary>
        /// 是否是邮箱
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmail(string value)
        {
            if (value.IsEmpty())
                return false;
            var m = RegexEmail.Match(value);
            return m.Success;
        }

        /// <summary>
        /// 是否是邮政编码
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsPostcode(string value)
        {
            if (value.IsEmpty())
                return false;
            var m = RegexPostcode.Match(value);
            return m.Success;
        }

        /// <summary>
        /// 是否是URL
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsUrl(string value)
        {
            if (value.IsEmpty())
                return false;
            var m = RegexUrl.Match(value);
            return m.Success;
        }

        /// <summary>
        /// 是否日期
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsDate(string value)
        {
            if (value.IsEmpty())
                return false;
            var m = RegexDate.Match(value);
            return m.Success;
        }

        /// <summary>
        /// 是否日期时间
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsDatetime(string value)
        {
            if (value.IsEmpty())
                return false;
            return MicrosoftSystem.DateTime.TryParse(value, out _);
        }

        /// <summary>
        /// 是否为合法的电话号码
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsTel(string value)
        {
            if (value.IsEmpty())
                return false;
            var m = RegexTel.Match(value);
            return m.Success;
        }

        /// <summary>
        /// 是否为合法的手机号码
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsMobile(string value)
        {
            if (value.IsEmpty())
                return false;
            var m = RegexMobile.Match(value);
            return m.Success;
        }

        /// <summary>
        /// 是否为合法的用户名（限中文/英文/数字/减号/下划线）
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsUserName(string value)
        {
            if (value.IsEmpty())
                return false;
            var m = RegexUserName.Match(value);
            return m.Success;
        }

        /// <summary>
        /// 是否为合法的IPv4地址
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsIp(string value)
        {
            if (value.IsEmpty())
                return false;
            var m = RegexIp.Match(value);
            return m.Success;
        }

        /// <summary>
        /// 是否合法身份证号
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsIdCard(string value)
        {
            if (value.IsEmpty())
                return false;
            var m = RegexIdCard.Match(value);
            return m.Success;
        }

        #endregion

        /// <summary>
        /// 获取邮箱名
        /// </summary>
        /// <param name="value">值</param>
        public static string GetEmailName(string value)
        {
            if (!IsEmail(value))
                return Empty;
            var list = value.Split("@");
            if (list.Count() < 1)
                return Empty;
            return list[0];
        }
    }
}