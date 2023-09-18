using System;
using Meow.Data.EntityFramework.PostgreSql;
using Microsoft.EntityFrameworkCore;

namespace Meow.Sample.Data.PgSql
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public class SampleUnitOfWork : PgSqlUnitOfWorkBase, ISampleUnitOfWork
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