using Meow.Exception.Prompt;

namespace Meow.Extension.Exception
{
    /// <summary>
    /// 异常扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 获取原始异常
        /// </summary>
        /// <param name="exception">异常</param>
        public static System.Exception GetRawException(this System.Exception exception)
        {
            if (exception == null)
                return null;
            if (exception is AspectCore.DynamicProxy.AspectInvocationException aspectInvocationException)
            {
                if (aspectInvocationException.InnerException == null)
                    return aspectInvocationException;
                return GetRawException(aspectInvocationException.InnerException);
            }
            return exception;
        }

        /// <summary>
        /// 获取异常提示
        /// </summary>
        /// <param name="exception">异常</param>
        public static string GetPrompt(this System.Exception exception)
        {
            return ExceptionPrompt.GetPrompt(exception);
        }
    }
}