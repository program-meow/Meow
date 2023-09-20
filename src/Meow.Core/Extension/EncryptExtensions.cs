namespace Meow.Extension;

/// <summary>
/// 加密扩展
/// </summary>
public static class EncryptExtensions {

    #region Md5加密

    /// <summary>
    /// Md5加密，返回16位结果
    /// </summary>
    /// <param name="value">值</param>
    public static string ToMd5By16( this string value ) {
        return Meow.Helper.Encrypt.Md5By16( value );
    }

    /// <summary>
    /// Md5加密，返回16位结果
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="encoding">字符编码</param>
    public static string ToMd5By16( this string value , Encoding encoding ) {
        return Meow.Helper.Encrypt.Md5By16( value , encoding );
    }

    /// <summary>
    /// Md5加密，返回32位结果
    /// </summary>
    /// <param name="value">值</param>
    public static string ToMd5By32( this string value ) {
        return Meow.Helper.Encrypt.Md5By32( value );
    }

    /// <summary>
    /// Md5加密，返回32位结果
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="encoding">字符编码</param>
    public static string ToMd5By32( this string value , Encoding encoding ) {
        return Meow.Helper.Encrypt.Md5By32( value , encoding );
    }

    #endregion

    #region DES加密

    /// <summary>
    /// DES加密
    /// </summary>
    /// <param name="value">待加密的值</param>
    public static string ToDesEncrypt( this object value ) {
        return Meow.Helper.Encrypt.DesEncrypt( value );
    }

    /// <summary>
    /// DES加密
    /// </summary>
    /// <param name="value">待加密的值</param>
    /// <param name="key">密钥,24位</param>
    /// <param name="encoding">编码</param>
    /// <param name="cipherMode">加密模式</param>
    /// <param name="paddingMode">填充模式</param>
    public static string ToDesEncrypt( this object value , string key , Encoding encoding = null , CipherMode cipherMode = CipherMode.ECB , PaddingMode paddingMode = PaddingMode.PKCS7 ) {
        return Meow.Helper.Encrypt.DesEncrypt( value , key , encoding , cipherMode , paddingMode );
    }

    /// <summary>
    /// DES解密
    /// </summary>
    /// <param name="value">加密后的值</param>
    public static string ToDesDecrypt( this object value ) {
        return Meow.Helper.Encrypt.DesDecrypt( value );
    }

    /// <summary>
    /// DES解密
    /// </summary>
    /// <param name="value">加密后的值</param>
    /// <param name="key">密钥,24位</param>
    /// <param name="encoding">编码</param>
    /// <param name="cipherMode">加密模式</param>
    /// <param name="paddingMode">填充模式</param>
    public static string ToDesDecrypt( this object value , string key , Encoding encoding = null , CipherMode cipherMode = CipherMode.ECB , PaddingMode paddingMode = PaddingMode.PKCS7 ) {
        return Meow.Helper.Encrypt.DesDecrypt( value , key , encoding , cipherMode , paddingMode );
    }

    #endregion

    #region AES加密

    /// <summary>
    /// AES加密
    /// </summary>
    /// <param name="value">待加密的值</param>
    public static string ToAesEncrypt( this string value ) {
        return Meow.Helper.Encrypt.AesEncrypt( value );
    }

    /// <summary>
    /// AES加密
    /// </summary>
    /// <param name="value">待加密的值</param>
    /// <param name="key">密钥</param>
    /// <param name="encoding">编码</param>
    /// <param name="cipherMode">加密模式</param>
    /// <param name="paddingMode">填充模式</param>
    /// <param name="iv">初始化向量</param>
    public static string ToAesEncrypt( this string value , string key , Encoding encoding = null , CipherMode cipherMode = CipherMode.CBC , PaddingMode paddingMode = PaddingMode.PKCS7 , byte[] iv = null ) {
        return Meow.Helper.Encrypt.AesEncrypt( value , key , encoding , cipherMode , paddingMode , iv );
    }

    /// <summary>
    /// AES解密
    /// </summary>
    /// <param name="value">加密后的值</param>
    public static string ToAesDecrypt( this string value ) {
        return Meow.Helper.Encrypt.AesDecrypt( value );
    }

    /// <summary>
    /// AES解密
    /// </summary>
    /// <param name="value">加密后的值</param>
    /// <param name="key">密钥</param>
    /// <param name="encoding">编码</param>
    /// <param name="cipherMode">加密模式</param>
    /// <param name="paddingMode">填充模式</param>
    /// <param name="iv">初始化向量</param>
    public static string ToAesDecrypt( this string value , string key , Encoding encoding = null , CipherMode cipherMode = CipherMode.CBC , PaddingMode paddingMode = PaddingMode.PKCS7 , byte[] iv = null ) {
        return Meow.Helper.Encrypt.AesDecrypt( value , key , encoding , cipherMode , paddingMode , iv );
    }

    #endregion

    #region HmacSha256加密

    /// <summary>
    /// HMACSHA256加密
    /// </summary>
    /// <param name="value">值</param>
    /// <param name="key">密钥</param>
    /// <param name="encoding">字符编码</param>
    public static string ToHmacSha256( this string value , string key , Encoding encoding = null ) {
        return Meow.Helper.Encrypt.HmacSha256( value , key , encoding );
    }

    #endregion

    #region RSA加密

    /// <summary>
    /// RSA签名
    /// </summary>
    /// <param name="value">待加密的值</param>
    /// <param name="privateKey">私钥</param>
    /// <param name="encoding">编码</param>
    /// <param name="hashAlgorithm">加密算法,默认值: HashAlgorithmName.SHA1</param>
    /// <param name="rsaKeyType">Rsa密钥类型,默认值: Pkcs1</param>
    public static string ToRsaSign( this string value , string privateKey , Encoding encoding = null , HashAlgorithmName? hashAlgorithm = null , RSAKeyType rsaKeyType = RSAKeyType.Pkcs1 ) {
        return Meow.Helper.Encrypt.RsaSign( value , privateKey , encoding , hashAlgorithm , rsaKeyType );
    }

    /// <summary>
    /// Rsa验签
    /// </summary>
    /// <param name="value">待验签的值</param>
    /// <param name="publicKey">公钥</param>
    /// <param name="sign">签名</param>
    /// <param name="encoding">编码</param>
    /// <param name="hashAlgorithm">加密算法,默认值: HashAlgorithmName.SHA1</param>
    public static bool ToRsaVerify( this string value , string publicKey , string sign , Encoding encoding = null , HashAlgorithmName? hashAlgorithm = null ) {
        return Meow.Helper.Encrypt.RsaVerify( value , publicKey , sign , encoding , hashAlgorithm );
    }

    /// <summary>
    /// RSA加密
    /// </summary>
    /// <param name="value">待加密的值</param>
    /// <param name="publicKey">公钥</param>
    public static string ToRsaEncrypt( this string value , string publicKey ) {
        return Meow.Helper.Encrypt.RsaEncrypt( value , publicKey );

    }

    /// <summary>
    /// RSA解密
    /// </summary>
    /// <param name="value">加密后的值</param>
    /// <param name="privateKey">私钥</param>
    public static string ToRsaDecrypt( this string value , string privateKey ) {
        return Meow.Helper.Encrypt.RsaDecrypt( value , privateKey );
    }

    #endregion
}