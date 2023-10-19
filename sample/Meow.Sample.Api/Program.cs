using Meow.Authentication;
using Meow.Data.EntityFrameworkCore;
using Meow.Data.EntityFrameworkCore.Migration;
using Meow.Extension;
using Meow.Sample.Data;
using Meow.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

//为web应用程序和服务初始化一个构建器的新实例
WebApplicationBuilder builder = WebApplication.CreateBuilder( args );

//将控制器的服务添加到指定的 IServiceCollection ( 指定服务描述符集合的约定 )
//此方法不会注册用于视图或页面的服务
builder.Services.AddControllers();

builder
    .AsBuild()
    //配置数据库连接字符串与工作单元映射
    //SqlServer 数据库
    .AddSqlServerUnitOfWork<ISampleUnitOfWork , Meow.Sample.Data.SqlServer.SampleUnitOfWork>(
        builder.Configuration.GetConnectionString( "SqlServerConnection" ) ,
        condition: builder.Configuration[ "App:DbType" ] == "SqlServer" )
    //PgSql 数据库
    .AddPgSqlUnitOfWork<ISampleUnitOfWork , Meow.Sample.Data.PgSql.SampleUnitOfWork>(
        builder.Configuration.GetConnectionString( "PgSqlConnection" ) ,
        condition: builder.Configuration[ "App:DbType" ] == "PgSql" )
    //Oracle 数据库
    .AddOracleUnitOfWork<ISampleUnitOfWork , Meow.Sample.Data.Oracle.SampleUnitOfWork>(
        builder.Configuration.GetConnectionString( "OracleConnection" ) ,
        condition: builder.Configuration[ "App:DbType" ] == "Oracle" )
    //MySql 数据库
    .AddMySqlUnitOfWork<ISampleUnitOfWork , Meow.Sample.Data.MySql.SampleUnitOfWork>(
        builder.Configuration.GetConnectionString( "MySqlConnection" ) ,
        condition: builder.Configuration[ "App:DbType" ] == "MySql" )
    .AddMeow();


//构建用于配置HTTP管道和路由的web应用程序。
WebApplication app = builder.Build();

//将 EndpointRoutingMiddleware ( 端点路由中间件 ) 注册到http管道中
//将请求与端点匹配，路由规则 ( 路由的匹配解析功能 )
app.UseRouting();


//将本程序集定义的所有 Controller 和 Action 转换为一个个的 EndPoint ( 终结点路由 ) 放到路由中间件的配置对象 RouteOptions ( 路由设置 ) 中
//将 EndpointMiddleware ( 端点的中间件 ) 注册到http管道中
app.MapControllers();

//配置数据库迁移
if( builder.Configuration[ "App:Migration" ] == "true" )
    await app.MigrationAsync<ISampleUnitOfWork>();


#region 废弃

////执行匹配的端点 ( 终结点执行功能 )
//app.UseEndpoints(op =>
//{
//    //将本程序集定义的所有 Controller 和 Action 转换为一个个的 EndPoint ( 终结点路由 ) 放到路由中间件的配置对象 RouteOptions ( 路由设置 ) 中
//    //将 EndpointMiddleware ( 端点的中间件 ) 注册到http管道中
//    op.MapControllers();
//});

#endregion

//运行应用
app.Run();