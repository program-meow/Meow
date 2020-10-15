using System;
using Meow.Extension.Helper;
using Meow.Helper;
using Meow.Parameter.Response;
using HttpContentTypeEnum = Meow.Parameter.Enum.HttpContentType;

namespace Meow.Extension.Parameter.Response
{
    /// <summary>
    /// HTTP请求响应对象扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 转换委托
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="vale">值</param>
        public delegate T ToHandler<out T>(string vale);

        /// <summary>
        /// 转换为泛型数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="value">值</param>
        public static HttpResponse<T> To<T>(this HttpResponse value)
        {
            if (value.IsNull() || value.Data.IsNull())
                return value.To((T)(object)null);
            if (typeof(T).IsSingleType())
                return value.To(Common.To<T>);
            return value.ContentType switch
            {
                HttpContentTypeEnum.FormData => throw new NotImplementedException("未实现表单数据类型解析"),
                HttpContentTypeEnum.FormFile => throw new NotImplementedException("未实现文件数据类型解析"),
                HttpContentTypeEnum.Json => value.To(Json.ToObject<T>),
                HttpContentTypeEnum.Xml => throw new NotImplementedException("未实现Xml数据类型解析"),
                _ => value.To((T)(object)null)
            };
        }

        /// <summary>
        /// 转换为泛型数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="data">数据</param>
        public static HttpResponse<T> To<T>(this HttpResponse value, T data)
        {
            if (value.IsNull())
                return new HttpResponse<T>();
            return new HttpResponse<T>(value.Code, value.ContentType, data, value.Message);
        }

        /// <summary>
        /// 转换为泛型数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="handler">委托方法</param>
        public static HttpResponse<T> To<T>(this HttpResponse value, ToHandler<T> handler)
        {
            if (value.IsNull())
                return new HttpResponse<T>();
            return new HttpResponse<T>(value.Code, value.ContentType, handler(value.Data), value.Message);
        }
    }
}