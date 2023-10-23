//ΪwebӦ�ó���ͷ����ʼ��һ������������ʵ��
using Meow.Sample.Api;

WebApplicationBuilder builder = WebApplication.CreateBuilder( args );

//����Meow
builder.AddMeow();

//���ÿ�����
//���������ķ�����ӵ�ָ���� IServiceCollection ( ָ���������������ϵ�Լ�� )
//�˷�������ע��������ͼ��ҳ��ķ���
builder.Services.AddControllers();

//���ù�����Ԫ
builder.AddIdentityUnitOfWork();


//����WebӦ�ó���
//������������HTTP�ܵ���·�ɵ�webӦ�ó���
WebApplication app = builder.Build();

//===== ��������ܵ� =====//

//����·��
//�� EndpointRoutingMiddleware ( �˵�·���м�� ) ע�ᵽhttp�ܵ���
//��������˵�ƥ�䣬·�ɹ��� ( ·�ɵ�ƥ��������� )
app.UseRouting();

//���ÿ�����
//�������򼯶�������� Controller �� Action ת��Ϊһ������ EndPoint ( �ս��·�� ) �ŵ�·���м�������ö��� RouteOptions ( ·������ ) ��
//�� EndpointMiddleware ( �˵���м�� ) ע�ᵽhttp�ܵ���
app.MapControllers();


#region ����

////ִ��ƥ��Ķ˵� ( �ս��ִ�й��� )
//app.UseEndpoints(op =>
//{
//    //�������򼯶�������� Controller �� Action ת��Ϊһ������ EndPoint ( �ս��·�� ) �ŵ�·���м�������ö��� RouteOptions ( ·������ ) ��
//    //�� EndpointMiddleware ( �˵���м�� ) ע�ᵽhttp�ܵ���
//    op.MapControllers();
//});

#endregion

try {
    //Ǩ������
    await app.MigrateAsync();

    //����Ӧ��
    app.Logger.LogInformation( "׼������Ӧ�� ..." );
    await app.RunAsync();
    return 0;
} catch( Exception ex ) {
    app.Logger.LogCritical( ex , "Ӧ������ʧ�� ..." );
    return 1;
} finally {
    Serilog.Log.CloseAndFlush();
}