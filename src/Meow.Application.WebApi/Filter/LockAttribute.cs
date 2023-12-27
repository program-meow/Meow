namespace Meow.Application.Filter;

/// <summary>
/// 请求锁过滤器,用于防止重复提交
/// </summary>
[AttributeUsage( AttributeTargets.Method )]
public class LockAttribute : ActionFilterAttribute {
    /// <summary>
    /// 业务标识
    /// </summary>
    public string Key { get; set; }
    /// <summary>
    /// 业务锁类型
    /// </summary>
    public LockTypeEnum Type { get; set; } = LockTypeEnum.User;
    /// <summary>
    /// 再次提交时间间隔，单位：秒
    /// </summary>
    public int Interval { get; set; }

    /// <summary>
    /// 执行
    /// </summary>
    public override async Task OnActionExecutionAsync( ActionExecutingContext context , ActionExecutionDelegate next ) {
        context.CheckNull( nameof( context ) );
        next.CheckNull( nameof( next ) );
        ILock @lock = CreateLock( context );
        string key = GetKey( context );
        bool isSuccess = false;
        try {
            isSuccess = await @lock.LockAsync( key , GetExpiration() );
            if( isSuccess == false ) {
                context.Result = GetResult( context , ResultStatusCodeEnum.Error.GetValue().SafeString() , GetFailMessage( context ) );
                return;
            }
            OnActionExecuting( context );
            if( context.Result != null )
                return;
            ActionExecutedContext executedContext = await next();
            OnActionExecuted( executedContext );
        } finally {
            if( isSuccess ) {
                await @lock.UnLockAsync();
            }
        }
    }

    /// <summary>
    /// 创建业务锁
    /// </summary>
    protected virtual ILock CreateLock( ActionExecutingContext context ) {
        return context.HttpContext.RequestServices.GetService<ILock>() ?? NullLock.Instance;
    }

    /// <summary>
    /// 获取锁定标识
    /// </summary>
    protected virtual string GetKey( ActionExecutingContext context ) {
        string userId = string.Empty;
        if( Type == LockTypeEnum.User )
            userId = $"{GetUserId( context )}_";
        return string.IsNullOrWhiteSpace( Key ) ? $"{userId}{Meow.Helper.Web.Request.Path}" : $"{userId}{Key}";
    }

    /// <summary>
    /// 获取用户标识
    /// </summary>
    protected string GetUserId( ActionExecutingContext context ) {
        IMeowSession session = context.HttpContext.RequestServices.GetService<IMeowSession>();
        return session?.UserId;
    }

    /// <summary>
    /// 获取到期时间间隔
    /// </summary>
    private TimeSpan? GetExpiration() {
        if( Interval == 0 )
            return null;
        return TimeSpan.FromSeconds( Interval );
    }

    /// <summary>
    /// 获取结果
    /// </summary>
    private IActionResult GetResult( ActionExecutingContext context , string code , string message ) {
        JsonSerializerOptions options = GetJsonSerializerOptions( context );
        IResultFactory resultFactory = context.HttpContext.RequestServices.GetService<IResultFactory>();
        if( resultFactory == null )
            return new Result( code , message , options: options );
        return resultFactory.CreateResult( code , message , null , null , options );
    }

    /// <summary>
    /// 获取Json序列化配置
    /// </summary>
    private JsonSerializerOptions GetJsonSerializerOptions( ActionExecutingContext context ) {
        IJsonSerializerOptionsFactory factory = context.HttpContext.RequestServices.GetService<IJsonSerializerOptionsFactory>();
        if( factory != null )
            return factory.CreateOptions();
        return new JsonSerializerOptions {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase ,
            Encoder = JavaScriptEncoder.Create( UnicodeRanges.All ) ,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull ,
            Converters = {
                new DateTimeJsonConverter(),
                new NullableDateTimeJsonConverter()
            }
        };
    }

    /// <summary>
    /// 获取失败消息
    /// </summary>
    protected virtual string GetFailMessage( ActionExecutingContext context ) {
        if( Type == LockTypeEnum.User )
            return GetLocalizedMessage( context , "请不要重复提交" );
        return GetLocalizedMessage( context , "其他用户正在执行该操作,请稍后再试" );
    }

    /// <summary>
    /// 获取本地化消息
    /// </summary>
    protected virtual string GetLocalizedMessage( ActionExecutingContext context , string message ) {
        IStringLocalizer stringLocalizer = context.HttpContext.RequestServices.GetService<IStringLocalizer>();
        if( stringLocalizer == null )
            return message;
        return stringLocalizer[ message ];
    }
}