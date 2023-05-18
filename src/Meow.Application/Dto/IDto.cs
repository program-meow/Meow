namespace Meow.Application.Dto;

/// <summary>
/// 数据传输对象
/// </summary>
public interface IDto : IRequest
{
    /// <summary>
    /// 标识
    /// </summary>
    string Id { get; set; }
}