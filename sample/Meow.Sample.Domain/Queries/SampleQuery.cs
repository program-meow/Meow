using System;
using System.ComponentModel.DataAnnotations;
using Meow.Query;

namespace Meow.Sample.Domain.Queries;

/// <summary>
/// 样本查询参数
/// </summary>
public class SampleQuery : QueryParameter {
    /// <summary>
    /// 名称
    ///</summary>
    [Display( Name = "meow.sample.name" )]
    public string Name { get; set; }
    /// <summary>
    /// 起始创建时间
    /// </summary>
    [Display( Name = "meow.sample.beginCreationTime" )]
    public DateTime? BeginCreationTime { get; set; }
    /// <summary>
    /// 结束创建时间
    /// </summary>
    [Display( Name = "meow.sample.endCreationTime" )]
    public DateTime? EndCreationTime { get; set; }
    /// <summary>
    /// 起始最后修改时间
    /// </summary>
    [Display( Name = "meow.sample.beginLastModificationTime" )]
    public DateTime? BeginLastModificationTime { get; set; }
    /// <summary>
    /// 结束最后修改时间
    /// </summary>
    [Display( Name = "meow.sample.endLastModificationTime" )]
    public DateTime? EndLastModificationTime { get; set; }
}
