using Meow.Helper;

namespace Meow.Extension.Helper
{
    /// <summary>
    /// 字节扩展
    /// </summary>
    public static partial class Extension
    {
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