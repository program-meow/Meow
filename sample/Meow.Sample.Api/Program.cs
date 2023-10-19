using Meow.Authentication;
using Meow.Data.EntityFrameworkCore;
using Meow.Data.EntityFrameworkCore.Migration;
using Meow.Extension;
using Meow.Sample.Data;
using Meow.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

//ΪwebӦ�ó���ͷ����ʼ��һ������������ʵ��
WebApplicationBuilder builder = WebApplication.CreateBuilder( args );

//���������ķ�����ӵ�ָ���� IServiceCollection ( ָ���������������ϵ�Լ�� )
//�˷�������ע��������ͼ��ҳ��ķ���
builder.Services.AddControllers();

builder
    .AsBuild()
    //�������ݿ������ַ����빤����Ԫӳ��
    //SqlServer ���ݿ�
    .AddSqlServerUnitOfWork<ISampleUnitOfWork , Meow.Sample.Data.SqlServer.SampleUnitOfWork>(
        builder.Configuration.GetConnectionString( "SqlServerConnection" ) ,
        condition: builder.Configuration[ "App:DbType" ] == "SqlServer" )
    //PgSql ���ݿ�
    .AddPgSqlUnitOfWork<ISampleUnitOfWork , Meow.Sample.Data.PgSql.SampleUnitOfWork>(
        builder.Configuration.GetConnectionString( "PgSqlConnection" ) ,
        condition: builder.Configuration[ "App:DbType" ] == "PgSql" )
    //Oracle ���ݿ�
    .AddOracleUnitOfWork<ISampleUnitOfWork , Meow.Sample.Data.Oracle.SampleUnitOfWork>(
        builder.Configuration.GetConnectionString( "OracleConnection" ) ,
        condition: builder.Configuration[ "App:DbType" ] == "Oracle" )
    //MySql ���ݿ�
    .AddMySqlUnitOfWork<ISampleUnitOfWork , Meow.Sample.Data.MySql.SampleUnitOfWork>(
        builder.Configuration.GetConnectionString( "MySqlConnection" ) ,
        condition: builder.Configuration[ "App:DbType" ] == "MySql" )
    .AddMeow();


//������������HTTP�ܵ���·�ɵ�webӦ�ó���
WebApplication app = builder.Build();

//�� EndpointRoutingMiddleware ( �˵�·���м�� ) ע�ᵽhttp�ܵ���
//��������˵�ƥ�䣬·�ɹ��� ( ·�ɵ�ƥ��������� )
app.UseRouting();


//�������򼯶�������� Controller �� Action ת��Ϊһ������ EndPoint ( �ս��·�� ) �ŵ�·���м�������ö��� RouteOptions ( ·������ ) ��
//�� EndpointMiddleware ( �˵���м�� ) ע�ᵽhttp�ܵ���
app.MapControllers();

//�������ݿ�Ǩ��
if( builder.Configuration[ "App:Migration" ] == "true" )
    await app.MigrationAsync<ISampleUnitOfWork>();


#region ����

////ִ��ƥ��Ķ˵� ( �ս��ִ�й��� )
//app.UseEndpoints(op =>
//{
//    //�������򼯶�������� Controller �� Action ת��Ϊһ������ EndPoint ( �ս��·�� ) �ŵ�·���м�������ö��� RouteOptions ( ·������ ) ��
//    //�� EndpointMiddleware ( �˵���м�� ) ע�ᵽhttp�ܵ���
//    op.MapControllers();
//});

#endregion

//����Ӧ��
app.Run();