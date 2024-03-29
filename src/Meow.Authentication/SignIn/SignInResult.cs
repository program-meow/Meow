﻿namespace Meow.Security.SignIn;

/// <summary>
/// 登录结果
/// </summary>
public class SignInResult {
    /// <summary>
    /// 初始化
    /// </summary>
    public SignInResult() {
        State = SignInStateEnum.Failed;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="state">登录状态</param>
    /// <param name="userId">用户标识</param>
    /// <param name="message">消息</param>
    public SignInResult( SignInStateEnum state , string userId , string message = null ) {
        State = state;
        UserId = userId;
        Message = message;
    }

    /// <summary>
    /// 登录状态
    /// </summary>
    public SignInStateEnum State { get; set; }

    /// <summary>
    /// 用户标识
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// 消息
    /// </summary>
    public string Message { get; set; }
}