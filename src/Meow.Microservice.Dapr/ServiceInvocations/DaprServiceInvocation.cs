namespace Meow.Microservice.Dapr.ServiceInvocations;

/// <summary>
/// Dapr WebApi服务调用操作
/// </summary>
public class DaprServiceInvocation : DaprServiceInvocationBase<IServiceInvocation>, IServiceInvocation {

    #region 构造方法

    /// <summary>
    /// 初始化Dapr WebApi服务调用操作
    /// </summary>
    /// <param name="client">Dapr客户端</param>
    /// <param name="options">Dapr配置</param>
    /// <param name="loggerFactory">日志工厂</param>
    public DaprServiceInvocation( DaprClient client , IOptions<DaprOptions> options , ILoggerFactory loggerFactory ) : base( client , options , loggerFactory ) {
    }

    #endregion

    #region 事件

    /// <summary>
    /// 调用前事件
    /// </summary>
    protected Func<HttpRequestMessage , bool> OnBeforeAction { get; set; }
    /// <summary>
    /// 响应结果转换事件
    /// </summary>
    protected Func<HttpResponseMessage , JsonSerializerOptions , CancellationToken , Task<object>> OnResultAction { get; set; }
    /// <summary>
    /// 调用后事件
    /// </summary>
    protected Action<HttpResponseMessage> OnAfterAction { get; set; }
    /// <summary>
    /// 调用成功事件
    /// </summary>
    protected Func<HttpRequestMessage , HttpResponseMessage , object , Task> OnSuccessAction { get; set; }
    /// <summary>
    /// 调用失败事件
    /// </summary>
    protected Func<HttpRequestMessage , HttpResponseMessage , System.Exception , Task> OnFailAction { get; set; }
    /// <summary>
    /// 调用未授权事件
    /// </summary>
    protected Func<HttpRequestMessage , HttpResponseMessage , Task> OnUnauthorizedAction { get; set; }
    /// <summary>
    /// 调用完成事件
    /// </summary>
    protected Func<HttpRequestMessage , HttpResponseMessage , Task> OnCompleteAction { get; set; }

    /// <inheritdoc />
    public IServiceInvocation OnBefore( Func<HttpRequestMessage , bool> action ) {
        OnBeforeAction = action;
        return this;
    }

    /// <summary>
    /// 响应结果转换事件
    /// </summary>
    /// <param name="action">响应结果转换操作</param>
    public IServiceInvocation OnResult( Func<HttpResponseMessage , JsonSerializerOptions , CancellationToken , Task<object>> action ) {
        OnResultAction = action;
        return this;
    }

    /// <summary>
    /// 调用后事件
    /// </summary>
    /// <param name="action">调用后处理操作</param>
    public IServiceInvocation OnAfter( Action<HttpResponseMessage> action ) {
        OnAfterAction = action;
        return this;
    }

    /// <inheritdoc />
    public IServiceInvocation OnSuccess<TResponse>( Func<HttpRequestMessage , HttpResponseMessage , TResponse , Task> action ) {
        if( action != null )
            OnSuccessAction = ( request , response , result ) => action( request , response , ( TResponse ) result );
        return this;
    }

    /// <summary>
    /// 调用失败事件
    /// </summary>
    /// <param name="action">调用失败处理操作</param>
    public IServiceInvocation OnFail( Func<HttpRequestMessage , HttpResponseMessage , System.Exception , Task> action ) {
        OnFailAction = action;
        return this;
    }

    /// <summary>
    /// 调用未授权事件
    /// </summary>
    /// <param name="action">调用未授权处理操作</param>
    public IServiceInvocation OnUnauthorized( Func<HttpRequestMessage , HttpResponseMessage , Task> action ) {
        OnUnauthorizedAction = action;
        return this;
    }

    /// <summary>
    /// 调用完成事件
    /// </summary>
    /// <param name="action">调用完成处理操作</param>
    public IServiceInvocation OnComplete( Func<HttpRequestMessage , HttpResponseMessage , Task> action ) {
        OnCompleteAction = action;
        return this;
    }

    #endregion

    #region 方法调用重载 

    /// <inheritdoc />
    public async Task InvokeAsync( string methodName , CancellationToken cancellationToken = default ) {
        await InvokeAsync( methodName , HttpMethod.Get , cancellationToken );
    }

    /// <inheritdoc />
    public async Task InvokeAsync( string methodName , HttpMethod httpMethod , CancellationToken cancellationToken = default ) {
        await InvokeAsync<object , object>( methodName , null , httpMethod , cancellationToken );
    }

    /// <inheritdoc />
    public async Task InvokeAsync( string methodName , object data , CancellationToken cancellationToken = default ) {
        await InvokeAsync( methodName , data , HttpMethod.Post , cancellationToken );
    }

    /// <inheritdoc />
    public async Task InvokeAsync( string methodName , object data , HttpMethod httpMethod , CancellationToken cancellationToken = default ) {
        await InvokeAsync<object , object>( methodName , data , httpMethod , cancellationToken );
    }

    /// <inheritdoc />
    public async Task<TResponse> InvokeAsync<TResponse>( string methodName , CancellationToken cancellationToken = default ) {
        return await InvokeAsync<TResponse>( methodName , HttpMethod.Get , cancellationToken );
    }

    /// <inheritdoc />
    public async Task<TResponse> InvokeAsync<TResponse>( string methodName , HttpMethod httpMethod , CancellationToken cancellationToken = default ) {
        return await InvokeAsync<TResponse>( methodName , null , httpMethod , cancellationToken );
    }

    /// <inheritdoc />
    public async Task<TResponse> InvokeAsync<TResponse>( string methodName , object data , CancellationToken cancellationToken = default ) {
        return await InvokeAsync<TResponse>( methodName , data , HttpMethod.Get , cancellationToken );
    }

    /// <inheritdoc />
    public async Task<TResponse> InvokeAsync<TResponse>( string methodName , object data , HttpMethod httpMethod , CancellationToken cancellationToken = default ) {
        return await InvokeAsync<object , TResponse>( methodName , data , httpMethod , cancellationToken );
    }

    /// <inheritdoc />
    public async Task<TResponse> InvokeAsync<TRequest, TResponse>( string methodName , TRequest data , CancellationToken cancellationToken = default ) {
        return await InvokeAsync<TRequest , TResponse>( methodName , data , HttpMethod.Get , cancellationToken );
    }

    #endregion

    #region InvokeAsync

    /// <inheritdoc />
    public async Task<TResponse> InvokeAsync<TRequest, TResponse>( string methodName , TRequest data , HttpMethod httpMethod , CancellationToken cancellationToken = default ) {
        Log.LogTrace( "准备调用服务方法,AppId:{AppId},MethodName:{MethodName}" , AppId , methodName );
        HttpRequestMessage request = CreateRequest( methodName , data , httpMethod );
        request = FilterRequest( request );
        if( await InvokeBefore( methodName , data , httpMethod , request ) == false )
            return default;
        HttpResponseMessage response = null;
        try {
            response = await Client.InvokeMethodWithResponseAsync( request , cancellationToken );
            response = FilterResponse( response );
            OnAfterAction?.Invoke( response );
            ServiceResult<TResponse> result = await ToResult<TResponse>( response , cancellationToken );
            await InvokeAfter( methodName , data , httpMethod , request , response , result );
            return result.Data;
        } catch( Warning ) {
            throw;
        } catch( System.Exception exception ) {
            await FailHandlerAsync( methodName , data , httpMethod , request , response , exception , null );
        } finally {
            await CompleteHandlerAsync( methodName , data , httpMethod , request , response );
        }
        return default;
    }

    #endregion

    #region CreateRequest

    /// <summary>
    /// 创建请求消息
    /// </summary>
    protected virtual HttpRequestMessage CreateRequest<TRequest>( string methodName , TRequest data , HttpMethod httpMethod ) {
        HttpRequestMessage result = CreateInvokeMethodRequest( methodName , data , httpMethod );
        AddHeaders( result , methodName );
        return result;
    }

    /// <summary>
    /// 创建请求消息
    /// </summary>
    private HttpRequestMessage CreateInvokeMethodRequest<TRequest>( string methodName , TRequest data , HttpMethod httpMethod ) {
        methodName = GetMethodName( methodName );
        methodName = GetMethodNameWithQueryString( methodName , data , httpMethod );
        if( data == null || httpMethod == HttpMethod.Get )
            return Client.CreateInvokeMethodRequest( httpMethod , AppId , methodName );
        return Client.CreateInvokeMethodRequest( httpMethod , AppId , methodName , data );
    }

    /// <summary>
    /// 获取方法名
    /// </summary>
    /// <param name="methodName">方法名</param>
    protected virtual string GetMethodName( string methodName ) {
        if( methodName.IsEmpty() )
            return string.Empty;
        if( methodName.StartsWith( "/" ) )
            return methodName;
        return $"/api/{methodName}";
    }

    /// <summary>
    /// 获取带查询字符串的方法名
    /// </summary>
    protected virtual string GetMethodNameWithQueryString<TRequest>( string methodName , TRequest data , HttpMethod httpMethod ) {
        if( httpMethod != HttpMethod.Get )
            return methodName;
        if( data == null )
            return methodName;
        return QueryHelpers.AddQueryString( methodName , ToDictionary( data ) );
    }

    /// <summary>
    /// 对象转换为字典
    /// </summary>
    /// <param name="data">对象</param>
    protected IDictionary<string , string> ToDictionary( object data ) {
        IDictionary<string , object> result = Meow.Helper.Convert.ToDictionary( data );
        return result.Where( t => t.Value != null ).ToDictionary( t => t.Key , t => t.Value.SafeString() );
    }

    /// <summary>
    /// 添加请求头
    /// </summary>
    protected virtual void AddHeaders( HttpRequestMessage message , string methodName ) {
        RemoveHeaders();
        IDictionary<string , StringValues> headers = GetHeaders();
        foreach( string key in headers.Keys ) {
            bool? success = message.Headers.TryAddWithoutValidation( key , headers[ key ].ToArray() );
            if( success.SafeValue() )
                continue;
            message.Content ??= new StringContent( string.Empty );
            success = message.Content.Headers.TryAddWithoutValidation( key , headers[ key ].ToArray() );
            if( success.SafeValue() )
                continue;
            Log.LogWarning( "添加请求头失败,Key:{RequestHeaderKey},AppId:{AppId},MethodName:{MethodName}" , key , AppId , methodName );
        }
    }

    /// <summary>
    /// 移除请求头
    /// </summary>
    private void RemoveHeaders() {
        foreach( string key in RemoveHeaderKeys ) {
            Headers.Remove( key );
            ImportHeaderKeys.Remove( key );
            Options.ServiceInvocation.ImportHeaderKeys.Remove( key );
        }
    }

    /// <summary>
    /// 获取请求头
    /// </summary>
    private IDictionary<string , StringValues> GetHeaders() {
        IDictionary<string , StringValues> result = GetImportHeaders();
        foreach( string key in Headers.Keys ) {
            if( result.ContainsKey( key ) )
                result.Remove( key );
            result.Add( key , Headers[ key ] );
        }
        return result;
    }

    /// <summary>
    /// 获取导入的请求头
    /// </summary>
    private IDictionary<string , StringValues> GetImportHeaders() {
        Dictionary<string , StringValues> result = new Dictionary<string , StringValues>();
        ImportHeaderKeys.AddRange( Options.ServiceInvocation.ImportHeaderKeys );
        if( ImportHeaderKeys.Count == 0 )
            return result;
        IHeaderDictionary headers = Meow.Helper.Web.Request?.Headers;
        if( headers == null )
            return result;
        foreach( string key in ImportHeaderKeys.Distinct() ) {
            if( headers.TryGetValue( key , out StringValues value ) )
                result.Add( key , value );
        }
        return result;
    }

    #endregion

    #region InvokeBefore

    /// <summary>
    /// 调用前操作
    /// </summary>
    protected async Task<bool> InvokeBefore( string methodName , object data , HttpMethod httpMethod , HttpRequestMessage message ) {
        if( Options?.ServiceInvocation?.OnBefore != null )
            await Options.ServiceInvocation.OnBefore( new ServiceInvocationArgument( AppId , methodName , httpMethod , data , message ) );
        if( OnBeforeAction == null )
            return true;
        return OnBeforeAction( message );
    }

    #endregion

    #region FilterRequest

    /// <summary>
    /// 过滤请求
    /// </summary>
    /// <param name="request">请求消息</param>
    protected virtual HttpRequestMessage FilterRequest( HttpRequestMessage request ) {
        RequestContext context = new RequestContext( request , Meow.Helper.Web.HttpContext );
        IList<IRequestFilter> requestFilters = Options?.ServiceInvocation?.RequestFilters;
        if( requestFilters == null || requestFilters.Count == 0 )
            return request;
        foreach( IRequestFilter filter in requestFilters.Where( t => t is { Enabled: true } ).OrderBy( t => t.Order ) )
            filter.Handle( context );
        return context.RequestMessage;
    }

    #endregion

    #region FilterResponse

    /// <summary>
    /// 过滤响应
    /// </summary>
    /// <param name="response">响应消息</param>
    protected virtual HttpResponseMessage FilterResponse( HttpResponseMessage response ) {
        ResponseContext context = new ResponseContext( response , Meow.Helper.Web.HttpContext );
        IList<IResponseFilter> responseFilters = Options?.ServiceInvocation?.ResponseFilters;
        if( responseFilters == null || responseFilters.Count == 0 )
            return response;
        foreach( IResponseFilter filter in responseFilters.Where( t => t is { Enabled: true } ).OrderBy( t => t.Order ) )
            filter.Handle( context );
        return context.ResponseMessage;
    }

    #endregion

    #region ToResult

    /// <summary>
    /// 转换结果
    /// </summary>
    /// <param name="response">响应消息</param>
    /// <param name="cancellationToken">取消令牌</param>
    protected virtual async Task<ServiceResult<TResponse>> ToResult<TResponse>( HttpResponseMessage response , CancellationToken cancellationToken ) {
        if( IsUnpackResult )
            return await ToUnpackResult<TResponse>( response , cancellationToken );
        return await ToNotUnpackResult<TResponse>( response , cancellationToken );
    }

    /// <summary>
    /// 转换结果 - 使用ServiceResult解包
    /// </summary>
    /// <param name="response">响应消息</param>
    /// <param name="cancellationToken">取消令牌</param>
    protected virtual async Task<ServiceResult<TResponse>> ToUnpackResult<TResponse>( HttpResponseMessage response , CancellationToken cancellationToken ) {
        response.EnsureSuccessStatusCode();
        if( OnResultAction == null )
            return await response.Content.ReadFromJsonAsync<ServiceResult<TResponse>>( Client.JsonSerializerOptions , cancellationToken );
        object objResult = await OnResultAction( response , Client.JsonSerializerOptions , cancellationToken );
        ServiceResult<TResponse> result = objResult as ServiceResult<TResponse>;
        result.CheckNull( nameof( result ) );
        return result;
    }

    /// <summary>
    /// 转换结果 - 不解包
    /// </summary>
    /// <param name="response">响应消息</param>
    /// <param name="cancellationToken">取消令牌</param>
    protected virtual async Task<ServiceResult<TResponse>> ToNotUnpackResult<TResponse>( HttpResponseMessage response , CancellationToken cancellationToken ) {
        ServiceResult<TResponse> result = new ServiceResult<TResponse> {
            Code = GetNotUnpackStateCode( response )
        };
        if( result.Code != ResultStatusEnum.Success ) {
            result.Message = await response.Content.ReadAsStringAsync( cancellationToken );
            return result;
        }
        if( OnResultAction == null ) {
            string json = await response.Content.ReadAsStringAsync( cancellationToken );
            if( json.IsEmpty() )
                return result;
            result.Data = Meow.Helper.Json.ToObject<TResponse>( json , Client.JsonSerializerOptions );
            return result;
        }
        object content = await OnResultAction( response , Client.JsonSerializerOptions , cancellationToken );
        result.Data = Meow.Helper.Convert.To<TResponse>( content );
        return result;
    }

    /// <summary>
    /// 获取未解包结果的状态码
    /// </summary>
    protected virtual ResultStatusEnum GetNotUnpackStateCode( HttpResponseMessage response ) {
        if( response.IsSuccessStatusCode )
            return ResultStatusEnum.Success;
        if( response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden )
            return ResultStatusEnum.Unauthorized;
        return ResultStatusEnum.Error;
    }

    #endregion

    #region InvokeAfter

    /// <summary>
    /// 调用方法后操作
    /// </summary>
    protected virtual async Task InvokeAfter<TResponse>( string methodName , object data , HttpMethod httpMethod , HttpRequestMessage request , HttpResponseMessage response , ServiceResult<TResponse> result ) {
        ServiceStateEnum state = ToState( result );
        if( state == ServiceStateEnum.Ok ) {
            await SuccessHandlerAsync( methodName , data , httpMethod , request , response , result.Data );
            return;
        }
        if( state == ServiceStateEnum.Fail ) {
            await FailHandlerAsync( methodName , data , httpMethod , request , response , null , result?.Message );
            return;
        }
        if( state == ServiceStateEnum.Unauthorized ) {
            await UnauthorizedHandlerAsync( methodName , data , httpMethod , request , response );
            return;
        }
    }

    #endregion

    #region ToState

    /// <summary>
    /// 转换为服务状态
    /// </summary>
    protected virtual ServiceStateEnum ToState<TResponse>( ServiceResult<TResponse> result ) {
        if( result == null )
            return ServiceStateEnum.Fail;
        if( OnStateAction != null )
            return OnStateAction( result.Code );
        switch( result.Code ) {
            case ResultStatusEnum.Success:
                return ServiceStateEnum.Ok;
            case ResultStatusEnum.Unauthorized:
                return ServiceStateEnum.Unauthorized;
            default:
                return ServiceStateEnum.Fail;
        }
    }

    #endregion

    #region SuccessHandlerAsync

    /// <summary>
    /// 成功处理操作
    /// </summary>
    protected virtual async Task SuccessHandlerAsync<TResponse>( string methodName , object data , HttpMethod httpMethod , HttpRequestMessage request , HttpResponseMessage response , TResponse result ) {
        Log.LogTrace( "调用服务成功,AppId:{AppId},MethodName:{MethodName}" , AppId , methodName );
        if( Options?.ServiceInvocation?.OnSuccess != null )
            await Options.ServiceInvocation.OnSuccess( new ServiceInvocationArgument( AppId , methodName , httpMethod , data , request , response , result ) );
        if( OnSuccessAction == null )
            return;
        await OnSuccessAction( request , response , result );
    }

    #endregion

    #region FailHandlerAsync

    /// <summary>
    /// 失败处理操作
    /// </summary>
    protected virtual async Task FailHandlerAsync( string methodName , object data , HttpMethod httpMethod , HttpRequestMessage request , HttpResponseMessage response , System.Exception exception , string message ) {
        if( exception == null )
            Log.LogWarning( "调用服务失败,AppId:{AppId},MethodName:{MethodName}" , AppId , methodName );
        else {
            Log.LogError( exception , "调用服务失败,AppId:{AppId},MethodName:{MethodName}" , AppId , methodName );
        }
        if( Options?.ServiceInvocation?.OnFail != null )
            await Options.ServiceInvocation.OnFail( new ServiceInvocationArgument( AppId , methodName , httpMethod , data , request , response , null , exception , message ) );
        if( OnFailAction != null ) {
            await OnFailAction( request , response , exception );
            return;
        }
        if( exception != null )
            throw new InvocationException( AppId , methodName , exception , response );
        throw new Warning( message );
    }

    #endregion

    #region UnauthorizedHandlerAsync

    /// <summary>
    /// 未授权处理操作
    /// </summary>
    protected virtual async Task UnauthorizedHandlerAsync( string methodName , object data , HttpMethod httpMethod , HttpRequestMessage request , HttpResponseMessage response ) {
        Log.LogWarning( "调用未授权的服务,AppId:{AppId},MethodName:{MethodName}" , AppId , methodName );
        if( Options?.ServiceInvocation?.OnUnauthorized != null )
            await Options.ServiceInvocation.OnUnauthorized( new ServiceInvocationArgument( AppId , methodName , httpMethod , data , request , response ) );
        if( OnUnauthorizedAction != null ) {
            await OnUnauthorizedAction( request , response );
            return;
        }
        throw new Warning( "您没有该操作的权限" , code: ResultStatusEnum.Unauthorized.GetValue().SafeString() );
    }

    #endregion

    #region CompleteHandlerAsync

    /// <summary>
    /// 调用完成操作
    /// </summary>
    protected virtual async Task CompleteHandlerAsync( string methodName , object data , HttpMethod httpMethod , HttpRequestMessage request , HttpResponseMessage response ) {
        Log.LogTrace( "调用服务完成,AppId:{AppId},MethodName:{MethodName}" , AppId , methodName );
        if( Options?.ServiceInvocation?.OnComplete != null )
            await Options.ServiceInvocation.OnComplete( new ServiceInvocationArgument( AppId , methodName , httpMethod , data , request , response ) );
        if( OnCompleteAction == null )
            return;
        await OnCompleteAction( request , response );
    }

    #endregion
}