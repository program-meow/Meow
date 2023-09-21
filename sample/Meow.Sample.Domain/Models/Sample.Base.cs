using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Meow.Domain.Auditing;
using Meow.Domain.Entity;
using Meow.Domain.Operation;
using Meow.Tenant;

namespace Meow.Sample.Domain.Models;

/// <summary>
/// 样本
/// </summary>
[Description( "样本" )]
public partial class Sample : AggregateRoot<Sample>, IDelete, IAudited, ITenant {
    /// <summary>
    /// 初始化样本
    /// </summary>
    public Sample() : this( Guid.Empty ) {
    }

    /// <summary>
    /// 初始化样本
    /// </summary>
    /// <param name="id">样本标识</param>
    public Sample( Guid id ) : base( id ) {
    }

    /// <summary>
    /// 名称
    ///</summary>
    [DisplayName( "名称" )]
    [Required]
    [MaxLength( 50 )]
    public string Name { get; set; }
    /// <summary>
    /// 创建时间
    ///</summary>
    [DisplayName( "创建时间" )]
    public DateTime? CreationTime { get; set; }
    /// <summary>
    /// 创建人标识
    ///</summary>
    [DisplayName( "创建人标识" )]
    public Guid? CreatorId { get; set; }
    /// <summary>
    /// 最后修改时间
    ///</summary>
    [DisplayName( "最后修改时间" )]
    public DateTime? LastModificationTime { get; set; }
    /// <summary>
    /// 最后修改人标识
    ///</summary>
    [DisplayName( "最后修改人标识" )]
    public Guid? LastModifierId { get; set; }
    /// <summary>
    /// 是否删除
    ///</summary>
    [DisplayName( "是否删除" )]
    public bool IsDeleted { get; set; }
    /// <summary>
    /// 租户标识
    ///</summary>
    [DisplayName( "租户标识" )]
    public string TenantId { get; set; }

    /// <summary>
    /// 添加变更列表
    /// </summary>
    protected override void AddChanges( Sample other ) {
        AddChange( t => t.Name , other.Name );
        AddChange( t => t.CreationTime , other.CreationTime );
        AddChange( t => t.CreatorId , other.CreatorId );
        AddChange( t => t.LastModificationTime , other.LastModificationTime );
        AddChange( t => t.LastModifierId , other.LastModifierId );
        AddChange( t => t.IsDeleted , other.IsDeleted );
        AddChange( t => t.TenantId , other.TenantId );
    }

}
