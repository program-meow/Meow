using Meow.Extension;

namespace Meow.Helper;

/// <summary>
/// 流操作
/// </summary>
public static class Stream {

    #region ToBytes  [流转换为字节数组]

    /// <summary>
    /// 流转换为字节数组
    /// </summary>
    /// <param name="stream">流</param>
    public static byte[] ToBytes( SystemStream stream ) {
        stream.Seek( 0 , SeekOrigin.Begin );
        byte[] buffer = new byte[ stream.Length ];
        int read = stream.Read( buffer , 0 , buffer.Length );
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
        if( data.IsEmpty() )
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
        int readAsync = await stream.ReadAsync( buffer , 0 , buffer.Length , cancellationToken );
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

    #region ToString  [流转换成字符串]

    /// <summary>
    /// 流转换成字符串
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="encoding">字符编码。默认：UTF8</param>
    /// <param name="bufferSize">缓冲区大小</param>
    /// <param name="isCloseStream">读取完成是否释放流，默认为true</param>
    public static string ToString( SystemStream stream , Encoding encoding = null , int bufferSize = 1024 * 2 , bool isCloseStream = true ) {
        if( stream == null )
            return string.Empty;
        if( encoding == null )
            encoding = Encoding.UTF8;
        if( stream.CanRead == false )
            return string.Empty;
        using( StreamReader reader = new StreamReader( stream , encoding , true , bufferSize , !isCloseStream ) ) {
            if( stream.CanSeek )
                stream.Seek( 0 , SeekOrigin.Begin );
            string result = reader.ReadToEnd();
            if( stream.CanSeek )
                stream.Seek( 0 , SeekOrigin.Begin );
            return result;
        }
    }

    /// <summary>
    /// 流转换成字符串
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="encoding">字符编码。默认：UTF8</param>
    /// <param name="bufferSize">缓冲区大小</param>
    /// <param name="isCloseStream">读取完成是否释放流，默认为true</param>
    public static async Task<string> ToStringAsync( SystemStream stream , Encoding encoding = null , int bufferSize = 1024 * 2 , bool isCloseStream = true ) {
        if( stream == null )
            return string.Empty;
        if( encoding == null )
            encoding = Encoding.UTF8;
        if( stream.CanRead == false )
            return string.Empty;
        using( StreamReader reader = new StreamReader( stream , encoding , true , bufferSize , !isCloseStream ) ) {
            if( stream.CanSeek )
                stream.Seek( 0 , SeekOrigin.Begin );
            string result = await reader.ReadToEndAsync();
            if( stream.CanSeek )
                stream.Seek( 0 , SeekOrigin.Begin );
            return result;
        }
    }

    /// <summary>
    /// 复制流并转换成字符串
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="encoding">字符编码。默认：UTF8</param>
    public static async Task<string> CopyToStringAsync( SystemStream stream , Encoding encoding = null ) {
        if( stream == null )
            return string.Empty;
        if( encoding == null )
            encoding = Encoding.UTF8;
        if( stream.CanRead == false )
            return string.Empty;
        using( MemoryStream memoryStream = new MemoryStream() ) {
            using( StreamReader reader = new StreamReader( memoryStream , encoding ) ) {
                if( stream.CanSeek )
                    stream.Seek( 0 , SeekOrigin.Begin );
                stream.CopyTo( memoryStream );
                if( memoryStream.CanSeek )
                    memoryStream.Seek( 0 , SeekOrigin.Begin );
                string result = await reader.ReadToEndAsync();
                if( stream.CanSeek )
                    stream.Seek( 0 , SeekOrigin.Begin );
                return result;
            }
        }
    }

    #endregion
}