namespace Meow.Microservice.Events;

/// <summary>
/// 订阅状态
/// </summary>
public enum SubscriptionStateEnum {
    /// <summary>
    /// 处理中
    /// </summary>
    [Description( "meow.subscriptionState.processing" )]
    Processing = 1,
    /// <summary>
    /// 成功完成
    /// </summary>
    [Description( "meow.subscriptionState.success" )]
    Success = 2,
    /// <summary>
    /// 失败
    /// </summary>
    [Description( "meow.subscriptionState.fail" )]
    Fail = 3
}