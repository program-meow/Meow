using Meow.Logging;

namespace Meow.Helper
{
    /// <summary>
    /// 日志操作
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// 创建日志操作
        /// </summary>
        /// <param name="type">日志类别类型</param>
        public static ILog Create(System.Type type)
        {
            try
            {
                var logFactory = Ioc.Create<ILogFactory>();
                return logFactory.CreateLog(type);
            }
            catch
            {
                return NullLog.Instance;
            }
        }

    }
}
