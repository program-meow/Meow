using System;
using System.Linq;
using Meow.Application;
using Meow.Data.Extension;
using Meow.Sample.Application.Dtos;
using Meow.Sample.Application.Services.Abstractions;
using Meow.Sample.Data;
using Meow.Sample.Domain.Queries;
using Meow.Sample.Domain.Repositories;

namespace Meow.Sample.Application.Services.Implements
{
    /// <summary>
    /// 样本服务
    /// </summary>
    public class SampleService : CrudServiceBase<Domain.Models.Sample, SampleDto, SampleQuery>, ISampleService
    {
        /// <summary>
        /// 样本仓储
        /// </summary>
        private readonly ISampleRepository _repository;

        /// <summary>
        /// 初始化样本服务
        /// </summary>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="repository">样本仓储</param>
        public SampleService(
            IServiceProvider serviceProvider
            , ISampleUnitOfWork unitOfWork
            , ISampleRepository repository
            ) : base(serviceProvider, unitOfWork, repository)
        {
            _repository = repository;
        }

        /// <inheritdoc />
        protected override IQueryable<Domain.Models.Sample> Filter(IQueryable<Domain.Models.Sample> queryable, SampleQuery query)
        {
            return queryable.WhereIfNotEmpty(t => t.Name.Contains(query.Name));
        }

    }
}