using System;
using Meow.Extension;

namespace Meow.Helper
{
    /// <summary>
    /// 32进制编码操作
    /// </summary>
    public static class EncodeByBase32
    {
        /// <summary>
        /// 编码
        /// </summary>
        /// <param name="value">值</param>
        public static byte[] Encode(string value)
        {
            if (value.IsEmpty())
                return Array.Empty<byte>();
            value = value.TrimEnd('=');
            int byteCount = value.Length * 5 / 8;
            byte[] returnArray = new byte[byteCount];
            byte curByte = 0, bitsRemaining = 8;
            int arrayIndex = 0;
            foreach (char c in value)
            {
                int cValue = CharToValue(c);

                int mask;
                if (bitsRemaining > 5)
                {
                    mask = cValue << (bitsRemaining - 5);
                    curByte = (byte)(curByte | mask);
                    bitsRemaining -= 5;
                }
                else
                {
                    mask = cValue >> (5 - bitsRemaining);
                    curByte = (byte)(curByte | mask);
                    returnArray[arrayIndex++] = curByte;
                    curByte = (byte)(cValue << (3 + bitsRemaining));
                    bitsRemaining += 3;
                }
            }
            if (arrayIndex != byteCount)
                returnArray[arrayIndex] = curByte;
            return returnArray;
        }

        /// <summary>
        /// 解码
        /// </summary>
        /// <param name="value">值</param>
        public static string Decode(byte[] value)
        {
            if (value.IsEmpty())
                return string.Empty;
            int charCount = (int)System.Math.Ceiling(value.Length / 5d) * 8;
            char[] returnArray = new char[charCount];
            byte nextChar = 0, bitsRemaining = 5;
            int arrayIndex = 0;
            foreach (var b in value)
            {
                nextChar = (byte)(nextChar | (b >> (8 - bitsRemaining)));
                returnArray[arrayIndex++] = ValueToChar(nextChar);

                if (bitsRemaining < 4)
                {
                    nextChar = (byte)((b >> (3 - bitsRemaining)) & 31);
                    returnArray[arrayIndex++] = ValueToChar(nextChar);
                    bitsRemaining += 5;
                }

                bitsRemaining -= 3;
                nextChar = (byte)((b << bitsRemaining) & 31);
            }
            if (arrayIndex != charCount)
            {
                returnArray[arrayIndex++] = ValueToChar(nextChar);
                while (arrayIndex != charCount) returnArray[arrayIndex++] = '=';
            }
            return new string(returnArray);
        }

        /// <summary>
        /// 字符转值
        /// </summary>
        /// <param name="c">字符</param>
        private static int CharToValue(char c)
        {
            var value = (int)c;

            //65-90 == uppercase letters
            if (value < 91 && value > 64)
            {
                return value - 65;
            }
            //50-55 == numbers 2-7
            if (value < 56 && value > 49)
            {
                return value - 24;
            }
            //97-122 == lowercase letters
            if (value < 123 && value > 96)
            {
                return value - 97;
            }

            throw new ArgumentException("Character is not a Base32 character.", nameof(c));
        }

        /// <summary>
        /// 字节值转字符
        /// </summary>
        /// <param name="b">字节</param>
        private static char ValueToChar(byte b)
        {
            if (b < 26)
            {
                return (char)(b + 65);
            }
            if (b < 32)
            {
                return (char)(b + 24);
            }
            throw new ArgumentException("Byte is not a Base32 value.", nameof(b));
        }
    }
}
