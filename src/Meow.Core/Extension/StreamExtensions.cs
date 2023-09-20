namespace Meow.Extension;

/// <summary>
/// 流扩展
/// </summary>
public static class StreamExtensions {
    /// <summary>
    /// 流转换为字节数组
    /// </summary>
    /// <param name="stream">流</param>
    public static byte[] ToBytes( this Stream stream ) {
        return Helper.Stream.ToBytes( stream );
    }

    /// <summary>
    /// 字符串转换成字节数组
    /// </summary>
    /// <param name="data">数据,默认字符编码utf-8</param>        
    public static byte[] ToBytes( this string data ) {
        return Helper.Stream.ToBytes( data );
    }

    /// <summary>
    /// 字符串转换成字节数组
    /// </summary>
    /// <param name="data">数据</param>
    /// <param name="encoding">字符编码</param>
    public static byte[] ToBytes( this string data , Encoding encoding ) {
        return Helper.Stream.ToBytes( data , encoding );
    }

    /// <summary>
    /// 流转换为字节数组
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="cancellationToken">取消令牌</param>
    public static async Task<byte[]> ToBytesAsync( this Stream stream , CancellationToken cancellationToken = default ) {
        return await Helper.Stream.ToBytesAsync( stream , cancellationToken );
    }

    /// <summary>
    /// 字符串转换成流
    /// </summary>
    /// <param name="data">数据</param>
    public static Stream ToStream( this string data ) {
        return Helper.Stream.ToStream( data );
    }

    /// <summary>
    /// 字符串转换成流
    /// </summary>
    /// <param name="data">数据</param>
    /// <param name="encoding">字符编码</param>
    public static Stream ToStream( this string data , Encoding encoding ) {
        return Helper.Stream.ToStream( data , encoding );
    }

    #region ToString  [流转换成字符串]

    /// <summary>
    /// 流转换成字符串
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="encoding">字符编码。默认：UTF8</param>
    /// <param name="bufferSize">缓冲区大小</param>
    /// <param name="isCloseStream">读取完成是否释放流，默认为true</param>
    public static string ToString( this Stream stream , Encoding encoding = null , int bufferSize = 1024 * 2 , bool isCloseStream = true ) {
        return Helper.Stream.ToString( stream , encoding , bufferSize , isCloseStream );
    }

    /// <summary>
    /// 流转换成字符串
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="encoding">字符编码。默认：UTF8</param>
    /// <param name="bufferSize">缓冲区大小</param>
    /// <param name="isCloseStream">读取完成是否释放流，默认为true</param>
    public static async Task<string> ToStringAsync( this Stream stream , Encoding encoding = null , int bufferSize = 1024 * 2 , bool isCloseStream = true ) {
        return await Helper.Stream.ToStringAsync( stream , encoding , bufferSize , isCloseStream );
    }

    /// <summary>
    /// 复制流并转换成字符串
    /// </summary>
    /// <param name="stream">流</param>
    /// <param name="encoding">字符编码。默认：UTF8</param>
    public static async Task<string> CopyToStringAsync( this Stream stream , Encoding encoding = null ) {
        return await Helper.Stream.CopyToStringAsync( stream , encoding );
    }

    #endregion

}