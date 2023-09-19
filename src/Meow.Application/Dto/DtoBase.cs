namespace Meow.Application.Dto;

/// <summary>
/// 数据传输对象
/// </summary>
public abstract class DtoBase : RequestBase, IDto {
    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; }
}