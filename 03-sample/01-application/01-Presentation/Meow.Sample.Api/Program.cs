using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Meow.Sample.Api
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
            CreateHostBuilder(args).Build().Run();
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
