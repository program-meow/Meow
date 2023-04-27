using System.IO;
using System.Linq;

namespace Meow.Helper
{
    /// <summary>
    /// Url操作
    /// </summary>
    public static class Url
    {
        /// <summary>
        /// 连接路径
        /// </summary>
        /// <param name="paths">路径列表</param>
        public static string JoinPath(params string[] paths)
        {
            if (paths == null)
                return string.Empty;
            paths = paths.Where(path => Validation.IsEmpty(path) == false).Select(t => t.Replace(@"\", "/")).ToArray();
            if (paths.Length == 0)
                return string.Empty;
            string firstPath = paths.First();
            string lastPath = paths.Last();
            paths = paths.Select(t => t.Trim('/')).ToArray();
            string result = Path.Combine(paths).Replace(@"\", "/");
            if (firstPath.StartsWith('/'))
                result = $"/{result}";
            if (lastPath.EndsWith('/'))
                result = $"{result}/";
            return result;
        }
    }
}
