using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SysStream = System.IO.Stream;

namespace Meow.Helpers
{
    /// <summary>
    /// 流操作
    /// </summary>
    public static class Stream
    {
        #region ToBytes  [流转换为字节数组]

        /// <summary>
        /// 流转换为字节数组
        /// </summary>
        /// <param name="stream">流</param>
        public static byte[] ToBytes(SysStream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            byte[] buffer = new byte[stream.Length];
            int read = stream.Read(buffer, 0, buffer.Length);
            return buffer;
        }

        /// <summary>
        /// 字符串转换成字节数组
        /// </summary>
        /// <param name="data">数据,默认字符编码utf-8</param>        
        public static byte[] ToBytes(string data)
        {
            return ToBytes(data, Encoding.UTF8);
        }

        /// <summary>
        /// 字符串转换成字节数组
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="encoding">字符编码</param>
        public static byte[] ToBytes(string data, Encoding encoding)
        {
            if (Meow.Helpers.Validation.IsEmpty(data))
                return new byte[] { };
            return encoding.GetBytes(data);
        }

        #endregion

        #region ToBytesAsync  [流转换为字节数组]

        /// <summary>
        /// 流转换为字节数组
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="cancellationToken">取消令牌</param>
        public static async Task<byte[]> ToBytesAsync(SysStream stream, CancellationToken cancellationToken = default)
        {
            stream.Seek(0, SeekOrigin.Begin);
            byte[] buffer = new byte[stream.Length];
            int readAsync = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
            return buffer;
        }

        #endregion

        #region ToStream  [字符串转换成流]

        /// <summary>
        /// 字符串转换成流
        /// </summary>
        /// <param name="data">数据</param>
        public static SysStream ToStream(string data)
        {
            return ToStream(data, Encoding.UTF8);
        }

        /// <summary>
        /// 字符串转换成流
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="encoding">字符编码</param>
        public static SysStream ToStream(string data, Encoding encoding)
        {
            if (Meow.Helpers.Validation.IsEmpty(data))
                return SysStream.Null;
            return new MemoryStream(ToBytes(data, encoding));
        }

        #endregion
    }
}
