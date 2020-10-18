using System.Text;
using Meow.Helper;

namespace Meow.Extension.Helper
{
    /// <summary>
    /// 字节扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">字符编码：默认Default</param>
        public static string GetString(this byte[] value, Encoding encoding = null)
        {
            return Byte.GetString(value, encoding);
        }

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="value">值</param>
        static byte[] Compress(this byte[] value)
        {
            return Byte.Compress(value);
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="value">值</param>
        public static byte[] Decompress(this byte[] value)
        {
            return Byte.Decompress(value);
        }
    }
}