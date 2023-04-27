using System.Text;

namespace Meow.Extension
{
    /// <summary>
    /// 哈希扩展
    /// </summary>
    public static class HashExtensions
    {
        /// <summary>
        /// 转换哈希字符串
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="type">哈希类型</param>
        /// <returns>哈希算法处理之后的字符串</returns>
        public static string ToHash(this string str, Meow.Helper.HashType type)
        {
            return Meow.Helper.Hash.ToString(type, str);
        }

        /// <summary>
        /// 转换哈希字符串
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="type">哈希类型</param>
        /// <param name="isLower">是否是小写</param>
        /// <returns>哈希算法处理之后的字符串</returns>
        public static string ToHash(this string str, Meow.Helper.HashType type, bool isLower)
        {
            return Meow.Helper.Hash.ToString(type, str, isLower);
        }

        /// <summary>
        /// 转换哈希字符串
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="type">哈希类型</param>
        /// <param name="key">key</param>
        /// <param name="isLower">是否是小写</param>
        /// <returns>哈希算法处理之后的字符串</returns>
        public static string ToHash(this string str, Meow.Helper.HashType type, string key, bool isLower = false)
        {
            return Meow.Helper.Hash.ToString(type, str, key, isLower);

        }

        /// <summary>
        /// 转换哈希字符串
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="type">哈希类型</param>
        /// <param name="encoding">编码类型</param>
        /// <param name="isLower">是否是小写</param>
        /// <returns>哈希算法处理之后的字符串</returns>
        public static string ToHash(this string str, Meow.Helper.HashType type, Encoding encoding, bool isLower = false)
        {
            return Meow.Helper.Hash.ToString(type, str, encoding, isLower);
        }

        /// <summary>
        /// 转换哈希字符串
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="type">哈希类型</param>
        /// <param name="key">key</param>
        /// <param name="encoding">编码类型</param>
        /// <param name="isLower">是否是小写</param>
        /// <returns>哈希算法处理之后的字符串</returns>
        public static string ToHash(this string str, Meow.Helper.HashType type, string key, Encoding encoding, bool isLower = false)
        {
            return Meow.Helper.Hash.ToString(type, str, key, encoding, isLower);
        }

        /// <summary>
        /// 计算字符串Hash值
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="type">hash类型</param>
        /// <returns>hash过的字节数组</returns>
        public static string ToStringByHash(this byte[] source, Meow.Helper.HashType type)
        {
            return Meow.Helper.Hash.ToString(type, source);
        }

        /// <summary>
        /// 计算字符串Hash值
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="type">hash类型</param>
        /// <param name="isLower">isLower</param>
        /// <returns>hash过的字节数组</returns>
        public static string ToStringByHash(this byte[] source, Meow.Helper.HashType type, bool isLower)
        {
            return Meow.Helper.Hash.ToString(type, source, isLower);
        }

        /// <summary>
        /// 转换哈希字符串
        /// </summary>
        /// <param name="source">源</param>
        /// <param name="type">哈希类型</param>
        /// <param name="key">key</param>
        /// <param name="isLower">是否是小写</param>
        /// <returns>哈希算法处理之后的字符串</returns>
        public static string ToStringByHash(this byte[] source, Meow.Helper.HashType type, byte[] key, bool isLower = false)
        {
            return Meow.Helper.Hash.ToString(type, source, key, isLower);
        }

        /// <summary>
        /// 计算字符串Hash值
        /// </summary>
        /// <param name="str">要hash的字符串</param>
        /// <param name="type">hash类型</param>
        /// <returns>hash过的字节数组</returns>
        public static byte[] ToBytesByHash(this string str, Meow.Helper.HashType type)
        {
            return Meow.Helper.Hash.ToBytes(type, str);
        }

        /// <summary>
        /// 计算字符串Hash值
        /// </summary>
        /// <param name="str">要hash的字符串</param>
        /// <param name="type">hash类型</param>
        /// <param name="encoding">编码类型</param>
        /// <returns>hash过的字节数组</returns>
        public static byte[] ToBytesByHash(this string str, Meow.Helper.HashType type, Encoding encoding)
        {
            return Meow.Helper.Hash.ToBytes(type, str, encoding);
        }

        /// <summary>
        /// 转换Hash后的字节数组
        /// </summary>
        /// <param name="bytes">原字节数组</param>
        /// <param name="type">哈希类型</param>
        /// <returns></returns>
        public static byte[] ToHash(this byte[] bytes, Meow.Helper.HashType type)
        {
            return Meow.Helper.Hash.ToBytes(type, bytes);
        }

        /// <summary>
        /// 转换Hash后的字节数组
        /// </summary>
        /// <param name="type">哈希类型</param>
        /// <param name="key">key</param>
        /// <param name="bytes">原字节数组</param>
        /// <returns></returns>
        public static byte[] ToHash(this byte[] bytes, Meow.Helper.HashType type, byte[] key)
        {
            return Meow.Helper.Hash.ToBytes(type, bytes, key);
        }
    }
}
