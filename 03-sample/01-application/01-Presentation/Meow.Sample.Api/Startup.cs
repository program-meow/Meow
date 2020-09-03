using System;
using Meow.Extension.Application;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Meow.Sample.Api
{
    /// <summary>
    /// ��������
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// ��ʼ����������
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// ����
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ���÷���
        /// </summary>
        /// <param name="services">����</param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var a = Configuration?.GetSection("SqlServerConnection"); 
            var b = Configuration?.GetSection("Connection")?["SqlServer"];


            return services.AddMeow();
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app">Ӧ��</param>
        /// <param name="env">����</param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
