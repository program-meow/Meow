using System.Threading.Tasks;
using System.Threading;
using System;

namespace Meow.Helper
{
    /// <summary>
    /// 重试操作
    /// </summary>
    public static class Retry
    {
        #region TryInvoke  [尝试调用]

        /// <summary>
        /// 试着调用
        /// </summary>
        /// <param name="action">委托方法</param>
        /// <param name="maxRetryTimes">最大重试次数</param>
        /// <param name="onRetry">重试函数</param>
        /// <param name="delayFunc">延迟函数</param>
        public static bool TryInvoke(System.Action action, int maxRetryTimes = 3, Action<int, TimeSpan, Exception> onRetry = null, Func<int, TimeSpan> delayFunc = null)
        {
            Meow.Helper.Validation.CheckNull(action, nameof(action));
            int time = 0;
            do
            {
                try
                {
                    action();
                    return true;
                }
                catch (Exception ex)
                {
                    time++;
                    TimeSpan? delay = delayFunc?.Invoke(time);
                    onRetry?.Invoke(time, delay.GetValueOrDefault(), ex);
                    if (delay.HasValue)
                        System.Threading.Thread.Sleep(delay.Value);
                }
            } while (time <= maxRetryTimes);
            return false;
        }

        /// <summary>
        /// 试着调用
        /// </summary>
        /// <param name="func">委托方法</param>
        /// <param name="maxRetryTimes">最大重试次数</param>
        /// <param name="onRetry">重试函数</param>
        /// <param name="delayFunc">延迟函数</param>
        public static bool TryInvoke(Func<bool> func, int maxRetryTimes = 3, Action<int, TimeSpan, Exception> onRetry = null, Func<int, TimeSpan> delayFunc = null)
        {
            Meow.Helper.Validation.CheckNull(func, nameof(func));

            bool result = false;
            int time = 0;
            Exception exception = default(Exception);
            do
            {
                if (time > 0)
                {
                    TimeSpan? delay = delayFunc?.Invoke(time);
                    onRetry?.Invoke(time, delay.GetValueOrDefault(), exception);
                    if (delay.HasValue)
                        Task.Delay(delay.Value).Wait();
                }
                try
                {
                    result = func();
                    exception = null;
                }
                catch (Exception ex)
                {
                    exception = ex;
                    Meow.Helper.Log.Create(func.GetType()).LogError().Exception(ex);
                }

                time++;
            } while (!result && time <= maxRetryTimes);
            return result;
        }

        /// <summary>
        /// 试着调用
        /// </summary>
        /// <param name="func">委托方法</param>
        /// <param name="maxRetryTimes">最大重试次数</param>
        /// <param name="validFunc">有效函数</param>
        /// <param name="delayFunc">延迟函数</param>
        public static TResult TryInvoke<TResult>(Func<TResult> func, Func<TResult, bool> validFunc, int maxRetryTimes = 3, Func<int, TimeSpan> delayFunc = null)
        {
            TResult result = default(TResult);
            int time = 0;
            do
            {
                if (time > 0 && delayFunc != null)
                    Task.Delay(delayFunc.Invoke(time)).Wait();
                try
                {
                    result = func();
                }
                catch (Exception ex)
                {
                    Meow.Helper.Log.Create(func.GetType()).LogError().Exception(ex);
                }
                time++;
            } while (!validFunc(result) && time <= maxRetryTimes);
            return result;
        }

        /// <summary>
        /// 试着调用
        /// </summary>
        /// <param name="func">委托方法</param>
        /// <param name="maxRetryTimes">最大重试次数</param>
        /// <param name="t1">第一个参数</param>
        /// <param name="validFunc">有效函数</param>
        /// <param name="delayFunc">延迟函数</param>
        public static TResult TryInvoke<T1, TResult>(Func<T1, TResult> func, T1 t1, Func<TResult, bool> validFunc, int maxRetryTimes = 3, Func<int, TimeSpan> delayFunc = null)
        {
            TResult result = default(TResult);
            int time = 0;
            do
            {
                if (time > 0 && delayFunc != null)
                    Task.Delay(delayFunc.Invoke(time)).Wait();
                try
                {
                    result = func(t1);
                }
                catch (Exception ex)
                {
                    Meow.Helper.Log.Create(func.GetType()).LogError().Exception(ex);
                }
                time++;
            } while (!validFunc(result) && time <= maxRetryTimes);
            return result;
        }

        /// <summary>
        /// 试着调用
        /// </summary>
        /// <param name="func">委托方法</param>
        /// <param name="maxRetryTimes">最大重试次数</param>
        /// <param name="t1">第一个参数</param>
        /// <param name="t2">第二个参数</param>
        /// <param name="validFunc">有效函数</param>
        /// <param name="delayFunc">延迟函数</param>
        public static TResult TryInvoke<T1, T2, TResult>(Func<T1, T2, TResult> func, T1 t1, T2 t2, Func<TResult, bool> validFunc, int maxRetryTimes = 3, Func<int, TimeSpan> delayFunc = null)
        {
            TResult result = default(TResult);
            int time = 0;
            do
            {
                if (time > 0 && delayFunc != null)
                    Task.Delay(delayFunc.Invoke(time)).Wait();
                try
                {
                    result = func(t1, t2);
                }
                catch (Exception ex)
                {
                    Meow.Helper.Log.Create(func.GetType()).LogError().Exception(ex);
                }
                time++;
            } while (!validFunc(result) && time <= maxRetryTimes);
            return result;
        }

        /// <summary>
        /// 试着调用
        /// </summary>
        /// <param name="func">委托方法</param>
        /// <param name="maxRetryTimes">最大重试次数</param>
        /// <param name="t1">第一个参数</param>
        /// <param name="t2">第二个参数</param>
        /// <param name="t3">第三个参数</param>
        /// <param name="validFunc">有效函数</param>
        /// <param name="delayFunc">延迟函数</param>
        public static TResult TryInvoke<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func, T1 t1, T2 t2, T3 t3, Func<TResult, bool> validFunc, int maxRetryTimes = 3, Func<int, TimeSpan> delayFunc = null)
        {
            TResult result = default(TResult);
            int time = 0;
            do
            {
                if (time > 0 && delayFunc != null)
                    Task.Delay(delayFunc.Invoke(time)).Wait();
                try
                {
                    result = func(t1, t2, t3);
                }
                catch (Exception ex)
                {
                    Meow.Helper.Log.Create(func.GetType()).LogError().Exception(ex);
                }
                time++;
            } while (!validFunc(result) && time <= maxRetryTimes);
            return result;
        }

        /// <summary>
        /// 试着调用
        /// </summary>
        /// <param name="func">委托方法</param>
        /// <param name="maxRetryTimes">最大重试次数</param>
        /// <param name="t1">第一个参数</param>
        /// <param name="t2">第二个参数</param>
        /// <param name="t3">第三个参数</param>
        /// <param name="t4">第四个参数</param>
        /// <param name="validFunc">有效函数</param>
        /// <param name="delayFunc">延迟函数</param>
        public static TResult TryInvoke<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func, T1 t1, T2 t2, T3 t3, T4 t4, Func<TResult, bool> validFunc, int maxRetryTimes = 3, Func<int, TimeSpan> delayFunc = null)
        {
            TResult result = default(TResult)!;
            int time = 0;
            do
            {
                if (time > 0 && delayFunc != null)
                    Task.Delay(delayFunc.Invoke(time)).Wait();
                try
                {
                    result = func(t1, t2, t3, t4);
                }
                catch (Exception ex)
                {
                    Meow.Helper.Log.Create(func.GetType()).LogError().Exception(ex);
                }
                time++;
            } while (!validFunc(result) && time <= maxRetryTimes);
            return result;
        }

        #endregion

        #region TryInvokeAsync  [尝试调用]

        /// <summary>
        /// 试着调用
        /// </summary>
        /// <param name="action">委托方法</param>
        /// <param name="maxRetryTimes">最大重试次数</param>
        /// <param name="onRetry">重试函数</param>
        /// <param name="delayFunc">延迟函数</param>
        /// <param name="cancellationToken">取消token</param>
        public static async Task<bool> TryInvokeAsync(Func<Task> action, int maxRetryTimes = 3, Action<int, TimeSpan, Exception> onRetry = null, Func<int, TimeSpan> delayFunc = null, CancellationToken cancellationToken = default)
        {
            Meow.Helper.Validation.CheckNull(action, nameof(action));
            int time = 0;
            do
            {
                try
                {
                    await action();
                    return true;
                }
                catch (Exception ex)
                {
                    time++;
                    TimeSpan? delay = delayFunc?.Invoke(time);
                    onRetry?.Invoke(time, delay.GetValueOrDefault(), ex);
                    if (delay.HasValue)
                        await Task.Delay(delay.Value, cancellationToken);
                }
            } while (time <= maxRetryTimes && !cancellationToken.IsCancellationRequested);
            return false;
        }

        /// <summary>
        /// 试着调用
        /// </summary>
        /// <param name="func">委托方法</param>
        /// <param name="maxRetryTimes">最大重试次数</param>
        /// <param name="onRetry">重试函数</param>
        /// <param name="delayFunc">延迟函数</param>
        /// <param name="cancellationToken">取消token</param>
        public static async Task<bool> TryInvokeAsync(Func<Task<bool>> func, int maxRetryTimes = 3, Action<int, TimeSpan, Exception> onRetry = null, Func<int, TimeSpan> delayFunc = null, CancellationToken cancellationToken = default)
        {
            Meow.Helper.Validation.CheckNull(func, nameof(func));
            bool result = false;
            int time = 0;
            Exception exception = default(Exception);
            do
            {
                if (time > 0)
                {
                    TimeSpan? delay = delayFunc?.Invoke(time);
                    onRetry?.Invoke(time, delay.GetValueOrDefault(), exception);
                    if (delay.HasValue)
                        await Task.Delay(delay.Value, cancellationToken);
                }
                try
                {
                    result = await func();
                    exception = null;
                }
                catch (Exception ex)
                {
                    exception = ex;
                    Meow.Helper.Log.Create(func.GetType()).LogError().Exception(ex);
                }
                time++;
            } while (!result && time <= maxRetryTimes && !cancellationToken.IsCancellationRequested);
            return result;
        }

        /// <summary>
        /// 试着调用
        /// </summary>
        /// <param name="func">委托方法</param>
        /// <param name="maxRetryTimes">最大重试次数</param>
        /// <param name="validFunc">有效函数</param>
        /// <param name="delayFunc">延迟函数</param>
        public static async Task<TResult> TryInvokeAsync<TResult>(Func<Task<TResult>> func, Func<TResult, bool> validFunc, int maxRetryTimes = 3, Func<int, TimeSpan> delayFunc = null)
        {
            TResult result = default(TResult);
            int time = 0;
            do
            {
                if (delayFunc != null && time > 0)
                    await Task.Delay(delayFunc(time));
                try
                {
                    result = await func();
                }
                catch (Exception ex)
                {
                    Meow.Helper.Log.Create(func.GetType()).LogError().Exception(ex);
                }
                time++;
            } while (!validFunc(result) && time <= maxRetryTimes);
            return result;
        }

        /// <summary>
        /// 试着调用
        /// </summary>
        /// <param name="func">委托方法</param>
        /// <param name="maxRetryTimes">最大重试次数</param>
        /// <param name="t1">第一个参数</param>
        /// <param name="validFunc">有效函数</param>
        /// <param name="delayFunc">延迟函数</param>
        public static async Task<TResult> TryInvokeAsync<T1, TResult>(Func<T1, Task<TResult>> func, T1 t1, Func<TResult, bool> validFunc, int maxRetryTimes = 3, Func<int, TimeSpan> delayFunc = null)
        {
            TResult result = default(TResult);
            int time = 0;
            do
            {
                if (delayFunc != null && time > 0)
                    await Task.Delay(delayFunc(time));
                try
                {
                    result = await func(t1);
                }
                catch (Exception ex)
                {
                    Meow.Helper.Log.Create(func.GetType()).LogError().Exception(ex);
                }
                time++;
            } while (!validFunc(result) && time <= maxRetryTimes);
            return result;
        }

        /// <summary>
        /// 试着调用
        /// </summary>
        /// <param name="func">委托方法</param>
        /// <param name="maxRetryTimes">最大重试次数</param>
        /// <param name="t1">第一个参数</param>
        /// <param name="t2">第二个参数</param>
        /// <param name="validFunc">有效函数</param>
        /// <param name="delayFunc">延迟函数</param>
        public static async Task<TResult> TryInvokeAsync<T1, T2, TResult>(Func<T1, T2, Task<TResult>> func, T1 t1, T2 t2, Func<TResult, bool> validFunc, int maxRetryTimes = 3, Func<int, TimeSpan> delayFunc = null)
        {
            TResult result = default(TResult);
            int time = 0;
            do
            {
                if (delayFunc != null && time > 0)
                    await Task.Delay(delayFunc(time));
                try
                {
                    result = await func(t1, t2);
                }
                catch (Exception ex)
                {
                    Meow.Helper.Log.Create(func.GetType()).LogError().Exception(ex);
                }
                time++;
            } while (!validFunc(result) && time <= maxRetryTimes);
            return result;
        }

        /// <summary>
        /// 试着调用
        /// </summary>
        /// <param name="func">委托方法</param>
        /// <param name="maxRetryTimes">最大重试次数</param>
        /// <param name="t1">第一个参数</param>
        /// <param name="t2">第二个参数</param>
        /// <param name="t3">第三个参数</param>
        /// <param name="validFunc">有效函数</param>
        /// <param name="delayFunc">延迟函数</param>
        public static async Task<TResult> TryInvokeAsync<T1, T2, T3, TResult>(Func<T1, T2, T3, Task<TResult>> func, T1 t1, T2 t2, T3 t3, Func<TResult, bool> validFunc, int maxRetryTimes = 3, Func<int, TimeSpan> delayFunc = null)
        {
            TResult result = default(TResult);
            int time = 0;
            do
            {
                if (delayFunc != null && time > 0)
                    await Task.Delay(delayFunc(time));
                try
                {
                    result = await func(t1, t2, t3);
                }
                catch (Exception ex)
                {
                    Meow.Helper.Log.Create(func.GetType()).LogError().Exception(ex);
                }
                time++;
            } while (!validFunc(result) && time <= maxRetryTimes);
            return result;
        }

        /// <summary>
        /// 试着调用
        /// </summary>
        /// <param name="func">委托方法</param>
        /// <param name="maxRetryTimes">最大重试次数</param>
        /// <param name="t1">第一个参数</param>
        /// <param name="t2">第二个参数</param>
        /// <param name="t3">第三个参数</param>
        /// <param name="t4">第四个参数</param>
        /// <param name="validFunc">有效函数</param>
        /// <param name="delayFunc">延迟函数</param>
        public static async Task<TResult> TryInvokeAsync<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, Task<TResult>> func, T1 t1, T2 t2, T3 t3, T4 t4, Func<TResult, bool> validFunc, int maxRetryTimes = 3, Func<int, TimeSpan> delayFunc = null)
        {
            TResult result = default(TResult);
            int time = 0;
            do
            {
                if (delayFunc != null && time > 0)
                    await Task.Delay(delayFunc(time));
                try
                {
                    result = await func(t1, t2, t3, t4);
                }
                catch (Exception ex)
                {
                    Meow.Helper.Log.Create(func.GetType()).LogError().Exception(ex);
                }
                time++;
            } while (!validFunc(result) && time <= maxRetryTimes);
            return result;
        }

        #endregion 
    }
}
