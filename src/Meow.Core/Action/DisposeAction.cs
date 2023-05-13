﻿using System;
using Meow.Extension;

namespace Meow.Action;

/// <summary>
/// 释放操作
/// </summary>
public class DisposeAction : IDisposable
{
    /// <summary>
    /// 操作
    /// </summary>
    private readonly System.Action _action;

    /// <summary>
    /// 初始化释放操作
    /// </summary>
    /// <param name="action">操作</param>
    public DisposeAction(System.Action action)
    {
        action.CheckNull(nameof(action));
        _action = action;
    }

    /// <summary>
    /// 空释放操作
    /// </summary>
    public static readonly IDisposable Null = new DisposeAction(null);

    /// <summary>
    /// 释放
    /// </summary>
    public void Dispose()
    {
        _action?.Invoke();
    }
}