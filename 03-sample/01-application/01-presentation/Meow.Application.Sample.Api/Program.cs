using Meow.Exception;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Meow.Application.Sample.Api
{
    /// <summary>
    /// Ӧ�ó���
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Ӧ�ó�����ڵ�
        /// </summary>
        /// <param name="args">��ڵ����</param>
        public static void Main(string[] args)
        {
            try
            {
                WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>()
                    .Build()
                    .Run();
            }
            catch (System.Exception ex)
            {
                throw new Warning($"Ӧ�ó�������ʧ��:{ex.Message}");
            }
            //CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// ��������������
        /// </summary>
        /// <param name="args">��ڵ����</param>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}