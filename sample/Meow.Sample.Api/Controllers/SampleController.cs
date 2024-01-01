using Meow.Sample.Application.Dtos;
using Meow.Sample.Application.Services.Abstractions;
using Meow.Sample.Domain.Queries;

namespace Meow.Sample.Api.Controllers;

/// <summary>
/// 样本控制器
/// </summary>
public class SampleController : CrudControllerBase<SampleDto , SampleQuery> {
    /// <summary>
    /// 初始化样本控制器
    /// </summary>
    /// <param name="service">样本服务</param>
    public SampleController( ISampleService service ) : base( service ) {
        Service = service;
    }

    /// <summary>
    /// 样本服务
    /// </summary>
    public ISampleService Service { get; }

    /// <summary>
    /// 获取单个实体
    /// </summary>
    /// <param name="id">标识</param>
    [HttpGet( "{id}" )]
    public new async Task<IActionResult> GetAsync( string id ) {
        return await base.GetAsync( id );
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="query">查询参数</param>
    [HttpGet( "Query" )]
    public new async Task<IActionResult> QueryAsync( [FromQuery] SampleQuery query ) {
        return await base.QueryAsync( query );
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="query">查询参数</param>
    [HttpGet]
    public new async Task<IActionResult> PageQueryAsync( [FromQuery] SampleQuery query ) {
        return await base.PageQueryAsync( query );
    }

    /// <summary>
    /// 获取项列表
    /// </summary>
    /// <param name="query">查询参数</param>
    [HttpGet( "Items" )]
    public new async Task<IActionResult> GetItemsAsync( [FromQuery] SampleQuery query ) {
        return await base.GetItemsAsync( query );
    }

    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="request">创建参数</param>
    [HttpPost]
    public new async Task<IActionResult> CreateAsync( SampleDto request ) {
        return await base.CreateAsync( request );
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="id">标识</param>
    /// <param name="request">修改参数</param>
    [HttpPut( "{id?}" )]
    public new async Task<IActionResult> UpdateAsync( string id , SampleDto request ) {
        return await base.UpdateAsync( id , request );
    }

    /// <summary>
    /// 删除，注意：该方法用于删除单个实体，批量删除请使用POST提交，否则可能失败
    /// </summary>
    /// <param name="id">标识</param>
    [HttpDelete( "{id}" )]
    public new async Task<IActionResult> DeleteAsync( string id ) {
        return await base.DeleteAsync( id );
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="ids">标识列表，多个Id用逗号分隔，范例：1,2,3</param>
    [HttpPost( "delete" )]
    public async Task<IActionResult> BatchDeleteAsync( [FromBody] string ids ) {
        return await base.DeleteAsync( ids );
    }

    /// <summary>
    /// 批量保存
    /// </summary>
    /// <param name="request">保存参数</param>
    [HttpPost( "save" )]
    public new async Task<IActionResult> SaveAsync( SaveModel request ) {
        return await base.SaveAsync( request );
    }

    /// <summary>
    /// 测试添加
    /// </summary>
    [HttpPost( "testAdd" )]
    public async Task<IActionResult> TestAddAsync() {
        try {
            await Service.TestAddAsync();
        } catch( System.Exception e ) {
            Console.WriteLine( e );
        }
        return Success();
    }

    /// <summary>
    /// 测试修改
    /// </summary>
    [HttpPost( "testUpdate" )]
    public async Task<IActionResult> TestUpdateAsync() {
        try {
            await Service.TestUpdateAsync();
        } catch( System.Exception e ) {
            Console.WriteLine( e );
        }
        return Success();
    }

}
