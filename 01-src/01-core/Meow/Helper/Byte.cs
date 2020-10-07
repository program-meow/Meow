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
        /// 转换为字节数组
        /// </summary>
        /// <param name="value">值</param>        
        public static byte[] ToBytes(object value)
        {
            return ToBytes(value, Encoding.UTF8);
        }

        /// <summary>
        /// 转换为字节数组
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">字符编码</param>
        public static byte[] ToBytes(object value, Encoding encoding)
        {
            var str = value.SafeString();
            return string.IsNullOrWhiteSpace(str) ? new byte[] { } : encoding.GetBytes(str);
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