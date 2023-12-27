namespace Meow.Security.SignIn;

/// <summary>
/// 登录状态
/// </summary>
public enum SignInStateEnum {
    /// <summary>
    /// 登录成功
    /// </summary>
    Succeeded = 1,
    /// <summary>
    /// 失败
    /// </summary>
    Failed = 2,
    /// <summary>
    /// 需要双因素认证
    /// </summary>
    TwoFactor = 3,
}