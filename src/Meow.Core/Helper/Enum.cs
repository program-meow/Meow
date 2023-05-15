using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Meow.Extension;
using SystemType = System.Type;

namespace Meow.Helper;

/// <summary>
/// 枚举操作
/// </summary>
public static class Enum
{
    /// <summary>
    /// 获取实例
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    /// <param name="member">成员名或值,范例:Enum1枚举有成员A=0,则传入"A"或"0"获取 Enum1.A</param>
    public static TEnum Parse<TEnum>(object member)
    {
        string value = Common.SafeString(member);
        if (value.IsEmpty())
        {
            if (typeof(TEnum).IsGenericType)
                return default(TEnum);
            throw new ArgumentNullException(nameof(member));
        }
        return (TEnum)System.Enum.Parse(Common.GetType<TEnum>(), value, true);
    }

    /// <summary>
    /// 获取成员名
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    /// <param name="member">成员名、值、实例均可,范例:Enum1枚举有成员A=0,则传入Enum1.A或0,获取成员名"A"</param>
    public static string GetName<TEnum>(object member)
    {
        return GetName(Common.GetType<TEnum>(), member);
    }

    /// <summary>
    /// 获取成员名
    /// </summary>
    /// <param name="instance">枚举实例</param>
    public static string GetName(System.Enum instance)
    {
        if (instance == null)
            return string.Empty;
        return GetName(instance.GetType(), instance);
    }

    /// <summary>
    /// 获取成员名
    /// </summary>
    /// <param name="type">枚举类型</param>
    /// <param name="member">成员名、值、实例均可</param>
    public static string GetName(SystemType type, object member)
    {
        if (type == null)
            return string.Empty;
        if (member == null)
            return string.Empty;
        if (member is string)
            return member.ToString();
        if (type.GetTypeInfo().IsEnum == false)
            return string.Empty;
        return System.Enum.GetName(type, member);
    }

    /// <summary>
    /// 获取成员值
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    /// <param name="member">成员名、值、实例均可，范例:Enum1枚举有成员A=0,可传入"A"、0、Enum1.A，获取值0</param>
    public static int? GetValue<TEnum>(object member)
    {
        return GetValue(Common.GetType<TEnum>(), member);
    }

    /// <summary>
    /// 获取枚举值
    /// </summary>
    /// <param name="instance">枚举实例</param>
    public static int? GetValue(System.Enum instance)
    {
        if (instance == null)
            return null;
        return GetValue(instance.GetType(), instance);
    }

    /// <summary>
    /// 获取枚举值
    /// </summary>
    /// <typeparam name="TResult">返回值类型</typeparam>
    /// <param name="instance">枚举实例</param>
    public static TResult GetValue<TResult>(System.Enum instance)
    {
        if (instance == null)
            return default;
        return GetValue(instance).To<TResult>();
    }

    /// <summary>
    /// 获取成员值
    /// </summary>
    /// <param name="type">枚举类型</param>
    /// <param name="member">成员名、值、实例均可</param>
    public static int? GetValue(SystemType type, object member)
    {
        string value = Common.SafeString(member);
        if (value.IsEmpty())
            return null;
        object result = System.Enum.Parse(type, value, true);
        return Convert.To<int?>(result);
    }

    /// <summary>
    /// 获取描述,使用System.ComponentModel.Description特性设置描述
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    /// <param name="member">成员名、值、实例均可</param>
    public static string GetDescription<TEnum>(object member)
    {
        return Reflection.GetDescription<TEnum>(GetName<TEnum>(member));
    }

    /// <summary>
    /// 获取枚举描述,使用System.ComponentModel.Description特性设置描述
    /// </summary>
    /// <param name="instance">枚举实例</param>
    public static string GetDescription(System.Enum instance)
    {
        if (instance == null)
            return string.Empty;
        return GetDescription(instance.GetType(), instance);
    }

    /// <summary>
    /// 获取描述,使用System.ComponentModel.Description特性设置描述
    /// </summary>
    /// <param name="type">枚举类型</param>
    /// <param name="member">成员名、值、实例均可</param>
    public static string GetDescription(SystemType type, object member)
    {
        return Reflection.GetDescription(type, GetName(type, member));
    }

    /// <summary>
    /// 获取项集合,文本设置为Description，值为Value
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    public static List<Meow.Model.Item> ToItemsList<TEnum>()
    {
        return ToItemsList(typeof(TEnum));
    }

    /// <summary>
    /// 转换项集合,文本设置为Description，值为Value
    /// </summary>
    /// <param name="instance">枚举实例</param>
    public static List<Meow.Model.Item> ToItemsList(System.Enum instance)
    {
        if (instance == null)
            return new List<Meow.Model.Item>();
        return ToItemsList(instance.GetType());
    }

    /// <summary>
    /// 获取项集合,文本设置为Description，值为Value
    /// </summary>
    /// <param name="type">枚举类型</param>
    public static List<Meow.Model.Item> ToItemsList(SystemType type)
    {
        type = Common.GetType(type);
        if (type.IsEnum == false)
            throw new InvalidOperationException(string.Format(Meow.Error.ErrorMessageKey.TypeNotEnum, type));
        var result = new List<Meow.Model.Item>();
        foreach (var field in type.GetFields())
            AddItem(type, result, field);
        return result.OrderBy(t => t.SortId).ToList();
    }

    /// <summary>
    /// 添加描述项
    /// </summary>
    private static void AddItem(SystemType type, ICollection<Meow.Model.Item> result, FieldInfo field)
    {
        if (!field.FieldType.IsEnum)
            return;
        var value = GetValue(type, field.Name);
        var description = Reflection.GetDescription(field);
        result.Add(new Meow.Model.Item(description, value, value));
    }

    /// <summary>
    /// 获取名称集合
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    public static List<string> ToNameList<TEnum>()
    {
        return ToNameList(typeof(TEnum));
    }

    /// <summary>
    /// 获取名称集合
    /// </summary>
    /// <param name="instance">枚举实例</param>
    public static List<string> ToNameList(System.Enum instance)
    {
        if (instance == null)
            return new List<string>();
        return ToNameList(instance.GetType());
    }

    /// <summary>
    /// 获取名称集合
    /// </summary>
    /// <param name="type">枚举类型</param>
    public static List<string> ToNameList(SystemType type)
    {
        type = Common.GetType(type);
        if (type.IsEnum == false)
            throw new InvalidOperationException(string.Format(Meow.Error.ErrorMessageKey.TypeNotEnum, type));
        var result = new List<string>();
        foreach (var field in type.GetFields())
        {
            if (!field.FieldType.IsEnum)
                continue;
            result.Add(field.Name);
        }
        return result;
    }

    /// <summary>
    /// 获取字典,文本设置为Description，Key为Value
    /// </summary>
    /// <param name="instance">枚举实例</param>
    public static Dictionary<int, string> ToDictionary(System.Enum instance)
    {
        if (instance == null)
            return new Dictionary<int, string>();
        return ToDictionary(instance.GetType());
    }

    /// <summary>
    /// 获取字典
    /// </summary>
    public static Dictionary<int, string> ToDictionary(SystemType type)
    {
        Dictionary<int, string> list = new Dictionary<int, string>();
        if (type.IsEnum == false)
            return list;
        SystemType typeDescription = typeof(DescriptionAttribute);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (FieldInfo field in fields)
        {
            if (field.IsSpecialName)
                continue;
            var key = Convert.ToInt(field.GetRawConstantValue());
            object[] arr = field.GetCustomAttributes(typeDescription, true);
            var text = arr.Length > 0 ? (arr[0] as DescriptionAttribute)?.Description : field.Name;
            list.Add(key, text);
        }
        return list;
    }

    /// <summary>
    /// 获取（标识、名）集合
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    public static List<Meow.Model.IdName<int?>> ToIdNameList<TEnum>()
    {
        return ToIdNameList(typeof(TEnum));
    }

    /// <summary>
    /// 转换（标识、名）集合
    /// </summary>
    /// <param name="instance">枚举实例</param>
    public static List<Meow.Model.IdName<int?>> ToIdNameList(System.Enum instance)
    {
        if (instance == null)
            return new List<Meow.Model.IdName<int?>>();
        return ToIdNameList(instance.GetType());
    }

    /// <summary>
    /// 转换（标识、名）集合
    /// </summary>
    /// <param name="type">枚举类型</param>
    public static List<Meow.Model.IdName<int?>> ToIdNameList(SystemType type)
    {
        type = Common.GetType(type);
        if (type.IsEnum == false)
            throw new InvalidOperationException(string.Format(Meow.Error.ErrorMessageKey.TypeNotEnum, type));
        var result = new List<Meow.Model.IdName<int?>>();
        foreach (var field in type.GetFields())
            AddIdName(type, result, field);
        return result.OrderBy(t => t.Id).ToList();
    }

    /// <summary>
    /// 转换（标识、名）集合
    /// </summary>
    private static void AddIdName(SystemType type, ICollection<Meow.Model.IdName<int?>> result, FieldInfo field)
    {
        if (!field.FieldType.IsEnum)
            return;
        var value = GetValue(type, field.Name);
        var name = GetName(type, field.Name);
        result.Add(new Meow.Model.IdName<int?>(value, name));
    }

    /// <summary>
    /// 转换（标识、名、描述）集合
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    public static List<Meow.Model.IdNameDesc<int?>> ToIdNameDescList<TEnum>()
    {
        return ToIdNameDescList(typeof(TEnum));
    }

    /// <summary>
    /// 转换（标识、名、描述）集合
    /// </summary>
    /// <param name="instance">枚举实例</param>
    public static List<Meow.Model.IdNameDesc<int?>> ToIdNameDescList(System.Enum instance)
    {
        if (instance == null)
            return new List<Meow.Model.IdNameDesc<int?>>();
        return ToIdNameDescList(instance.GetType());
    }

    /// <summary>
    /// 转换（标识、名、描述）集合
    /// </summary>
    /// <param name="type">枚举类型</param>
    public static List<Meow.Model.IdNameDesc<int?>> ToIdNameDescList(SystemType type)
    {
        type = Common.GetType(type);
        if (type.IsEnum == false)
            throw new InvalidOperationException(string.Format(Meow.Error.ErrorMessageKey.TypeNotEnum, type));
        var result = new List<Meow.Model.IdNameDesc<int?>>();
        foreach (var field in type.GetFields())
            AddIdNameDesc(type, result, field);
        return result.OrderBy(t => t.Id).ToList();
    }

    /// <summary>
    /// 添加描述项
    /// </summary>
    private static void AddIdNameDesc(SystemType type, ICollection<Meow.Model.IdNameDesc<int?>> result, FieldInfo field)
    {
        if (!field.FieldType.IsEnum)
            return;
        var value = GetValue(type, field.Name);
        var name = GetName(type, field.Name);
        var description = Reflection.GetDescription(field);
        result.Add(new Meow.Model.IdNameDesc<int?>(value, name, description));
    }

    /// <summary>
    /// 转换（标识、名）
    /// </summary>
    /// <param name="instance">枚举实例</param>
    public static Meow.Model.IdName<int?> ToIdName(System.Enum instance)
    {
        if (instance == null)
            return new Meow.Model.IdName<int?>(null, string.Empty);
        return ToIdName(instance.GetType(), instance);
    }

    /// <summary>
    /// 转换（标识、名）
    /// </summary>
    /// <param name="type">枚举类型</param>
    /// <param name="member">成员名、值、实例均可</param>
    public static Meow.Model.IdName<int?> ToIdName(SystemType type, object member)
    {
        var value = GetValue(type, member);
        var name = GetName(type, member);
        return new Meow.Model.IdName<int?>(value, name);
    }

    /// <summary>
    /// 转换（标识、名、描述）
    /// </summary>
    /// <param name="instance">枚举实例</param>
    public static Meow.Model.IdNameDesc<int?> ToIdNameDesc(System.Enum instance)
    {
        if (instance == null)
            return new Meow.Model.IdNameDesc<int?>(null, string.Empty, string.Empty);
        return ToIdNameDesc(instance.GetType(), instance);
    }

    /// <summary>
    /// 转换（标识、名）
    /// </summary>
    /// <param name="type">枚举类型</param>
    /// <param name="member">成员名、值、实例均可</param>
    public static Meow.Model.IdNameDesc<int?> ToIdNameDesc(SystemType type, object member)
    {
        var value = GetValue(type, member);
        var name = GetName(type, member);
        var description = GetDescription(type, member);
        return new Meow.Model.IdNameDesc<int?>(value, name, description);
    }
}