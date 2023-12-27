namespace Meow.Localization.Json;

/// <summary>
/// 路径解析器
/// </summary>
public class PathResolver : IPathResolver {
    /// <inheritdoc />
    public string GetRootNamespace( Assembly assembly ) {
        assembly.CheckNull( nameof( assembly ) );
        RootNamespaceAttribute attribute = assembly.GetCustomAttribute<RootNamespaceAttribute>();
        return attribute == null ? assembly.GetName().Name : attribute.RootNamespace;
    }

    /// <inheritdoc />
    public string GetResourcesRootPath( Assembly assembly , string rootPath ) {
        if( assembly == null )
            return rootPath;
        ResourceLocationAttribute attribute = assembly.GetCustomAttribute<ResourceLocationAttribute>();
        return attribute == null ? rootPath : attribute.ResourceLocation;
    }

    /// <inheritdoc />
    public string GetResourcesBaseName( Assembly assembly , string typeFullName ) {
        string rootNamespace = GetRootNamespace( assembly );
        return typeFullName.RemoveStart( $"{rootNamespace}." );
    }

    /// <inheritdoc />
    public string GetJsonResourcePath( string rootPath , string baseName , CultureInfo culture ) {
        if( baseName.IsEmpty() )
            return Path.Combine( Meow.Helper.Program.BaseDirectory , rootPath , $"{culture.Name}.json" );
        baseName = FixInnerClassPath( baseName );
        return Path.Combine( Meow.Helper.Program.BaseDirectory , rootPath , $"{baseName}.{culture.Name}.json" );
    }

    /// <summary>
    /// 修复内部类分隔符+
    /// </summary>
    private string FixInnerClassPath( string path ) {
        const char innerClassSeparator = '+';
        return path.Contains( innerClassSeparator ) ? path.Replace( innerClassSeparator , '.' ) : path;
    }
}