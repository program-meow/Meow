﻿namespace Meow.Data.Query;

/// <summary>
/// 查询边界
/// </summary>
public enum BoundaryEnum {
    /// <summary>
    /// 包含左边
    /// </summary>
    Left,
    /// <summary>
    /// 包含右边
    /// </summary>
    Right,
    /// <summary>
    /// 包含两边
    /// </summary>
    Both,
    /// <summary>
    /// 不包含
    /// </summary>
    Neither
}