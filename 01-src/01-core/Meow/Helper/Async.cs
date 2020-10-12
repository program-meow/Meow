using System;
using System.Threading;
using System.Threading.Tasks;

namespace Meow.Helper
{
    /// <summary>
    /// 异步操作
    /// </summary>
    public static class Async
    {
        /// <summary>
        /// 异步工厂
        /// </summary>
        private static readonly TaskFactory _taskFactory = new
            TaskFactory(CancellationToken.None,
                TaskCreationOptions.None,
                TaskContinuationOptions.None,
                TaskScheduler.Default);

        /// <summary>
        /// 异步执行
        /// </summary>
        /// <typeparam name="TResult">返回对象元素类型</typeparam>
        /// <param name="func">异步方法</param>
        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return Async._taskFactory
                .StartNew<Task<TResult>>(func)
                .Unwrap<TResult>()
                .GetAwaiter()
                .GetResult();
        }

        /// <summary>
        /// 异步执行
        /// </summary>
        /// <param name="func">异步方法</param>
        public static void RunSync(Func<Task> func)
        {
            Async._taskFactory
                .StartNew<Task>(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();
        }
    }
}