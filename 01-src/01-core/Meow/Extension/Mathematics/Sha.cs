using System.Text;

namespace Meow.Extension.Mathematics
{
    /// <summary>
    /// 安全散列算法扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// SHA1算法加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">编码：默认UTF8</param>
        public static string Sha1(this string value, Encoding encoding = null)
        {
            return Meow.Mathematics.Sha.Sha1(value, encoding);
        }

        /// <summary>
        /// SHA256算法加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">编码：默认UTF8</param>
        public static string Sha256(this string value, Encoding encoding = null)
        {
            return Meow.Mathematics.Sha.Sha256(value, encoding);
        }
    }
}