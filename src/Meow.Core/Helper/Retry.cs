using Meow.Extension;
using Meow.Response;

namespace Meow.Helper;

/// <summary>
/// 重试操作
/// </summary>
public static class Retry {

    #region 默认配置

    /// <summary>
    /// 校验bool值结果默认方法
    /// </summary>
    private static Func<bool? , bool> _validateBoolDefaultFunc() => ( ( result ) => result.SafeValue() );
    /// <summary>
    /// 校验结果默认方法
    /// </summary>
    private static Func<TResult , bool> _validateDefaultFunc<TResult>() => ( ( result ) => result != null );
    /// <summary>
    /// 设置延迟时间默认方法
    /// </summary>
    private static Func<int , TimeSpan> _delayDefaultAction() => ( ( time ) => new TimeSpan( 0 , 0 , 0 , 0 , 1000 ) );

    #endregion

    #region TryInvoke  [尝试调用]

    /// <summary>
    /// 试着调用
    /// </summary>
    /// <param name="action">委托方法</param>
    /// <param name="maxRetryTimes">最大重试次数</param>
    /// <param name="listenerExceptionFunc">监听异常方法</param>
    /// <param name="delayFunc">设置延迟时间方法</param>
    public static Result<bool> TryInvoke( SystemAction action , int maxRetryTimes = 3 , Action<int , TimeSpan , SystemException> listenerExceptionFunc = null , Func<int , TimeSpan> delayFunc = null ) {
        action.CheckNull( nameof( action ) );

        ResultStatusEnum statusCode;
        SystemException exception;
        delayFunc ??= _delayDefaultAction();

        int time = 0;
        do {
            try {
                action();

                statusCode = ResultStatusEnum.Success;
                exception = null;

                return new Result<bool>( statusCode , statusCode.GetDescription() , true );
            } catch( SystemException ex ) {
                statusCode = ResultStatusEnum.Error;
                exception = ex;

                time++;
                TimeSpan? delay = delayFunc?.Invoke( time );
                listenerExceptionFunc?.Invoke( time , delay.GetValueOrDefault() , ex );
                if( delay.HasValue )
                    System.Threading.Thread.Sleep( delay.Value );
            }
        } while( time <= maxRetryTimes );

        return new Result<bool>( statusCode , exception?.Message , false );
    }

    /// <summary>
    /// 试着调用
    /// </summary>
    /// <param name="func">委托方法</param>
    /// <param name="validateResultFunc">校验结果方法</param>
    /// <param name="maxRetryTimes">最大重试次数</param>
    /// <param name="listenerExceptionFunc">监听异常方法</param>
    /// <param name="delayFunc">设置延迟时间方法</param>
    public static Result<bool?> TryInvoke( Func<bool> func , Func<bool? , bool> validateResultFunc = null , int maxRetryTimes = 3 , Action<int , TimeSpan , SystemException> listenerExceptionFunc = null , Func<int , TimeSpan> delayFunc = null ) {
        func.CheckNull( nameof( func ) );

        ResultStatusEnum statusCode;
        SystemException exception;
        validateResultFunc ??= _validateBoolDefaultFunc();
        delayFunc ??= _delayDefaultAction();

        bool? result;

        int time = 0;
        do {
            try {
                result = func();

                statusCode = ResultStatusEnum.Success;
                exception = null;

                if( validateResultFunc( result ) )
                    return new Result<bool?>( statusCode , statusCode.GetDescription() , result );
            } catch( SystemException ex ) {
                result = null;

                statusCode = ResultStatusEnum.Error;
                exception = ex;

                time++;
                TimeSpan? delay = delayFunc?.Invoke( time );
                listenerExceptionFunc?.Invoke( time , delay.GetValueOrDefault() , ex );
                if( delay.HasValue )
                    System.Threading.Thread.Sleep( delay.Value );
            }
        } while( time <= maxRetryTimes );

        return new Result<bool?>( statusCode , exception?.Message , result );
    }

    /// <summary>
    /// 试着调用
    /// </summary>
    /// <param name="func">委托方法</param>
    /// <param name="validateResultFunc">校验结果方法</param>
    /// <param name="maxRetryTimes">最大重试次数</param>
    /// <param name="listenerExceptionFunc">监听异常方法</param>
    /// <param name="delayFunc">设置延迟时间方法</param>
    public static Result<TResult> TryInvoke<TResult>( Func<TResult> func , Func<TResult , bool> validateResultFunc , int maxRetryTimes = 3 , Action<int , TimeSpan , SystemException> listenerExceptionFunc = null , Func<int , TimeSpan> delayFunc = null ) {
        func.CheckNull( nameof( func ) );

        ResultStatusEnum statusCode;
        SystemException exception;
        validateResultFunc ??= _validateDefaultFunc<TResult>();
        delayFunc ??= _delayDefaultAction();

        TResult result;

        int time = 0;
        do {
            try {
                result = func();

                statusCode = ResultStatusEnum.Success;
                exception = null;

                if( validateResultFunc( result ) )
                    return new Result<TResult>( statusCode , statusCode.GetDescription() , result );
            } catch( SystemException ex ) {
                result = default( TResult );

                statusCode = ResultStatusEnum.Error;
                exception = ex;

                time++;
                TimeSpan? delay = delayFunc?.Invoke( time );
                listenerExceptionFunc?.Invoke( time , delay.GetValueOrDefault() , ex );
                if( delay.HasValue )
                    System.Threading.Thread.Sleep( delay.Value );
            }
        } while( time <= maxRetryTimes );

        return new Result<TResult>( statusCode , exception?.Message , result );
    }

    /// <summary>
    /// 试着调用
    /// </summary>
    /// <param name="func">委托方法</param>
    /// <param name="t1">第一个参数</param>
    /// <param name="validateResultFunc">校验结果方法</param>
    /// <param name="maxRetryTimes">最大重试次数</param>
    /// <param name="listenerExceptionFunc">监听异常方法</param>
    /// <param name="delayFunc">设置延迟时间方法</param>
    public static Result<TResult> TryInvoke<T1, TResult>( Func<T1 , TResult> func , T1 t1 , Func<TResult , bool> validateResultFunc , int maxRetryTimes = 3 , Action<int , TimeSpan , SystemException> listenerExceptionFunc = null , Func<int , TimeSpan> delayFunc = null ) {
        func.CheckNull( nameof( func ) );

        ResultStatusEnum statusCode;
        SystemException exception;
        validateResultFunc ??= _validateDefaultFunc<TResult>();
        delayFunc ??= _delayDefaultAction();

        TResult result;

        int time = 0;
        do {
            try {
                result = func( t1 );

                statusCode = ResultStatusEnum.Success;
                exception = null;

                if( validateResultFunc( result ) )
                    return new Result<TResult>( statusCode , statusCode.GetDescription() , result );
            } catch( SystemException ex ) {
                result = default( TResult );

                statusCode = ResultStatusEnum.Error;
                exception = ex;

                time++;
                TimeSpan? delay = delayFunc?.Invoke( time );
                listenerExceptionFunc?.Invoke( time , delay.GetValueOrDefault() , ex );
                if( delay.HasValue )
                    System.Threading.Thread.Sleep( delay.Value );
            }
        } while( time <= maxRetryTimes );

        return new Result<TResult>( statusCode , exception?.Message , result );
    }

    /// <summary>
    /// 试着调用
    /// </summary>
    /// <param name="func">委托方法</param>
    /// <param name="t1">第一个参数</param>
    /// <param name="t2">第二个参数</param>
    /// <param name="validateResultFunc">校验结果方法</param>
    /// <param name="maxRetryTimes">最大重试次数</param>
    /// <param name="listenerExceptionFunc">监听异常方法</param>
    /// <param name="delayFunc">设置延迟时间方法</param>
    public static Result<TResult> TryInvoke<T1, T2, TResult>( Func<T1 , T2 , TResult> func , T1 t1 , T2 t2 , Func<TResult , bool> validateResultFunc , int maxRetryTimes = 3 , Action<int , TimeSpan , SystemException> listenerExceptionFunc = null , Func<int , TimeSpan> delayFunc = null ) {
        func.CheckNull( nameof( func ) );

        ResultStatusEnum statusCode;
        SystemException exception;
        validateResultFunc ??= _validateDefaultFunc<TResult>();
        delayFunc ??= _delayDefaultAction();

        TResult result;

        int time = 0;
        do {
            try {
                result = func( t1 , t2 );

                statusCode = ResultStatusEnum.Success;
                exception = null;

                if( validateResultFunc( result ) )
                    return new Result<TResult>( statusCode , statusCode.GetDescription() , result );
            } catch( SystemException ex ) {
                result = default( TResult );

                statusCode = ResultStatusEnum.Error;
                exception = ex;

                time++;
                TimeSpan? delay = delayFunc?.Invoke( time );
                listenerExceptionFunc?.Invoke( time , delay.GetValueOrDefault() , ex );
                if( delay.HasValue )
                    System.Threading.Thread.Sleep( delay.Value );
            }
        } while( time <= maxRetryTimes );

        return new Result<TResult>( statusCode , exception?.Message , result );
    }

    /// <summary>
    /// 试着调用
    /// </summary>
    /// <param name="func">委托方法</param>
    /// <param name="t1">第一个参数</param>
    /// <param name="t2">第二个参数</param>
    /// <param name="t3">第三个参数</param>
    /// <param name="validateResultFunc">校验结果方法</param>
    /// <param name="maxRetryTimes">最大重试次数</param>
    /// <param name="listenerExceptionFunc">监听异常方法</param>
    /// <param name="delayFunc">设置延迟时间方法</param>
    public static Result<TResult> TryInvoke<T1, T2, T3, TResult>( Func<T1 , T2 , T3 , TResult> func , T1 t1 , T2 t2 , T3 t3 , Func<TResult , bool> validateResultFunc , int maxRetryTimes = 3 , Action<int , TimeSpan , SystemException> listenerExceptionFunc = null , Func<int , TimeSpan> delayFunc = null ) {
        func.CheckNull( nameof( func ) );

        ResultStatusEnum statusCode;
        SystemException exception;
        validateResultFunc ??= _validateDefaultFunc<TResult>();
        delayFunc ??= _delayDefaultAction();

        TResult result;

        int time = 0;
        do {
            try {
                result = func( t1 , t2 , t3 );

                statusCode = ResultStatusEnum.Success;
                exception = null;

                if( validateResultFunc( result ) )
                    return new Result<TResult>( statusCode , statusCode.GetDescription() , result );
            } catch( SystemException ex ) {
                result = default( TResult );

                statusCode = ResultStatusEnum.Error;
                exception = ex;

                time++;
                TimeSpan? delay = delayFunc?.Invoke( time );
                listenerExceptionFunc?.Invoke( time , delay.GetValueOrDefault() , ex );
                if( delay.HasValue )
                    System.Threading.Thread.Sleep( delay.Value );
            }
        } while( time <= maxRetryTimes );

        return new Result<TResult>( statusCode , exception?.Message , result );
    }

    /// <summary>
    /// 试着调用
    /// </summary>
    /// <param name="func">委托方法</param>
    /// <param name="t1">第一个参数</param>
    /// <param name="t2">第二个参数</param>
    /// <param name="t3">第三个参数</param>
    /// <param name="t4">第四个参数</param>
    /// <param name="validateResultFunc">校验结果方法</param>
    /// <param name="maxRetryTimes">最大重试次数</param>
    /// <param name="listenerExceptionFunc">监听异常方法</param>
    /// <param name="delayFunc">设置延迟时间方法</param>
    public static Result<TResult> TryInvoke<T1, T2, T3, T4, TResult>( Func<T1 , T2 , T3 , T4 , TResult> func , T1 t1 , T2 t2 , T3 t3 , T4 t4 , Func<TResult , bool> validateResultFunc , int maxRetryTimes = 3 , Action<int , TimeSpan , SystemException> listenerExceptionFunc = null , Func<int , TimeSpan> delayFunc = null ) {
        func.CheckNull( nameof( func ) );

        ResultStatusEnum statusCode;
        SystemException exception;
        validateResultFunc ??= _validateDefaultFunc<TResult>();
        delayFunc ??= _delayDefaultAction();

        TResult result;

        int time = 0;
        do {
            try {
                result = func( t1 , t2 , t3 , t4 );

                statusCode = ResultStatusEnum.Success;
                exception = null;

                if( validateResultFunc( result ) )
                    return new Result<TResult>( statusCode , statusCode.GetDescription() , result );
            } catch( SystemException ex ) {
                result = default( TResult );

                statusCode = ResultStatusEnum.Error;
                exception = ex;

                time++;
                TimeSpan? delay = delayFunc?.Invoke( time );
                listenerExceptionFunc?.Invoke( time , delay.GetValueOrDefault() , ex );
                if( delay.HasValue )
                    System.Threading.Thread.Sleep( delay.Value );
            }
        } while( time <= maxRetryTimes );

        return new Result<TResult>( statusCode , exception?.Message , result );
    }

    #endregion

    #region TryInvokeAsync  [尝试调用]

    /// <summary>
    /// 试着调用
    /// </summary>
    /// <param name="action">委托方法</param>
    /// <param name="maxRetryTimes">最大重试次数</param>
    /// <param name="listenerExceptionFunc">监听异常方法</param>
    /// <param name="delayFunc">设置延迟时间方法</param>
    public static async Task<Result<bool>> TryInvokeAsync( Func<Task> action , int maxRetryTimes = 3 , Action<int , TimeSpan , SystemException> listenerExceptionFunc = null , Func<int , TimeSpan> delayFunc = null ) {
        action.CheckNull( nameof( action ) );

        ResultStatusEnum statusCode;
        SystemException exception;
        delayFunc ??= _delayDefaultAction();

        int time = 0;
        do {
            try {
                await action();

                statusCode = ResultStatusEnum.Success;
                exception = null;

                return new Result<bool>( statusCode , statusCode.GetDescription() , true );
            } catch( SystemException ex ) {
                statusCode = ResultStatusEnum.Error;
                exception = ex;

                time++;
                TimeSpan? delay = delayFunc?.Invoke( time );
                listenerExceptionFunc?.Invoke( time , delay.GetValueOrDefault() , ex );
                if( delay.HasValue )
                    System.Threading.Thread.Sleep( delay.Value );
            }
        } while( time <= maxRetryTimes );

        return new Result<bool>( statusCode , exception?.Message , false );
    }

    /// <summary>
    /// 试着调用
    /// </summary>
    /// <param name="func">委托方法</param>
    /// <param name="validateResultFunc">校验结果方法</param>
    /// <param name="maxRetryTimes">最大重试次数</param>
    /// <param name="listenerExceptionFunc">监听异常方法</param>
    /// <param name="delayFunc">设置延迟时间方法</param>
    public static async Task<Result<bool?>> TryInvokeAsync( Func<Task<bool>> func , Func<bool? , bool> validateResultFunc = null , int maxRetryTimes = 3 , Action<int , TimeSpan , SystemException> listenerExceptionFunc = null , Func<int , TimeSpan> delayFunc = null ) {
        func.CheckNull( nameof( func ) );

        ResultStatusEnum statusCode;
        SystemException exception;
        validateResultFunc ??= _validateBoolDefaultFunc();
        delayFunc ??= _delayDefaultAction();

        bool? result;

        int time = 0;
        do {
            try {
                result = await func();

                statusCode = ResultStatusEnum.Success;
                exception = null;

                if( validateResultFunc( result ) )
                    return new Result<bool?>( statusCode , statusCode.GetDescription() , result );
            } catch( SystemException ex ) {
                result = null;

                statusCode = ResultStatusEnum.Error;
                exception = ex;

                time++;
                TimeSpan? delay = delayFunc?.Invoke( time );
                listenerExceptionFunc?.Invoke( time , delay.GetValueOrDefault() , ex );
                if( delay.HasValue )
                    System.Threading.Thread.Sleep( delay.Value );
            }
        } while( time <= maxRetryTimes );

        return new Result<bool?>( statusCode , exception?.Message , result );
    }

    /// <summary>
    /// 试着调用
    /// </summary>
    /// <param name="func">委托方法</param>
    /// <param name="validateResultFunc">校验结果方法</param>
    /// <param name="maxRetryTimes">最大重试次数</param>
    /// <param name="listenerExceptionFunc">监听异常方法</param>
    /// <param name="delayFunc">设置延迟时间方法</param>
    public static async Task<Result<TResult>> TryInvokeAsync<TResult>( Func<Task<TResult>> func , Func<TResult , bool> validateResultFunc , int maxRetryTimes = 3 , Action<int , TimeSpan , SystemException> listenerExceptionFunc = null , Func<int , TimeSpan> delayFunc = null ) {
        func.CheckNull( nameof( func ) );

        ResultStatusEnum statusCode;
        SystemException exception;
        validateResultFunc ??= _validateDefaultFunc<TResult>();
        delayFunc ??= _delayDefaultAction();

        TResult result;

        int time = 0;
        do {
            try {
                result = await func();

                statusCode = ResultStatusEnum.Success;
                exception = null;

                if( validateResultFunc( result ) )
                    return new Result<TResult>( statusCode , statusCode.GetDescription() , result );
            } catch( SystemException ex ) {
                result = default( TResult );

                statusCode = ResultStatusEnum.Error;
                exception = ex;

                time++;
                TimeSpan? delay = delayFunc?.Invoke( time );
                listenerExceptionFunc?.Invoke( time , delay.GetValueOrDefault() , ex );
                if( delay.HasValue )
                    System.Threading.Thread.Sleep( delay.Value );
            }
        } while( time <= maxRetryTimes );

        return new Result<TResult>( statusCode , exception?.Message , result );
    }

    /// <summary>
    /// 试着调用
    /// </summary>
    /// <param name="func">委托方法</param>
    /// <param name="t1">第一个参数</param>
    /// <param name="validateResultFunc">校验结果方法</param>
    /// <param name="maxRetryTimes">最大重试次数</param>
    /// <param name="listenerExceptionFunc">监听异常方法</param>
    /// <param name="delayFunc">设置延迟时间方法</param>
    public static async Task<Result<TResult>> TryInvokeAsync<T1, TResult>( Func<T1 , Task<TResult>> func , T1 t1 , Func<TResult , bool> validateResultFunc , int maxRetryTimes = 3 , Action<int , TimeSpan , SystemException> listenerExceptionFunc = null , Func<int , TimeSpan> delayFunc = null ) {
        func.CheckNull( nameof( func ) );

        ResultStatusEnum statusCode;
        SystemException exception;
        validateResultFunc ??= _validateDefaultFunc<TResult>();
        delayFunc ??= _delayDefaultAction();

        TResult result;

        int time = 0;
        do {
            try {
                result = await func( t1 );

                statusCode = ResultStatusEnum.Success;
                exception = null;

                if( validateResultFunc( result ) )
                    return new Result<TResult>( statusCode , statusCode.GetDescription() , result );
            } catch( SystemException ex ) {
                result = default( TResult );

                statusCode = ResultStatusEnum.Error;
                exception = ex;

                time++;
                TimeSpan? delay = delayFunc?.Invoke( time );
                listenerExceptionFunc?.Invoke( time , delay.GetValueOrDefault() , ex );
                if( delay.HasValue )
                    System.Threading.Thread.Sleep( delay.Value );
            }
        } while( time <= maxRetryTimes );

        return new Result<TResult>( statusCode , exception?.Message , result );
    }

    /// <summary>
    /// 试着调用
    /// </summary>
    /// <param name="func">委托方法</param>
    /// <param name="t1">第一个参数</param>
    /// <param name="t2">第二个参数</param>
    /// <param name="validateResultFunc">校验结果方法</param>
    /// <param name="maxRetryTimes">最大重试次数</param>
    /// <param name="listenerExceptionFunc">监听异常方法</param>
    /// <param name="delayFunc">设置延迟时间方法</param>
    public static async Task<Result<TResult>> TryInvokeAsync<T1, T2, TResult>( Func<T1 , T2 , Task<TResult>> func , T1 t1 , T2 t2 , Func<TResult , bool> validateResultFunc , int maxRetryTimes = 3 , Action<int , TimeSpan , SystemException> listenerExceptionFunc = null , Func<int , TimeSpan> delayFunc = null ) {
        func.CheckNull( nameof( func ) );

        ResultStatusEnum statusCode;
        SystemException exception;
        validateResultFunc ??= _validateDefaultFunc<TResult>();
        delayFunc ??= _delayDefaultAction();

        TResult result;

        int time = 0;
        do {
            try {
                result = await func( t1 , t2 );

                statusCode = ResultStatusEnum.Success;
                exception = null;

                if( validateResultFunc( result ) )
                    return new Result<TResult>( statusCode , statusCode.GetDescription() , result );
            } catch( SystemException ex ) {
                result = default( TResult );

                statusCode = ResultStatusEnum.Error;
                exception = ex;

                time++;
                TimeSpan? delay = delayFunc?.Invoke( time );
                listenerExceptionFunc?.Invoke( time , delay.GetValueOrDefault() , ex );
                if( delay.HasValue )
                    System.Threading.Thread.Sleep( delay.Value );
            }
        } while( time <= maxRetryTimes );

        return new Result<TResult>( statusCode , exception?.Message , result );
    }

    /// <summary>
    /// 试着调用
    /// </summary>
    /// <param name="func">委托方法</param>
    /// <param name="t1">第一个参数</param>
    /// <param name="t2">第二个参数</param>
    /// <param name="t3">第三个参数</param>
    /// <param name="validateResultFunc">校验结果方法</param>
    /// <param name="maxRetryTimes">最大重试次数</param>
    /// <param name="listenerExceptionFunc">监听异常方法</param>
    /// <param name="delayFunc">设置延迟时间方法</param>
    public static async Task<Result<TResult>> TryInvokeAsync<T1, T2, T3, TResult>( Func<T1 , T2 , T3 , Task<TResult>> func , T1 t1 , T2 t2 , T3 t3 , Func<TResult , bool> validateResultFunc , int maxRetryTimes = 3 , Action<int , TimeSpan , SystemException> listenerExceptionFunc = null , Func<int , TimeSpan> delayFunc = null ) {
        func.CheckNull( nameof( func ) );

        ResultStatusEnum statusCode;
        SystemException exception;
        validateResultFunc ??= _validateDefaultFunc<TResult>();
        delayFunc ??= _delayDefaultAction();

        TResult result;

        int time = 0;
        do {
            try {
                result = await func( t1 , t2 , t3 );

                statusCode = ResultStatusEnum.Success;
                exception = null;

                if( validateResultFunc( result ) )
                    return new Result<TResult>( statusCode , statusCode.GetDescription() , result );
            } catch( SystemException ex ) {
                result = default( TResult );

                statusCode = ResultStatusEnum.Error;
                exception = ex;

                time++;
                TimeSpan? delay = delayFunc?.Invoke( time );
                listenerExceptionFunc?.Invoke( time , delay.GetValueOrDefault() , ex );
                if( delay.HasValue )
                    System.Threading.Thread.Sleep( delay.Value );
            }
        } while( time <= maxRetryTimes );

        return new Result<TResult>( statusCode , exception?.Message , result );
    }

    /// <summary>
    /// 试着调用
    /// </summary>
    /// <param name="func">委托方法</param>
    /// <param name="t1">第一个参数</param>
    /// <param name="t2">第二个参数</param>
    /// <param name="t3">第三个参数</param>
    /// <param name="t4">第四个参数</param>
    /// <param name="validateResultFunc">校验结果方法</param>
    /// <param name="maxRetryTimes">最大重试次数</param>
    /// <param name="listenerExceptionFunc">监听异常方法</param>
    /// <param name="delayFunc">设置延迟时间方法</param>
    public static async Task<Result<TResult>> TryInvokeAsync<T1, T2, T3, T4, TResult>( Func<T1 , T2 , T3 , T4 , Task<TResult>> func , T1 t1 , T2 t2 , T3 t3 , T4 t4 , Func<TResult , bool> validateResultFunc , int maxRetryTimes = 3 , Action<int , TimeSpan , SystemException> listenerExceptionFunc = null , Func<int , TimeSpan> delayFunc = null ) {
        func.CheckNull( nameof( func ) );

        ResultStatusEnum statusCode;
        SystemException exception;
        validateResultFunc ??= _validateDefaultFunc<TResult>();
        delayFunc ??= _delayDefaultAction();

        TResult result;

        int time = 0;
        do {
            try {
                result = await func( t1 , t2 , t3 , t4 );

                statusCode = ResultStatusEnum.Success;
                exception = null;

                if( validateResultFunc( result ) )
                    return new Result<TResult>( statusCode , statusCode.GetDescription() , result );
            } catch( SystemException ex ) {
                result = default( TResult );

                statusCode = ResultStatusEnum.Error;
                exception = ex;

                time++;
                TimeSpan? delay = delayFunc?.Invoke( time );
                listenerExceptionFunc?.Invoke( time , delay.GetValueOrDefault() , ex );
                if( delay.HasValue )
                    System.Threading.Thread.Sleep( delay.Value );
            }
        } while( time <= maxRetryTimes );

        return new Result<TResult>( statusCode , exception?.Message , result );
    }

    #endregion 
}