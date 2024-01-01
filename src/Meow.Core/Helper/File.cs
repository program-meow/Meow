using Meow.Extension;

namespace Meow.Helper;

/// <summary>
/// 文件流操作
/// </summary>
public static class File {

    #region ToBytes  [流转换为字节数组]

    /// <summary>
    /// 流转换为字节数组
    /// </summary>
    /// <param name="stream">流</param>
    public static byte[] ToBytes( SystemStream stream ) {
        stream.Seek( 0 , SeekOrigin.Begin );
        byte[] buffer = new byte[ stream.Length ];
        stream.Read( buffer , 0 , buffer.Length );
        return buffer;
    }

    /// <summary>
    /// 字符串转换成字节数组
    /// </summary>
    /// <param name="data">数据,默认字符编码utf-8</param>        
    public static byte[] ToBytes( string data ) {
        return ToBytes( data , Encoding.UTF8 );
    }

    /// <summary>
    /// 字符串转换成字节数组
    /// </summary>
    /// <param name="data">数据</param>
    /// <param name="encoding">字符编码</param>
    public static byte[] ToBytes( string data , Encoding encoding ) {
        if( string.IsNullOrWhiteSpace( data ) )
            return new byte[] { };
        return encoding.GetBytes( data );
    }

    #endregion

    #region ToBytesAsync  [流转换为字节数组]

    /// <summary>
    /// 流转换为字节数组
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="cancellationToken">取消令牌</param>
    public static async Task<byte[]> ToBytesAsync( SystemStream stream , CancellationToken cancellationToken = default ) {
        stream.Seek( 0 , SeekOrigin.Begin );
        byte[] buffer = new byte[ stream.Length ];
        await stream.ReadAsync( buffer , 0 , buffer.Length , cancellationToken );
        return buffer;
    }

    #endregion

    #region ToStream  [字符串转换成流]

    /// <summary>
    /// 字符串转换成流
    /// </summary>
    /// <param name="data">数据</param>
    public static SystemStream ToStream( string data ) {
        return ToStream( data , Encoding.UTF8 );
    }

    /// <summary>
    /// 字符串转换成流
    /// </summary>
    /// <param name="data">数据</param>
    /// <param name="encoding">字符编码</param>
    public static SystemStream ToStream( string data , Encoding encoding ) {
        if( data.IsEmpty() )
            return SystemStream.Null;
        return new MemoryStream( ToBytes( data , encoding ) );
    }

    #endregion

    #region ExistsFile  [判断是否存在文件]

    /// <summary>
    /// 判断是否存在文件
    /// </summary>
    /// <param name="path">文件绝对路径</param>
    public static bool ExistsFile( string path ) {
        return System.IO.File.Exists( path );
    }

    #endregion

    #region ExistsDirectory  [判断是否存在目录]

    /// <summary>
    /// 判断是否存在目录
    /// </summary>
    /// <param name="path">目录绝对路径</param>
    public static bool ExistsDirectory( string path ) {
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

    /// <summary>
    /// 读取流转换成字节数组
    /// </summary>
    /// <param name="stream">流</param>
    public static byte[] ReadToBytes( SystemStream stream ) {
        if( stream == null )
            return null;
        if( stream.CanRead == false )
            return null;
        if( stream.CanSeek )
            stream.Seek( 0 , SeekOrigin.Begin );
        byte[] buffer = new byte[ stream.Length ];
        stream.Read( buffer , 0 , buffer.Length );
        if( stream.CanSeek )
            stream.Seek( 0 , SeekOrigin.Begin );
        return buffer;
    }

    #endregion

    #region ReadToBytesAsync  [读取流转换成字节数组]

    /// <summary>
    /// 读取流转换成字节数组
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="cancellationToken">取消令牌</param>
    public static async Task<byte[]> ReadToBytesAsync( SystemStream stream , CancellationToken cancellationToken = default ) {
        if( stream == null )
            return null;
        if( stream.CanRead == false )
            return null;
        if( stream.CanSeek )
            stream.Seek( 0 , SeekOrigin.Begin );
        byte[] buffer = new byte[ stream.Length ];
        await stream.ReadAsync( buffer , 0 , buffer.Length , cancellationToken );
        if( stream.CanSeek )
            stream.Seek( 0 , SeekOrigin.Begin );
        return buffer;
    }

    #endregion

    #region ReadToMemoryStreamAsync  [读取文件到内存流]

    /// <summary>
    /// 读取文件到内存流
    /// </summary>
    /// <param name="filePath">文件绝对路径</param>
    /// <param name="cancellationToken">取消令牌</param>
    public static async Task<MemoryStream> ReadToMemoryStreamAsync( string filePath , CancellationToken cancellationToken = default ) {
        try {
            if( ExistsFile( filePath ) == false )
                return null;
            MemoryStream memoryStream = new MemoryStream();
            await using FileStream stream = new FileStream( filePath , FileMode.Open );
            await stream.CopyToAsync( memoryStream , cancellationToken ).ConfigureAwait( false );
            return memoryStream;
        } catch {
            return null;
        }
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

    #region Copy  [复制文件]

    /// <summary>
    /// 复制文件
    /// </summary>
    /// <param name="sourceFilePath">源文件绝对路径</param>
    /// <param name="destinationFilePath">目标文件绝对路径</param>
    /// <param name="overwrite">目标文件存在时是否覆盖,默认值: false</param>
    public static void Copy( string sourceFilePath , string destinationFilePath , bool overwrite = false ) {
        if( sourceFilePath.IsEmpty() || destinationFilePath.IsEmpty() )
            return;
        if( ExistsFile( sourceFilePath ) == false )
            return;
        CreateDirectory( destinationFilePath );
        System.IO.File.Copy( sourceFilePath , destinationFilePath , overwrite );
    }

    #endregion

    #region Move  [移动文件]

    /// <summary>
    /// 移动文件
    /// </summary>
    /// <param name="sourceFilePath">源文件绝对路径</param>
    /// <param name="destinationFilePath">目标文件绝对路径</param>
    /// <param name="overwrite">目标文件存在时是否覆盖,默认值: false</param>
    public static void Move( string sourceFilePath , string destinationFilePath , bool overwrite = false ) {
        if( sourceFilePath.IsEmpty() || destinationFilePath.IsEmpty() )
            return;
        if( ExistsFile( sourceFilePath ) == false )
            return;
        CreateDirectory( destinationFilePath );
        System.IO.File.Move( sourceFilePath , destinationFilePath , overwrite );
    }

    #endregion

}