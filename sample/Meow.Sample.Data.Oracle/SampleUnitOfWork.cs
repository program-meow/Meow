using System;
using Meow.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Meow.Sample.Data.Oracle
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public class SampleUnitOfWork : OracleUnitOfWorkBase, ISampleUnitOfWork
    {
        /// <summary>
        /// 初始化工作单元
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="options">配置项</param>
        public SampleUnitOfWork(IServiceProvider serviceProvider, DbContextOptions<SampleUnitOfWork> options) : base(serviceProvider, options)
        {
        }
    }
}