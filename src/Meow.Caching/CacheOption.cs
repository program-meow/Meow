using System;

namespace Meow.Caching;

/// <summary>
/// 缓存配置
/// </summary>
public class CacheOption
{
    /// <summary>
    /// 过期时间间隔,默认值: 8小时
    /// </summary>
    public TimeSpan? Expiration { get; set; }
}