using Meow.Extension;

namespace Meow.Helper;

/// <summary>
/// 文件流操作
/// </summary>
public static class File {

    #region IsExistsFile  [判断是否存在文件]

    /// <summary>
    /// 判断是否存在文件
    /// </summary>
    /// <param name="path">文件绝对路径</param>
    public static bool IsExistsFile( string path ) {
        return System.IO.File.Exists( path );
    }

    #endregion

    #region IsExistsDirectory  [判断是否存在目录]

    /// <summary>
    /// 判断是否存在目录
    /// </summary>
    /// <param name="path">目录绝对路径</param>
    public static bool IsExistsDirectory( string path ) {
        return Directory.Exists( path );
    }

    #endregion

    #region CreateDirectory  [创建目录]

    /// <summary>
    /// 创建目录
    /// </summary>
    /// <param name="path">文件或目录绝对路径</param>
    public static void CreateDirectory( string path ) {
        if( path.IsEmpty() )
            return;
        FileInfo file = new FileInfo( path );
        string directoryPath = file.Directory?.FullName;
        if( Directory.Exists( directoryPath ) )
            return;
        Directory.CreateDirectory( directoryPath );
    }

    #endregion

    #region ReadToString  [读取文件到字符串]

    /// <summary>
    /// 读取文件到字符串
    /// </summary>
    /// <param name="filePath">文件绝对路径</param>
    public static string ReadToString( string filePath ) {
        return ReadToString( filePath , Encoding.UTF8 );
    }

    /// <summary>
    /// 读取文件到字符串
    /// </summary>
    /// <param name="filePath">文件绝对路径</param>
    /// <param name="encoding">字符编码</param>
    public static string ReadToString( string filePath , Encoding encoding ) {
        if( System.IO.File.Exists( filePath ) == false )
            return string.Empty;
        using StreamReader reader = new StreamReader( filePath , encoding );
        return reader.ReadToEnd();
    }

    #endregion

    #region ReadToStringAsync  [读取文件到字符串]

    /// <summary>
    /// 读取文件到字符串
    /// </summary>
    /// <param name="filePath">文件绝对路径</param>
    public static async Task<string> ReadToStringAsync( string filePath ) {
        return await ReadToStringAsync( filePath , Encoding.UTF8 );
    }

    /// <summary>
    /// 读取文件到字符串
    /// </summary>
    /// <param name="filePath">文件绝对路径</param>
    /// <param name="encoding">字符编码</param>
    public static async Task<string> ReadToStringAsync( string filePath , Encoding encoding ) {
        if( System.IO.File.Exists( filePath ) == false )
            return string.Empty;
        using StreamReader reader = new StreamReader( filePath , encoding );
        return await reader.ReadToEndAsync();
    }

    #endregion

    #region ReadToStream  [读取文件流]

    /// <summary>
    /// 读取文件流
    /// </summary>
    /// <param name="filePath">文件绝对路径</param>
    public static SystemStream ReadToStream( string filePath ) {
        return new FileStream( filePath , FileMode.Open );
    }

    #endregion

    #region ReadToBytes  [将文件读取到字节流中]

    /// <summary>
    /// 将文件读取到字节流中
    /// </summary>
    /// <param name="filePath">文件的绝对路径</param>
    public static byte[] ReadToBytes( string filePath ) {
        if( !System.IO.File.Exists( filePath ) )
            return null;
        FileInfo fileInfo = new FileInfo( filePath );
        using BinaryReader reader = new BinaryReader( fileInfo.Open( FileMode.Open ) );
        return reader.ReadBytes( ( int ) fileInfo.Length );
    }

    #endregion

    #region Write  [将字符串写入文件]

    /// <summary>
    /// 将字符串写入文件
    /// </summary>
    /// <param name="filePath">文件绝对路径</param>
    /// <param name="content">内容</param>
    public static void Write( string filePath , string content ) {
        Write( filePath , Convert.ToBytes( content ) );
    }

    /// <summary>
    /// 将字节流写入文件
    /// </summary>
    /// <param name="filePath">文件绝对路径</param>
    /// <param name="content">内容</param>
    public static void Write( string filePath , byte[] content ) {
        if( filePath.IsEmpty() )
            return;
        if( content == null )
            return;
        CreateDirectory( filePath );
        System.IO.File.WriteAllBytes( filePath , content );
    }

    #endregion

    #region WriteAsync  [将字符串写入文件]

    /// <summary>
    /// 将字符串写入文件
    /// </summary>
    /// <param name="filePath">文件绝对路径</param>
    /// <param name="content">内容</param>
    public static async Task WriteAsync( string filePath , string content ) {
        await WriteAsync( filePath , Convert.ToBytes( content ) );
    }

    /// <summary>
    /// 将字节流写入文件
    /// </summary>
    /// <param name="filePath">文件绝对路径</param>
    /// <param name="content">内容</param>
    public static async Task WriteAsync( string filePath , byte[] content ) {
        if( filePath.IsEmpty() )
            return;
        if( content == null )
            return;
        CreateDirectory( filePath );
        await System.IO.File.WriteAllBytesAsync( filePath , content );
    }

    #endregion

    #region Delete  [删除文件]

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="filePaths">文件绝对路径集合</param>
    public static void Delete( IEnumerable<string> filePaths ) {
        foreach( string filePath in filePaths )
            Delete( filePath );
    }

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="filePath">文件绝对路径</param>
    public static void Delete( string filePath ) {
        if( filePath.IsEmpty() )
            return;
        if( System.IO.File.Exists( filePath ) )
            System.IO.File.Delete( filePath );
    }

    #endregion

    #region GetAllFiles  [获取全部文件,包括所有子目录]

    /// <summary>
    /// 获取全部文件,包括所有子目录
    /// </summary>
    /// <param name="path">目录路径</param>
    /// <param name="searchPattern">搜索模式</param>
    public static List<FileInfo> GetAllFiles( string path , string searchPattern ) {
        return Directory.GetFiles( path , searchPattern , SearchOption.AllDirectories )
            .Select( filePath => new FileInfo( filePath ) ).ToList();
    }

    #endregion

    /// <summary>
    /// 连接路径
    /// </summary>
    /// <param name="paths">路径列表</param>
    public static string JoinPath( params string[] paths ) {
        return Url.JoinPath( paths );
    }

    /// <summary>
    /// 获取物理路径
    /// </summary>
    /// <param name="relativePath">相对路径,范例:"test/a.txt" 或 "/test/a.txt"</param>
    /// <param name="basePath">基路径,默认为AppContext.BaseDirectory</param>
    public static string GetPhysicalPath( string relativePath , string basePath = null ) {
        if( relativePath.StartsWith( "~" ) )
            relativePath = relativePath.TrimStart( '~' );
        if( relativePath.StartsWith( "/" ) )
            relativePath = relativePath.TrimStart( '/' );
        if( relativePath.StartsWith( "\\" ) )
            relativePath = relativePath.TrimStart( '\\' );
        basePath ??= Meow.Helper.Program.BaseDirectory;
        return Path.Combine( basePath , relativePath );
    }
}