using System;
using System.Linq;
using System.Threading.Tasks;
using Meow.Application;
using Meow.Data.Query;
using Meow.Sample.Application.Dtos;
using Meow.Sample.Application.Services.Abstractions;
using Meow.Sample.Data;
using Meow.Sample.Domain.Queries;
using Meow.Sample.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using DomainSample = Meow.Sample.Domain.Models.Sample;

namespace Meow.Sample.Application.Services.Implements;

/// <summary>
/// 样本服务
/// </summary>
public class SampleService : CrudServiceBase<DomainSample , SampleDto , SampleQuery>, ISampleService {
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
        ) : base( serviceProvider , unitOfWork , repository ) {
        _repository = repository;
    }

    /// <inheritdoc />
    protected override IQueryable<DomainSample> Filter( IQueryable<DomainSample> queryable , SampleQuery query ) {
        return queryable.WhereIfNotEmpty( t => t.Name.Contains( query.Name ) );
    }

    /// <summary>
    /// 测试添加
    /// </summary>
    public async Task TestAddAsync() {
        var entity = new DomainSample {
            Name = "测试" ,
        };
        entity.Init();
        await _repository.AddAsync( entity );
        await CommitAsync();
    }

    /// <summary>
    /// 测试修改
    /// </summary>
    public async Task TestUpdateAsync() {
        var entity = await _repository.Find().FirstOrDefaultAsync();
        if( entity == null )
            return;
        entity.Name = "测试修改";
        await _repository.UpdateAsync( entity );
        await CommitAsync();
    }


}
