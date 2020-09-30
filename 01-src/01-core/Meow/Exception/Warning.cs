using System;
using System.Collections.Generic;
using System.Text;
using Meow.Extension.Helper;
using Microsoft.Extensions.Logging;

namespace Meow.Exception
{
    /// <summary>
    /// 应用程序异常
    /// </summary>
    public class Warning : System.Exception
    {
        /// <summary>
        /// 初始化应用程序异常
        /// </summary>
        /// <param name="message">错误消息</param>
        public Warning(string message)
            : this(message, null)
        {
        }

        /// <summary>
        /// 初始化应用程序异常
        /// </summary>
        /// <param name="exception">异常</param>
        public Warning(System.Exception exception)
            : this(null, null, exception)
        {
        }

        /// <summary>
        /// 初始化应用程序异常
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="code">错误码</param>
        public Warning(string message, string code)
            : this(message, code, null)
        {
        }

        /// <summary>
        /// 初始化应用程序异常
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="code">错误码</param>
        /// <param name="exception">异常</param>
        public Warning(string message, string code, System.Exception exception)
            : base(message ?? "", exception)
        {
            Code = code;
        }

        /// <summary>
        /// 错误码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 获取错误消息
        /// </summary>
        public string GetMessage()
        {
            return GetMessage(this);
        }

        /// <summary>
        /// 获取错误消息
        /// </summary>
        public static string GetMessage(System.Exception ex)
        {
            var result = new StringBuilder();
            var list = GetExceptions(ex);
            foreach (var exception in list)
                AppendMessage(result, exception);
            return result.ToString().RemoveEnd(Environment.NewLine);
        }

        /// <summary>
        /// 添加异常消息
        /// </summary>
        private static void AppendMessage(StringBuilder result, System.Exception exception)
        {
            if (exception == null)
                return;
            result.AppendLine(exception.Message);
        }

        /// <summary>
        /// 获取异常列表
        /// </summary>
        public IList<System.Exception> GetExceptions()
        {
            return GetExceptions(this);
        }

        /// <summary>
        /// 获取异常列表
        /// </summary>
        /// <param name="ex">异常</param>
        public static IList<System.Exception> GetExceptions(System.Exception ex)
        {
            var result = new List<System.Exception>();
            AddException(result, ex);
            return result;
        }

        /// <summary>
        /// 添加内部异常
        /// </summary>
        private static void AddException(List<System.Exception> result, System.Exception exception)
        {
            if (exception == null)
                return;
            result.Add(exception);
            AddException(result, exception.InnerException);
        }

        /// <summary>
        /// 获取友情提示
        /// </summary>
        /// <param name="level">日志级别</param>
        public string GetPrompt(LogLevel level)
        {
            if (level == LogLevel.Error)
                return Meow.Parameter.Message.System.Error;
            return Message;
        }
    }
}