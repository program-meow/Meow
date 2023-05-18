﻿namespace Meow.Caching.EasyCaching;

/// <summary>
/// 缓存提供器标识
/// </summary>
public static class CacheProviderKey
{
    /// <summary>
    /// 内存缓存提供器标识
    /// </summary>
    public const string MemoryCache = "meow.memory.cache";
    /// <summary>
    /// Redis缓存提供器标识
    /// </summary>
    public const string RedisCache = "meow.redis.cache";
}