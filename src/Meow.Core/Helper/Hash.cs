using Meow.Extension;

namespace Meow.Helper;

/// <summary>
/// 哈希操作
/// </summary>
public static class Hash {
    /// <summary>
    /// 转换哈希字符串
    /// </summary>
    /// <param name="type">哈希类型</param>
    /// <param name="str">源字符串</param>
    /// <returns>哈希算法处理之后的字符串</returns>
    public static string ToString( HashTypeEnum type , string str ) => ToString( type , str , Encoding.UTF8 );

    /// <summary>
    /// 转换哈希字符串
    /// </summary>
    /// <param name="type">哈希类型</param>
    /// <param name="str">源字符串</param>
    /// <param name="isLower">是否是小写</param>
    /// <returns>哈希算法处理之后的字符串</returns>
    public static string ToString( HashTypeEnum type , string str , bool isLower ) => ToString( type , str , Encoding.UTF8 , isLower );

    /// <summary>
    /// 转换哈希字符串
    /// </summary>
    /// <param name="type">哈希类型</param>
    /// <param name="str">源字符串</param>
    /// <param name="key">key</param>
    /// <param name="isLower">是否是小写</param>
    /// <returns>哈希算法处理之后的字符串</returns>
    public static string ToString( HashTypeEnum type , string str , string key , bool isLower = false ) => ToString( type , str , key , Encoding.UTF8 , isLower );

    /// <summary>
    /// 转换哈希字符串
    /// </summary>
    /// <param name="type">哈希类型</param>
    /// <param name="str">源字符串</param>
    /// <param name="encoding">编码类型</param>
    /// <param name="isLower">是否是小写</param>
    /// <returns>哈希算法处理之后的字符串</returns>
    public static string ToString( HashTypeEnum type , string str , Encoding encoding , bool isLower = false ) => ToString( type , str , null , encoding , isLower );

    /// <summary>
    /// 转换哈希字符串
    /// </summary>
    /// <param name="type">哈希类型</param>
    /// <param name="str">源字符串</param>
    /// <param name="key">key</param>
    /// <param name="encoding">编码类型</param>
    /// <param name="isLower">是否是小写</param>
    /// <returns>哈希算法处理之后的字符串</returns>
    public static string ToString( HashTypeEnum type , string str , string key , Encoding encoding , bool isLower = false ) {
        if( str.IsEmpty() )
            return string.Empty;
        return ToString( type , encoding.GetBytes( str ) , key.IsEmpty()
            ? null
            : encoding.GetBytes( key! ) , isLower );
    }

    /// <summary>
    /// 计算字符串Hash值
    /// </summary>
    /// <param name="type">hash类型</param>
    /// <param name="source">source</param>
    /// <returns>hash过的字节数组</returns>
    public static string ToString( HashTypeEnum type , byte[] source ) => ToString( type , source , null );

    /// <summary>
    /// 计算字符串Hash值
    /// </summary>
    /// <param name="type">hash类型</param>
    /// <param name="source">source</param>
    /// <param name="isLower">isLower</param>
    /// <returns>hash过的字节数组</returns>
    public static string ToString( HashTypeEnum type , byte[] source , bool isLower ) => ToString( type , source , null , isLower );

    /// <summary>
    /// 转换哈希字符串
    /// </summary>
    /// <param name="type">哈希类型</param>
    /// <param name="source">源</param>
    /// <param name="key">key</param>
    /// <param name="isLower">是否是小写</param>
    /// <returns>哈希算法处理之后的字符串</returns>
    public static string ToString( HashTypeEnum type , byte[] source , byte[] key , bool isLower = false ) {
        if( source.IsEmpty() )
            return string.Empty;
        byte[] hashedBytes = ToBytes( type , source , key );
        StringBuilder sbText = new StringBuilder();
        if( isLower ) {
            foreach( byte b in hashedBytes )
                sbText.Append( b.ToString( "x2" ) );
        } else {
            foreach( byte b in hashedBytes )
                sbText.Append( b.ToString( "X2" ) );
        }
        return sbText.ToString();
    }

    /// <summary>
    /// 计算字符串Hash值
    /// </summary>
    /// <param name="type">hash类型</param>
    /// <param name="str">要hash的字符串</param>
    /// <returns>hash过的字节数组</returns>
    public static byte[] ToBytes( HashTypeEnum type , string str ) => ToBytes( type , str , Encoding.UTF8 );

    /// <summary>
    /// 计算字符串Hash值
    /// </summary>
    /// <param name="type">hash类型</param>
    /// <param name="str">要hash的字符串</param>
    /// <param name="encoding">编码类型</param>
    /// <returns>hash过的字节数组</returns>
    public static byte[] ToBytes( HashTypeEnum type , string str , Encoding encoding ) {
        if( str.IsEmpty() )
            return Array.Empty<byte>();
        byte[] bytes = encoding.GetBytes( str );
        return ToBytes( type , bytes );
    }

    /// <summary>
    /// 转换Hash后的字节数组
    /// </summary>
    /// <param name="type">哈希类型</param>
    /// <param name="bytes">原字节数组</param>
    /// <returns></returns>
    public static byte[] ToBytes( HashTypeEnum type , byte[] bytes ) => ToBytes( type , bytes , null );

    /// <summary>
    /// 转换Hash后的字节数组
    /// </summary>
    /// <param name="type">哈希类型</param>
    /// <param name="key">key</param>
    /// <param name="bytes">原字节数组</param>
    /// <returns></returns>
    public static byte[] ToBytes( HashTypeEnum type , byte[] bytes , byte[] key ) {
        if( bytes.IsEmpty() )
            return Array.Empty<byte>();
        HashAlgorithm algorithm = null;
        try {
            if( key == null ) {
                algorithm = type switch {
                    HashTypeEnum.SHA1 => SHA1.Create(),
                    HashTypeEnum.SHA256 => SHA256.Create(),
                    HashTypeEnum.SHA384 => SHA384.Create(),
                    HashTypeEnum.SHA512 => SHA512.Create(),
                    _ => MD5.Create()
                };
            } else {
                algorithm = type switch {
                    HashTypeEnum.SHA1 => new HMACSHA1( key ),
                    HashTypeEnum.SHA256 => new HMACSHA256( key ),
                    HashTypeEnum.SHA384 => new HMACSHA384( key ),
                    HashTypeEnum.SHA512 => new HMACSHA512( key ),
                    _ => new HMACMD5( key )
                };
            }
            return algorithm.ComputeHash( bytes );
        } finally {
            algorithm?.Dispose();
        }
    }
}

/// <summary>
/// 哈希 类型
/// </summary>
public enum HashTypeEnum {
    /// <summary>
    /// MD5
    /// </summary>
    MD5 = 0,

    /// <summary>
    /// SHA1
    /// </summary>
    SHA1 = 1,

    /// <summary>
    /// SHA256
    /// </summary>
    SHA256 = 2,

    /// <summary>
    /// SHA384
    /// </summary>
    SHA384 = 3,

    /// <summary>
    /// SHA512
    /// </summary>
    SHA512 = 4
}