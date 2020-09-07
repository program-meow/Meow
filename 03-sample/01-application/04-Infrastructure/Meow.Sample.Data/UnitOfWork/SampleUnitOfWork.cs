using Microsoft.EntityFrameworkCore;
using System;

namespace Meow.Sample.Data.UnitOfWork
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public class SampleUnitOfWork : Meow.Data.Ef.UnitOfWork.UnitOfWork, ISampleUnitOfWork
    {
        /// <summary>
        /// 初始化工作单元
        /// </summary>
        /// <param name="options">配置项</param>
        /// <param name="serviceProvider">服务提供器</param>
        public SampleUnitOfWork(DbContextOptions<SampleUnitOfWork> options, IServiceProvider serviceProvider)
            : base(options, serviceProvider)
        {
            foreach (var item in options.Extensions)
            {
                var aa = item.ToString();
            }
        }
    }
}
