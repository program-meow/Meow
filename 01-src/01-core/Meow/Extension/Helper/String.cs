using System.Collections.Generic;

namespace Meow.Extension.Helper
{
    /// <summary>
    /// JSON扩展
    /// </summary>
    public static partial class Extension
    {
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
    }
}