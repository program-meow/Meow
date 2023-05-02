using System.Net.Http.Headers;
using Meow.Extension;

namespace Meow.Http.Extension
{
    /// <summary>
    /// Http请求头值集合扩展
    /// </summary>
    public static class HttpHeaderValueCollectionExtensions
    {
        /// <summary>
        /// 添加不为null的值
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="value">值</param>
        public static HttpHeaderValueCollection<T> AddNotNull<T>(this HttpHeaderValueCollection<T> array, T value) where T : class
        {
            if (value == null)
                return array;
            array?.Add(value);
            return array;
        }

        /// <summary>
        /// 添加不为空的值
        /// </summary>
        /// <param name="array">集合</param>
        /// <param name="value">值</param>
        public static HttpHeaderValueCollection<MediaTypeWithQualityHeaderValue> AddNotEmpty(this HttpHeaderValueCollection<MediaTypeWithQualityHeaderValue> array, string value)
        {
            if (value.IsEmpty())
                return array;
            array?.Add(new MediaTypeWithQualityHeaderValue(value));
            return array;
        }

        /// <summary>
        /// 添加不为空的值
        /// </summary>
        /// <param name="array">集合</param>
        /// <param name="value">值</param>
        public static HttpHeaderValueCollection<StringWithQualityHeaderValue> AddNotEmpty(this HttpHeaderValueCollection<StringWithQualityHeaderValue> array, string value)
        {
            if (value.IsEmpty())
                return array;
            array?.Add(new StringWithQualityHeaderValue(value));
            return array;
        }

        /// <summary>
        /// 添加不为空的值
        /// </summary>
        /// <param name="array">集合</param>
        /// <param name="value">值</param>
        public static HttpHeaderValueCollection<string> AddNotEmpty(this HttpHeaderValueCollection<string> array, string value)
        {
            if (value.IsEmpty())
                return array;
            array?.Add(value);
            return array;
        }
    }
}
