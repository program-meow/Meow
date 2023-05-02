using System;
using System.Text;
using Meow.Extension;

namespace Meow.Helper
{
    /// <summary>
    /// 64进制URL编码操作
    /// </summary>
    public static class EncodeByBase64Url
    {
        private const char Base64PadCharacter = '=';
        private const string DoubleBase64PadCharacter = "==";
        private const char Base64Character62 = '+';
        private const char Base64Character63 = '/';
        private const char Base64UrlCharacter62 = '-';
        private const char Base64UrlCharacter63 = '_';

        /// <summary>
        /// 以下函数执行 base-64-url 编码，与常规base64编码的区别如下
        /// * 省略填充，因此填充字符 '=' 不必进行百分比编码
        /// * 第62和63个常规 base-64 编码字符 ('+'和'/') 被替换为 ('-'和'_')
        /// 这些更改使编码字母文件和 URL 安全
        /// </summary>
        /// <param name="value">要编码的字符串</param>
        /// <returns>Base 64 Url 的 UTF8 字节编码</returns>
        public static string Encode(string value)
        {
            if (value.IsEmpty())
                return string.Empty;
            byte[] inArray = Encoding.UTF8.GetBytes(value);
            return Encode(inArray);
        }

        /// <summary>
        /// 将一个8位无符号整数数组的子集转换为其等效的字符串表示形式，该字符串表示形式由 base-64-url 数字编码
        /// 参数将子集指定为输入数组中的偏移量，以及数组中要转换的元素数量
        /// </summary>
        /// <param name="inArray">由8位无符号整数组成的数组</param>
        /// <param name="length">inArray 中的偏移量</param>
        /// <param name="offset">要转换的 inArray 元素的数量</param>
        /// <returns>inArray 的长度元素的 base64 url编码的字符串表示形式，从位置偏移开始</returns>
        public static string Encode(byte[] inArray, int offset, int length)
        {
            if (inArray.IsEmpty())
                return string.Empty;
            return System.Convert.ToBase64String(inArray, offset, length)
                .Split(Base64PadCharacter)[0]
                .Replace(Base64Character62, Base64UrlCharacter62)
                .Replace(Base64Character63, Base64UrlCharacter63);
        }

        /// <summary>
        /// 将一个8位无符号整数数组的子集转换为其等效的字符串表示形式，该字符串表示形式由 base-64-url 数字编码
        /// 参数将子集指定为输入数组中的偏移量，以及数组中要转换的元素数量
        /// </summary>
        /// <param name="inArray">由8位无符号整数组成的数组</param>
        /// <returns>inArray 的长度元素的64进制 url 编码的字符串表示形式，从位置偏移开始</returns>
        public static string Encode(byte[] inArray)
        {
            if (inArray.IsEmpty())
                return string.Empty;
            return System.Convert.ToBase64String(inArray, 0, inArray.Length)
                .Split(Base64PadCharacter)[0]
                .Replace(Base64Character62, Base64UrlCharacter62)
                .Replace(Base64Character63, Base64UrlCharacter63);
        }

        /// <summary>
        /// 将指定的字符串(该字符串将二进制数据编码为base-64-url数字)转换为等效的8位无符号整数数组
        /// </summary>
        /// <param name="value">base64Url 编码字符串</param>
        /// <returns>UTF8 bytes</returns>
        public static byte[] DecodeToBytes(string value)
        {
            if (value.IsEmpty())
                return Array.Empty<byte>();
            value = value.Replace(Base64UrlCharacter62, Base64Character62);
            value = value.Replace(Base64UrlCharacter63, Base64Character63);
            switch (value.Length % 4)
            {
                case 0:
                    return System.Convert.FromBase64String(value);
                case 2:
                    value += DoubleBase64PadCharacter;
                    goto case 0;
                case 3:
                    value += Base64PadCharacter.ToString();
                    goto case 0;
                default:
                    throw new FormatException($"IDX10400: Unable to decode: '{value}' as Base64url encoded string.");
            }
        }

        /// <summary>
        /// 将字符串从 Base64 Url Encoded解码为 UTF8
        /// </summary>
        /// <param name="value">要解码的字符串</param>
        /// <returns>UTF8 字符串</returns>
        public static string Decode(string value)
        {
            if (value.IsEmpty())
                return string.Empty;
            return Encoding.UTF8.GetString(DecodeToBytes(value));
        }
    }
}
