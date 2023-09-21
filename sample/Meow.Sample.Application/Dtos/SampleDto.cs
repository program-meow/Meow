using System;
using System.ComponentModel.DataAnnotations;
using Meow.Application.Dto;

namespace Meow.Sample.Application.Dtos;

/// <summary>
/// 样本参数
/// </summary>
public class SampleDto : DtoBase {
    /// <summary>
    /// 名称
    ///</summary>
    [Display( Name = "meow.sample.name" )]
    [Required]
    [MaxLength( 200 )]
    public string Name { get; set; }
    /// <summary>
    /// 创建时间
    ///</summary>
    [Display( Name = "meow.sample.creationTime" )]
    public DateTime? CreationTime { get; set; }
    /// <summary>
    /// 创建人标识
    ///</summary>
    public Guid? CreatorId { get; set; }
    /// <summary>
    /// 最后修改时间
    ///</summary>
    [Display( Name = "meow.sample.lastModificationTime" )]
    public DateTime? LastModificationTime { get; set; }
    /// <summary>
    /// 最后修改人标识
    ///</summary>
    public Guid? LastModifierId { get; set; }
    /// <summary>
    /// 版本号
    ///</summary>
    public byte[] Version { get; set; }
}
