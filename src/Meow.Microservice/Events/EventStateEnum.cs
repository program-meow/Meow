namespace Meow.Microservice.Events;

/// <summary>
/// 事件状态
/// </summary>
public enum EventStateEnum {
    /// <summary>
    /// 已发布
    /// </summary>
    [Description( "meow.eventState.published" )]
    Published = 1,
    /// <summary>
    /// 处理中
    /// </summary>
    [Description( "meow.eventState.processing" )]
    Processing = 2,
    /// <summary>
    /// 所有订阅全部成功完成
    /// </summary>
    [Description( "meow.eventState.success" )]
    Success = 3,
    /// <summary>
    /// 失败
    /// </summary>
    [Description( "meow.eventState.fail" )]
    Fail = 4
}