using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Meow.Extension;

namespace Meow.Helper
{
    /// <summary>
    /// 62进制编码操作
    /// </summary>
    public static class EncodeByBase62
    {
        /// <summary>
        /// 字符集
        /// </summary>
        private const string Charset = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="guid">Guid值</param>
        public static string Encode(Guid? guid)
        {
            if (guid.IsEmpty())
                return string.Empty;
            return Encode(guid.SafeValue());
        }

        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="guid">Guid值</param>
        public static string Encode(Guid guid)
        {
            List<byte> bytes = guid.ToByteArray().ToList();
            if (BitConverter.IsLittleEndian)
                bytes.Add(0);
            else
                bytes.Insert(0, 0);
            return Encode(bytes.ToArray());
        }

        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="value">值</param>
        public static string Encode(string value)
        {
            if (value.IsEmpty())
                return string.Empty;
            byte[] bytes = Encoding.ASCII.GetBytes(value);
            if (BitConverter.IsLittleEndian)
                System.Array.Reverse(bytes);
            return Encode(bytes);
        }

        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="num">长整型值</param>
        public static string Encode(long? num)
        {
            if (num == null)
                return string.Empty;
            return Encode(num.SafeValue());
        }

        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="num">长整型值</param>
        public static string Encode(long num)
        {
            List<byte> bytes = BitConverter.GetBytes(num).ToList();
            if (BitConverter.IsLittleEndian)
                bytes.Add(0);
            else
                bytes.Insert(0, 0);
            return Encode(bytes.ToArray());
        }

        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="bytes">字节值</param>
        private static string Encode(byte[] bytes)
        {
            string result = string.Empty;
            if (bytes.IsEmpty())
                return result;
            BigInteger bi = new BigInteger(bytes);
            do
            {
                result = Charset[(int)(bi % 62)] + result;
                bi /= 62;
            } while (bi > 0);
            return result;
        }

        /// <summary>
        /// 解码成字符串
        /// </summary>
        /// <param name="value">值</param>
        public static string DecodeToString(string value)
        {
            if (value.IsEmpty())
                return string.Empty;
            byte[] bytes = Decode(value);
            if (BitConverter.IsLittleEndian)
                System.Array.Reverse(bytes);
            return Encoding.ASCII.GetString(bytes);
        }

        /// <summary>
        /// 解码成Guid
        /// </summary>
        /// <param name="value">值</param>
        public static Guid? DecodeToGuid(string value)
        {
            if (value.IsEmpty())
                return null;
            List<byte> bytes = Decode(value).ToList();
            if (bytes.Count > 16)
            {
                if (BitConverter.IsLittleEndian)
                    bytes.RemoveAt(bytes.Count - 1);
                else
                    bytes.RemoveAt(0);
            }
            else if (bytes.Count < 16)
                bytes.AddRange(Enumerable.Repeat((byte)0, 16 - bytes.Count));
            return new Guid(bytes.ToArray());
        }

        /// <summary>
        /// 解码成长整型
        /// </summary>
        /// <param name="value">值</param>
        public static long? DecodeToLong(string value)
        {
            if (value.IsEmpty())
                return null;
            List<byte> bytes = Decode(value).ToList();
            if (bytes.Count > 8)
            {
                if (BitConverter.IsLittleEndian)
                    bytes.RemoveAt(bytes.Count - 1);
                else
                    bytes.RemoveAt(0);
            }
            else if (bytes.Count < 8)
                bytes.AddRange(Enumerable.Repeat((byte)0, 8 - bytes.Count));
            return BitConverter.ToInt64(bytes.ToArray(), 0);
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="value">值</param>
        private static byte[] Decode(string value)
        {
            if (value.IsEmpty())
                return Array.Empty<byte>();
            BigInteger result = new BigInteger(0);
            int len = value.Length;
            for (var i = 0; i < len; i++)
            {
                char ch = value[i];
                int num = Charset.IndexOf(ch);
                result += num * BigInteger.Pow(62, len - i - 1);
            }
            return result.ToByteArray();
        }
    }
}
