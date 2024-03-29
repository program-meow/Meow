﻿namespace Meow.Extension;

/// <summary>
/// 枚举扩展
/// </summary>
public static class EnumExtensions {
    /// <summary>
    /// 枚举 - 获取实例
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    /// <param name="member">成员名或值,范例:Enum1枚举有成员A=0,则传入"A"或"0"获取 Enum1.A</param>
    public static TEnum ParseEnum<TEnum>( this object member ) {
        return Meow.Helper.Enum.Parse<TEnum>( member );
    }

    /// <summary>
    /// 获取成员名
    /// </summary>
    /// <param name="instance">枚举实例</param>
    public static string GetName( this System.Enum instance ) {
        return Meow.Helper.Enum.GetName( instance );
    }

    /// <summary>
    /// 获取枚举值
    /// </summary>
    /// <param name="instance">枚举实例</param>
    public static int? GetValue( this System.Enum instance ) {
        return Meow.Helper.Enum.GetValue( instance );
    }

    /// <summary>
    /// 获取枚举值
    /// </summary>
    /// <typeparam name="TResult">返回值类型</typeparam>
    /// <param name="instance">枚举实例</param>
    public static TResult GetValue<TResult>( this System.Enum instance ) {
        return Meow.Helper.Enum.GetValue<TResult>( instance );
    }

    /// <summary>
    /// 获取枚举描述,使用System.ComponentModel.Description特性设置描述
    /// </summary>
    /// <param name="instance">枚举实例</param>
    public static string GetDescription( this System.Enum instance ) {
        return Meow.Helper.Enum.GetDescription( instance );
    }

    /// <summary>
    /// 转换项集合,文本设置为Description，值为Value
    /// </summary>
    /// <param name="instance">枚举实例</param>
    public static List<Meow.Model.Item> GetItems( this System.Enum instance ) {
        return Meow.Helper.Enum.GetItems( instance );
    }

    /// <summary>
    /// 获取名称集合
    /// </summary>
    /// <param name="instance">枚举实例</param>
    public static List<string> GetNames( this System.Enum instance ) {
        return Meow.Helper.Enum.GetNames( instance );
    }

    /// <summary>
    /// 获取字典,文本设置为Description，Key为Value
    /// </summary>
    /// <param name="instance">枚举实例</param>
    public static Dictionary<int , string> GetDictionary( this System.Enum instance ) {
        return Meow.Helper.Enum.GetDictionary( instance );
    }

    /// <summary>
    /// 转换（标识、名）集合
    /// </summary>
    /// <param name="instance">枚举实例</param>
    public static List<Meow.Model.IdName<int?>> GetIdNames( this System.Enum instance ) {
        return Meow.Helper.Enum.GetIdNames( instance );
    }

    /// <summary>
    /// 转换（标识、名、描述）集合
    /// </summary>
    /// <param name="instance">枚举实例</param>
    public static List<Meow.Model.IdNameDescription<int?>> GetIdNameDescriptions( this System.Enum instance ) {
        return Meow.Helper.Enum.GetIdNameDescriptions( instance );
    }

    /// <summary>
    /// 转换（标识、名）
    /// </summary>
    /// <param name="instance">枚举实例</param>
    public static Meow.Model.IdName<int?> GetIdName( this System.Enum instance ) {
        return Meow.Helper.Enum.GetIdName( instance );
    }

    /// <summary>
    /// 转换（标识、名、描述）
    /// </summary>
    /// <param name="instance">枚举实例</param>
    public static Meow.Model.IdNameDescription<int?> GetIdNameDescription( this System.Enum instance ) {
        return Meow.Helper.Enum.GetIdNameDescription( instance );
    }
}