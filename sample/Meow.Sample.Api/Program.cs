//为web应用程序和服务初始化一个构建器的新实例
using Meow.Sample.Api;

WebApplicationBuilder builder = WebApplication.CreateBuilder( args );

//配置Meow
builder.AddMeow();

//配置控制器
//将控制器的服务添加到指定的 IServiceCollection ( 指定服务描述符集合的约定 )
//此方法不会注册用于视图或页面的服务
builder.Services.AddControllers();

//配置工作单元
builder.AddIdentityUnitOfWork();


//创建Web应用程序
//构建用于配置HTTP管道和路由的web应用程序。
WebApplication app = builder.Build();

//===== 配置请求管道 =====//

//配置路由
//将 EndpointRoutingMiddleware ( 端点路由中间件 ) 注册到http管道中
//将请求与端点匹配，路由规则 ( 路由的匹配解析功能 )
app.UseRouting();

//配置控制器
//将本程序集定义的所有 Controller 和 Action 转换为一个个的 EndPoint ( 终结点路由 ) 放到路由中间件的配置对象 RouteOptions ( 路由设置 ) 中
//将 EndpointMiddleware ( 端点的中间件 ) 注册到http管道中
app.MapControllers();


#region 废弃

////执行匹配的端点 ( 终结点执行功能 )
//app.UseEndpoints(op =>
//{
//    //将本程序集定义的所有 Controller 和 Action 转换为一个个的 EndPoint ( 终结点路由 ) 放到路由中间件的配置对象 RouteOptions ( 路由设置 ) 中
//    //将 EndpointMiddleware ( 端点的中间件 ) 注册到http管道中
//    op.MapControllers();
//});

#endregion

try {
    //迁移数据
    await app.MigrateAsync();

    //启动应用
    app.Logger.LogInformation( "准备启动应用 ..." );
    await app.RunAsync();
    return 0;
} catch( Exception ex ) {
    app.Logger.LogCritical( ex , "应用启动失败 ..." );
    return 1;
} finally {
    Serilog.Log.CloseAndFlush();
}