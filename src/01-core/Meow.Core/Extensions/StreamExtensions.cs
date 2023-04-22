using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SysStream = System.IO.Stream;

namespace Meow.Extensions
{
    /// <summary>
    /// 流扩展
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// 流转换为字节数组
        /// </summary>
        /// <param name="stream">流</param>
        public static byte[] ToBytes(this SysStream stream)
        {
            return Meow.Helpers.Stream.ToBytes(stream);
        }

        /// <summary>
        /// 字符串转换成字节数组
        /// </summary>
        /// <param name="data">数据,默认字符编码utf-8</param>        
        public static byte[] ToBytes(this string data)
        {
            return Meow.Helpers.Stream.ToBytes(data);
        }

        /// <summary>
        /// 字符串转换成字节数组
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="encoding">字符编码</param>
        public static byte[] ToBytes(this string data, Encoding encoding)
        {
            return Meow.Helpers.Stream.ToBytes(data, encoding);
        }

        /// <summary>
        /// 流转换为字节数组
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="cancellationToken">取消令牌</param>
        public static async Task<byte[]> ToBytesAsync(this Stream stream, CancellationToken cancellationToken = default)
        {
            return await Meow.Helpers.Stream.ToBytesAsync(stream, cancellationToken);
        }

        /// <summary>
        /// 字符串转换成流
        /// </summary>
        /// <param name="data">数据</param>
        public static Stream ToStream(this string data)
        {
            return Meow.Helpers.Stream.ToStream(data);
        }

        /// <summary>
        /// 字符串转换成流
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="encoding">字符编码</param>
        public static Stream ToStream(this string data, Encoding encoding)
        {
            return Meow.Helpers.Stream.ToStream(data, encoding);
        }
    }
}
