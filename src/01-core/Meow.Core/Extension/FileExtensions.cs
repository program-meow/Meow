using System.Threading.Tasks;

namespace Meow.Extension
{
    /// <summary>
    /// 文件扩展
    /// </summary>
    public static class FileExtensions
    {

        #region Write  [将字符串写入文件]

        /// <summary>
        /// 将字符串写入文件
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="filePath">文件绝对路径</param>
        public static void FileWrite(this string content, string filePath)
        {
            Meow.Helper.File.Write(filePath, content);
        }

        /// <summary>
        /// 将字节流写入文件
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="filePath">文件绝对路径</param>
        public static void FileWrite(this byte[] content, string filePath)
        {
            Meow.Helper.File.Write(filePath, content);
        }

        #endregion

        #region WriteAsync  [将字符串写入文件]

        /// <summary>
        /// 将字符串写入文件
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="filePath">文件绝对路径</param>
        public static async Task FileWriteAsync(this string content, string filePath)
        {
            await Meow.Helper.File.WriteAsync(filePath, content);
        }

        /// <summary>
        /// 将字节流写入文件
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="filePath">文件绝对路径</param>
        public static async Task FileWriteAsync(this byte[] content, string filePath)
        {
            await Meow.Helper.File.WriteAsync(filePath, content);
        }

        #endregion

    }
}
