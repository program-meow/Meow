﻿namespace Meow.Extension;

/// <summary>
/// 编码扩展
/// </summary>
public static class EncodeExtensions {

    #region Base32

    /// <summary>
    /// base32 编码
    /// </summary>
    /// <param name="value">值</param>
    public static byte[] ToEncodeByBase32( this string value ) {
        return Meow.Helper.EncodeByBase32.Encode( value );
    }

    /// <summary>
    /// base32 解码
    /// </summary>
    /// <param name="value">值</param>
    public static string ToDecodeByBase32( this byte[] value ) {
        return Meow.Helper.EncodeByBase32.Decode( value );
    }

    #endregion

    #region Base36

    /// <summary>
    /// 编码
    /// </summary>
    /// <param name="guid">Guid值</param>
    public static string ToEncodeByBase36( this Guid? guid ) {
        return Meow.Helper.EncodeByBase36.Encode( guid );
    }

    /// <summary>
    /// 编码
    /// </summary>
    /// <param name="guid">Guid值</param>
    public static string ToEncodeByBase36( this Guid guid ) {
        return Meow.Helper.EncodeByBase36.Encode( guid );
    }

    /// <summary>
    /// 编码
    /// </summary>
    /// <param name="value">值</param>
    public static string ToEncodeByBase36( this string value ) {
        return Meow.Helper.EncodeByBase36.Encode( value );
    }

    /// <summary>
    /// 编码
    /// </summary>
    /// <param name="num">长整型值</param>
    public static string ToEncodeByBase36( this long? num ) {
        return Meow.Helper.EncodeByBase36.Encode( num );
    }

    /// <summary>
    /// 编码
    /// </summary>
    /// <param name="num">长整型值</param>
    public static string ToEncodeByBase36( this long num ) {
        return Meow.Helper.EncodeByBase36.Encode( num );
    }

    /// <summary>
    /// 解码成字符串
    /// </summary>
    /// <param name="value">值</param>
    public static string ToDecodeToStringByBase36( this string value ) {
        return Meow.Helper.EncodeByBase36.DecodeToString( value );
    }

    /// <summary>
    /// 解码成Guid
    /// </summary>
    /// <param name="value">值</param>
    public static Guid? ToDecodeToGuidByBase36( this string value ) {
        return Meow.Helper.EncodeByBase36.DecodeToGuid( value );
    }

    /// <summary>
    /// 解码成长整型
    /// </summary>
    /// <param name="value">值</param>
    public static long? ToDecodeToLongByBase36( this string value ) {
        return Meow.Helper.EncodeByBase36.DecodeToLong( value );
    }

    #endregion

    #region Base62

    /// <summary>
    /// 编码
    /// </summary>
    /// <param name="guid">Guid值</param>
    public static string ToEncodeByBase62( this Guid? guid ) {
        return Meow.Helper.EncodeByBase62.Encode( guid );
    }

    /// <summary>
    /// 编码
    /// </summary>
    /// <param name="guid">Guid值</param>
    public static string ToEncodeByBase62( this Guid guid ) {
        return Meow.Helper.EncodeByBase62.Encode( guid );
    }

    /// <summary>
    /// 编码
    /// </summary>
    /// <param name="value">值</param>
    public static string ToEncodeByBase62( this string value ) {
        return Meow.Helper.EncodeByBase62.Encode( value );
    }

    /// <summary>
    /// 编码
    /// </summary>
    /// <param name="num">长整型值</param>
    public static string ToEncodeByBase62( this long? num ) {
        return Meow.Helper.EncodeByBase62.Encode( num );
    }

    /// <summary>
    /// 编码
    /// </summary>
    /// <param name="num">长整型值</param>
    public static string ToEncodeByBase62( this long num ) {
        return Meow.Helper.EncodeByBase62.Encode( num );
    }

    /// <summary>
    /// 解码成字符串
    /// </summary>
    /// <param name="value">值</param>
    public static string ToDecodeToStringByBase62( this string value ) {
        return Meow.Helper.EncodeByBase62.DecodeToString( value );
    }

    /// <summary>
    /// 解码成Guid
    /// </summary>
    /// <param name="value">值</param>
    public static Guid? ToDecodeToGuidByBase62( this string value ) {
        return Meow.Helper.EncodeByBase62.DecodeToGuid( value );
    }

    /// <summary>
    /// 解码成长整型
    /// </summary>
    /// <param name="value">值</param>
    public static long? ToDecodeToLongByBase62( this string value ) {
        return Meow.Helper.EncodeByBase62.DecodeToLong( value );
    }

    #endregion

    #region Base64Url

    /// <summary>
    /// 以下函数执行 base-64-url 编码，与常规base64编码的区别如下
    /// * 省略填充，因此填充字符 '=' 不必进行百分比编码
    /// * 第62和63个常规 base-64 编码字符 ('+'和'/') 被替换为 ('-'和'_')
    /// 这些更改使编码字母文件和 URL 安全
    /// </summary>
    /// <param name="value">要编码的字符串</param>
    /// <returns>Base 64 Url 的 UTF8 字节编码</returns>
    public static string ToEncodeByBase64Url( this string value ) {
        return Meow.Helper.EncodeByBase64Url.Encode( value );
    }

    /// <summary>
    /// 将一个8位无符号整数数组的子集转换为其等效的字符串表示形式，该字符串表示形式由 base-64-url 数字编码
    /// 参数将子集指定为输入数组中的偏移量，以及数组中要转换的元素数量
    /// </summary>
    /// <param name="inArray">由8位无符号整数组成的数组</param>
    /// <param name="length">inArray 中的偏移量</param>
    /// <param name="offset">要转换的 inArray 元素的数量</param>
    /// <returns>inArray 的长度元素的 base64 url编码的字符串表示形式，从位置偏移开始</returns>
    public static string ToEncodeByBase64Url( this byte[] inArray , int offset , int length ) {
        return Meow.Helper.EncodeByBase64Url.Encode( inArray , offset , length );
    }

    /// <summary>
    /// 将一个8位无符号整数数组的子集转换为其等效的字符串表示形式，该字符串表示形式由 base-64-url 数字编码
    /// 参数将子集指定为输入数组中的偏移量，以及数组中要转换的元素数量
    /// </summary>
    /// <param name="inArray">由8位无符号整数组成的数组</param>
    /// <returns>inArray 的长度元素的64进制 url 编码的字符串表示形式，从位置偏移开始</returns>
    public static string ToEncodeByBase64Url( this byte[] inArray ) {
        return Meow.Helper.EncodeByBase64Url.Encode( inArray );
    }

    /// <summary>
    /// 将指定的字符串(该字符串将二进制数据编码为base-64-url数字)转换为等效的8位无符号整数数组
    /// </summary>
    /// <param name="value">base64Url 编码字符串</param>
    /// <returns>UTF8 bytes</returns>
    public static byte[] ToDecodeToBytesByBase64Url( this string value ) {
        return Meow.Helper.EncodeByBase64Url.DecodeToBytes( value );
    }

    /// <summary>
    /// 将字符串从 Base64 Url Encoded解码为 UTF8
    /// </summary>
    /// <param name="value">要解码的字符串</param>
    /// <returns>UTF8 字符串</returns>
    public static string ToDecodeByBase64Url( this string value ) {
        return Meow.Helper.EncodeByBase64Url.Decode( value );
    }

    #endregion
}