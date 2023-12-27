namespace Meow.Application.Filter;

/// <summary>
/// 异常处理过滤器
/// </summary>
public class ExceptionHandlerAttribute : ExceptionFilterAttribute {
    /// <summary>
    /// 异常处理
    /// </summary>
    public override void OnException( ExceptionContext context ) {
        context.ExceptionHandled = true;
        string message = context.Exception.GetPrompt( Meow.Helper.Web.Environment.IsProduction() );
        message = GetLocalizedMessages( context , message );
        string errorCode = context.Exception.GetErrorCode() ?? ResultStatusCodeEnum.Error.GetValue().SafeString();
        int httpStatusCode = context.Exception.GetHttpStatusCode() ?? 200;
        context.Result = GetResult( context , errorCode , message , httpStatusCode );
    }

    /// <summary>
    /// 获取本地化异常消息
    /// </summary>
    protected virtual string GetLocalizedMessages( ExceptionContext context , string message ) {
        SystemException exception = context.Exception.GetRawException();
        if( exception is not Warning warning )
            return message;
        if( warning.IsLocalization == false )
            return message;
        if( warning.IsLocalization == null ) {
            IOptions<Meow.Localization.LocalizationOptions> localizationOptions = context.HttpContext.RequestServices.GetService<IOptions<Meow.Localization.LocalizationOptions>>();
            if( localizationOptions.Value.IsLocalizeWarning == false )
                return message;
        }
        IStringLocalizerFactory stringLocalizerFactory = context.HttpContext.RequestServices.GetService<IStringLocalizerFactory>();
        if( stringLocalizerFactory == null )
            return message;
        IStringLocalizer stringLocalizer = stringLocalizerFactory.Create( "Warning" , null );
        LocalizedString localizedString = stringLocalizer[ message ];
        if( localizedString.ResourceNotFound == false )
            return localizedString.Value;
        stringLocalizer = context.HttpContext.RequestServices.GetService<IStringLocalizer>();
        if( stringLocalizer == null )
            return message;
        return stringLocalizer[ message ];
    }

    /// <summary>
    /// 获取结果
    /// </summary>
    protected virtual IActionResult GetResult( ExceptionContext context , string code , string message , int? httpStatusCode ) {
        JsonSerializerOptions options = GetJsonSerializerOptions( context );
        IResultFactory resultFactory = context.HttpContext.RequestServices.GetService<IResultFactory>();
        if( resultFactory == null )
            return new Result( code , message , null , httpStatusCode , options );
        return resultFactory.CreateResult( code , message , null , httpStatusCode , options );
    }

    /// <summary>
    /// 获取Json序列化配置
    /// </summary>
    private JsonSerializerOptions GetJsonSerializerOptions( ExceptionContext context ) {
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
}