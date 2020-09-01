﻿using System.Text;

namespace Meow.Helper
{
    /// <summary>
    /// 字节操作
    /// </summary>
    public static class Byte
    {
        /// <summary>
        /// 转换为字节数组
        /// </summary>
        /// <param name="value">值</param>        
        public static byte[] ToBytes(string value)
        {
            return ToBytes(value, Encoding.UTF8);
        }

        /// <summary>
        /// 转换为字节数组
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">字符编码</param>
        public static byte[] ToBytes(string value, Encoding encoding)
        {
            return string.IsNullOrWhiteSpace(value) ? new byte[] { } : encoding.GetBytes(value);
        }
    }
}