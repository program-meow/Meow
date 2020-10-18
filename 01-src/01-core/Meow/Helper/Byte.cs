using System.Text;
using Meow.Extension.Helper;
using MicrosoftSystem = System;

namespace Meow.Helper
{
    /// <summary>
    /// 字节操作
    /// </summary>
    public static class Byte
    {
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">字符编码：默认Default</param>
        public static string GetString(byte[] value, Encoding encoding = null)
        {
            encoding ??= Encoding.Default;
            return encoding.GetString(value);
        }

        /// <summary>
        /// 转换为字节数组
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">字符编码：默认UTF8</param>
        public static byte[] ToBytes(object value, Encoding encoding = null)
        {
            var str = value.SafeString();
            if (str.IsEmpty())
                return new byte[] { };
            encoding ??= Encoding.UTF8;
            return encoding.GetBytes(str);
        }

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="value">值</param>
        public static byte[] Compress(byte[] value)
        {
            var ms = new MicrosoftSystem.IO.MemoryStream();
            var cs = new MicrosoftSystem.IO.Compression.GZipStream(ms, MicrosoftSystem.IO.Compression.CompressionMode.Compress, true);
            cs.Write(value, 0, value.Length);
            cs.Close();
            return ms.ToArray();
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="value">值</param>
        public static byte[] Decompress(byte[] value)
        {
            var ms = new MicrosoftSystem.IO.MemoryStream(value);
            var cs = new MicrosoftSystem.IO.Compression.GZipStream(ms, MicrosoftSystem.IO.Compression.CompressionMode.Decompress);
            var outBuffer = new MicrosoftSystem.IO.MemoryStream();
            var block = new byte[1024];
            while (true)
            {
                var bytesRead = cs.Read(block, 0, block.Length);
                if (bytesRead <= 0)
                    break;
                outBuffer.Write(block, 0, bytesRead);
            }
            cs.Close();
            return outBuffer.ToArray();
        }
    }
}