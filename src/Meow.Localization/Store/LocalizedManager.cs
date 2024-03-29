﻿namespace Meow.Localization.Store;

/// <summary>
/// 本地化资源管理器
/// </summary>
public class LocalizedManager : ILocalizedManager {
    /// <summary>
    /// 本地化资源存储器
    /// </summary>
    private readonly ILocalizedStore _store;
    /// <summary>
    /// 缓存
    /// </summary>
    private readonly IMemoryCache _cache;
    /// <summary>
    /// 本地化配置
    /// </summary>
    private readonly LocalizationOptions _options;

    /// <summary>
    /// 初始化本地化资源管理器
    /// </summary>
    /// <param name="store">本地化资源存储器</param>
    /// <param name="cache">缓存</param>
    /// <param name="options">本地化配置</param>
    public LocalizedManager( ILocalizedStore store , IMemoryCache cache , IOptions<LocalizationOptions> options ) {
        _store = store ?? throw new ArgumentNullException( nameof( store ) );
        _cache = cache ?? throw new ArgumentNullException( nameof( cache ) );
        _options = options?.Value ?? new LocalizationOptions();
    }

    /// <inheritdoc />
    public virtual void LoadAllResources() {
        List<string> cultures = GetCultures();
        foreach( string culture in cultures )
            LoadAllResources( culture );
    }

    /// <summary>
    /// 获取区域文化列表
    /// </summary>
    protected virtual List<string> GetCultures() {
        List<string> result = new List<string> {
            Meow.Helper.Culture.GetCurrentUICultureName()
        };
        IList<string> cultures = _store.GetCultures();
        if( cultures is { Count: > 0 } )
            result.AddRange( cultures );
        return result.Distinct().ToList();
    }

    /// <inheritdoc />
    public virtual void LoadAllResources( string culture ) {
        if( culture.IsEmpty() )
            return;
        IList<string> types = _store.GetTypes();
        if( types == null || types.Count == 0 ) {
            LoadAllResources( culture , null );
            return;
        }
        foreach( string type in types )
            LoadAllResources( culture , type );
    }

    /// <inheritdoc />
    public virtual void LoadAllResources( string culture , string type ) {
        if( culture.IsEmpty() )
            return;
        IDictionary<string , string> resources = _store.GetResources( culture , type );
        if( resources == null )
            return;
        foreach( KeyValuePair<string , string> resource in resources )
            LoadResourceCache( culture , type , resource.Key , resource.Value );
    }

    /// <summary>
    /// 加载资源缓存
    /// </summary>
    protected virtual void LoadResourceCache( string culture , string type , string name , string value ) {
        string cacheKey = CacheHelper.GetCacheKey( culture , type , name );
        LocalizedString localizedString = new LocalizedString( name , value , false , null );
        _cache.Set( cacheKey , localizedString , TimeSpan.FromSeconds( CacheHelper.GetExpiration( _options ) ) );
    }

    /// <inheritdoc />
    public void LoadResourcesByType( string type ) {
        List<string> cultures = GetCultures();
        foreach( string culture in cultures )
            LoadAllResources( culture , type );
    }
}