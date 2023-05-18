using Meow.Extension;

namespace Meow.Caching;

/// <summary>
/// 缓存键扩展
/// </summary>
public static class CacheKeyExtension
{
    /// <summary>
    /// 验证缓存键
    /// </summary>
    /// <param name="cacheKey">缓存键</param>
    public static void Validate(this CacheKey cacheKey)
    {
        cacheKey.CheckNull(nameof(cacheKey));
        cacheKey.Key.CheckNull(nameof(cacheKey.Key));
    }
}