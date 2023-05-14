using System.Threading.Tasks;

namespace Meow.Extension;

/// <summary>
/// 异步扩展
/// </summary>
public static class AsyncExtension
{

    /// <summary>
    /// 异步执行
    /// </summary>
    /// <typeparam name="TResult">返回对象元素类型</typeparam>
    /// <param name="func">异步方法</param>
    public static TResult RunSync<TResult>(this Task<TResult> func)
    {
        return Meow.Helper.Async.RunSync(func);
    }

    /// <summary>
    /// 异步执行
    /// </summary>
    /// <param name="func">异步方法</param>
    public static void RunSync(this Task func)
    {
        Meow.Helper.Async.RunSync(func);
    }

    /// <summary>
    /// null报错
    /// </summary>
    /// <typeparam name="TResult">返回对象元素类型</typeparam>
    /// <param name="func">异步方法</param>
    /// <param name="message">错误信息</param>
    public static async Task<TResult> WarningByNull<TResult>(this Task<TResult> func, string message)
    {
        return await Meow.Helper.Async.WarningByNull(func, message);
    }
}