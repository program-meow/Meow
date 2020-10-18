using System.Security.Cryptography;
using System.Text;
using Meow.Extension.Helper;

namespace Meow.Mathematics
{
    /// <summary>
    /// 安全散列算法
    /// </summary>
    public static class Sha
    {
        /// <summary>
        /// SHA1算法加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">编码：默认UTF8</param>
        public static string Sha1(string value, Encoding encoding = null)
        {
            if (value.IsEmpty())
                return string.Empty;
            var sha1 = new SHA1Managed();
            var hash = sha1.ComputeHash(value.ToBytes(encoding));
            return hash.GetString();
        }

        /// <summary>
        /// SHA256算法加密
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="encoding">编码：默认UTF8</param>
        public static string Sha256(string value, Encoding encoding = null)
        {
            if (value.IsEmpty())
                return string.Empty;
            var sha256 = new SHA256Managed();
            var hash = sha256.ComputeHash(value.ToBytes(encoding));
            return hash.GetString();
        }
    }
}