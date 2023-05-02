using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using Meow.Extension;

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
            paths = paths.Where(path => !path.IsEmpty()).Select(t => t.Replace(@"\", "/")).ToArray();
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

        /// <summary>
        /// 将给定的查询键和值附加到URI
        /// </summary>
        /// <param name="uri">基本URI</param>
        /// <param name="query">要追加的名称值查询对的集合</param>
        public static string AddQueryString(string uri, IEnumerable<KeyValuePair<string, string>> query)
        {
            return AddQueryString(uri, query.Where(t => !t.Value.IsEmpty()).Select(t => new KeyValuePair<string, object>(t.Key, t.Value)));
        }

        /// <summary>
        /// 将给定的查询键和值附加到URI
        /// </summary>
        /// <param name="uri">基本URI</param>
        /// <param name="query">要追加的名称值查询对的集合</param>
        public static string AddQueryString(string uri, IEnumerable<KeyValuePair<string, object>> query)
        {
            if (uri.IsEmpty())
                throw new ArgumentNullException(nameof(uri));
            if (query.IsEmpty())
                return uri;
            int num = uri.IndexOf('#');
            string str1 = uri;
            string str2 = "";
            if (num != -1)
            {
                str2 = uri.Substring(num);
                str1 = uri.Substring(0, num);
            }
            bool flag = str1.IndexOf('?') != -1;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(str1);
            foreach (KeyValuePair<string, object> keyValuePair in query)
            {
                string value = keyValuePair.Value.SafeString();
                if (value.IsEmpty())
                    continue;
                stringBuilder.Append(flag ? '&' : '?');
                stringBuilder.Append(UrlEncoder.Default.Encode(keyValuePair.Key));
                stringBuilder.Append('=');
                stringBuilder.Append(UrlEncoder.Default.Encode(value));
                flag = true;
            }
            stringBuilder.Append(str2);
            return stringBuilder.ToString();
        }

    }
}
