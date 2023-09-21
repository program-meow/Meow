using Meow.Json;

namespace Meow.Application.Infrastructure;

/// <summary>
/// Web Api服务注册器
/// </summary>
public class WebApiServiceRegistrar : IServiceRegistrar {
    /// <summary>
    /// 获取服务名
    /// </summary>
    public static string ServiceName => "Meow.Application.Infrastructure.WebApiServiceRegistrar";

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderId => 1210;

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled => ServiceRegistrarConfig.IsEnabled( ServiceName );

    /// <summary>
    /// 注册服务
    /// </summary>
    /// <param name="serviceContext">服务上下文</param>
    public SystemAction Register( ServiceContext serviceContext ) {
        serviceContext.HostBuilder.ConfigureServices( ( context , services ) => {
            services.AddSingleton<ILogContextAccessor , LogContextAccessor>();
            services.AddSingleton<IStartupFilter , LogContextStartupFilter>();
            ConfigApiBehaviorOptions( services );
        } );
        return null;
    }

    /// <summary>
    /// 配置ApiBehavior
    /// </summary>
    private void ConfigApiBehaviorOptions( IServiceCollection services ) {
        services.Configure<ApiBehaviorOptions>( options => {
            options.SuppressModelStateInvalidFilter = false;
            Func<ActionContext , IActionResult> builtInFactory = options.InvalidModelStateResponseFactory;
            options.InvalidModelStateResponseFactory = actionContext => {
                ILogger<WebApiServiceRegistrar> log = actionContext.HttpContext.RequestServices.GetRequiredService<ILogger<WebApiServiceRegistrar>>();
                if( actionContext.ModelState.IsValid )
                    return builtInFactory( actionContext );
                ModelError error = GetModelError( actionContext );
                log.LogError( error.Exception , "Model Binding Failed,ErrorMessage: {ErrorMessage}" , error.ErrorMessage );
                string message = GetLocalizedMessages( actionContext.HttpContext , error.ErrorMessage );
                return GetResult( actionContext.HttpContext , ResultStatusCodeEnum.Error.GetValue().SafeString() , message , 200 );
            };
        } );
    }

    /// <summary>
    /// 获取模型错误
    /// </summary>
    private ModelError GetModelError( ActionContext actionContext ) {
        foreach( KeyValuePair<string , ModelStateEntry> state in actionContext.ModelState ) {
            foreach( ModelError error in state.Value.Errors )
                return error;
        }
        return null;
    }

    /// <summary>
    /// 获取本地化异常消息
    /// </summary>
    protected virtual string GetLocalizedMessages( HttpContext context , string message ) {
        IStringLocalizerFactory stringLocalizerFactory = context.RequestServices.GetService<IStringLocalizerFactory>();
        if( stringLocalizerFactory == null )
            return message;
        AssemblyName assemblyName = new AssemblyName( GetType().Assembly.FullName );
        IStringLocalizer stringLocalizer = stringLocalizerFactory.Create( "Warning" , assemblyName.Name );
        LocalizedString localizedString = stringLocalizer[ message ];
        if( localizedString.ResourceNotFound == false )
            return localizedString.Value;
        stringLocalizer = context.RequestServices.GetService<IStringLocalizer>();
        if( stringLocalizer == null )
            return message;
        return stringLocalizer[ message ];
    }

    /// <summary>
    /// 获取结果
    /// </summary>
    protected virtual IActionResult GetResult( HttpContext context , string code , string message , int? httpStatusCode ) {
        JsonSerializerOptions options = GetJsonSerializerOptions( context );
        IResultFactory resultFactory = context.RequestServices.GetService<IResultFactory>();
        if( resultFactory == null )
            return new Result( code , message , null , httpStatusCode , options );
        return resultFactory.CreateResult( code , message , null , httpStatusCode , options );
    }

    /// <summary>
    /// 获取Json序列化配置
    /// </summary>
    private JsonSerializerOptions GetJsonSerializerOptions( HttpContext context ) {
        IJsonSerializerOptionsFactory factory = context.RequestServices.GetService<IJsonSerializerOptionsFactory>();
        factory.CheckNull( nameof( factory ) );
        return factory.CreateOptions();
    }
}