using System.Net;
using System.ComponentModel;
using Meow.Extension.Helper;
using Meow.Extension.Validation;

namespace Meow.Parameter.Object
{
    /// <summary>
    /// 返回结果
    /// </summary>
    public class Result<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [DisplayName("是否成功")]
        public bool Success { get; }
        /// <summary>
        /// 状态码
        /// </summary>
        [DisplayName("状态码")]
        public string Code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        [DisplayName("消息")]
        public string Message { get; }
        /// <summary>
        /// 数据
        /// </summary>
        [DisplayName("数据")]
        public T Data { get; }

        /// <summary>
        /// 初始化返回结果
        /// </summary>
        /// <param name="success">是否成功</param>
        /// <param name="code">状态码</param>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        public Result(bool success = true, string code = "", string message = "", T data = default(T))
        {
            Success = success;
            Code = code.IsEmpty() ? success ? HttpStatusCode.OK.Value().SafeString() : HttpStatusCode.InternalServerError.Value().SafeString() : code;
            Message = message.IsEmpty() ? success ? "成功" : "失败" : message;
            Data = data;
        }
    }

    /// <summary>
    /// 返回结果
    /// </summary>
    public class Result : Result<string>
    {
        /// <summary>
        /// 初始化返回结果
        /// </summary>
        /// <param name="success">是否成功</param>
        /// <param name="code">状态码</param>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        public Result(bool success = true, string code = "", string message = "", string data = "") : base(success, code, message, data)
        {
        }
    }
}