using System.Collections.Generic;

namespace Meow.Extension
{
    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 枚举 - 获取实例
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="member">成员名或值,范例:Enum1枚举有成员A=0,则传入"A"或"0"获取 Enum1.A</param>
        public static TEnum ParseEnum<TEnum>(this object member)
        {
            return Meow.Helper.Enum.Parse<TEnum>(member);
        }

        /// <summary>
        /// 获取成员名
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static string GetName(this System.Enum instance)
        {
            if (instance == null)
                return string.Empty;
            return Meow.Helper.Enum.GetName(instance.GetType(), instance);
        }

        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static int? GetValue(this System.Enum instance)
        {
            if (instance == null)
                return null;
            return Meow.Helper.Enum.GetValue(instance.GetType(), instance);
        }

        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="instance">枚举实例</param>
        public static TResult GetValue<TResult>(this System.Enum instance)
        {
            if (instance == null)
                return default;
            return Meow.Helper.Convert.To<TResult>(GetValue(instance));
        }

        /// <summary>
        /// 获取枚举描述,使用System.ComponentModel.Description特性设置描述
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static string GetDescription(this System.Enum instance)
        {
            if (instance == null)
                return string.Empty;
            return Meow.Helper.Enum.GetDescription(instance.GetType(), instance);
        }

        /// <summary>
        /// 转换项集合,文本设置为Description，值为Value
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static List<Meow.Model.Item> ToItemsList(this System.Enum instance)
        {
            if (instance == null)
                return new List<Meow.Model.Item>();
            return Meow.Helper.Enum.ToItemsList(instance.GetType());
        }

        /// <summary>
        /// 获取名称集合
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static List<string> GetNames(this System.Enum instance)
        {
            if (instance == null)
                return new List<string>();
            return Meow.Helper.Enum.GetNames(instance.GetType());
        }

        /// <summary>
        /// 获取字典,文本设置为Description，Key为Value
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static Dictionary<int, string> ToDictionary(this System.Enum instance)
        {
            if (instance == null)
                return new Dictionary<int, string>();
            return Meow.Helper.Enum.ToDictionary(instance.GetType());
        }

        /// <summary>
        /// 转换（标识、名）集合
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static List<Meow.Model.IdName<int?>> ToIdNameList(this System.Enum instance)
        {
            if (instance == null)
                return new List<Meow.Model.IdName<int?>>();
            return Meow.Helper.Enum.ToIdNameList(instance.GetType());
        }

        /// <summary>
        /// 转换（标识、名、描述）集合
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static List<Meow.Model.IdNameDesc<int?>> ToIdNameDescList(this System.Enum instance)
        {
            if (instance == null)
                return new List<Meow.Model.IdNameDesc<int?>>();
            return Meow.Helper.Enum.ToIdNameDescList(instance.GetType());
        }

        /// <summary>
        /// 转换（标识、名）
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static Meow.Model.IdName<int?> ToIdName(this System.Enum instance)
        {
            if (instance == null)
                return new Meow.Model.IdName<int?>(null, string.Empty);
            return Meow.Helper.Enum.ToIdName(instance.GetType(), instance);
        }

        /// <summary>
        /// 转换（标识、名、描述）
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static Meow.Model.IdNameDesc<int?> ToIdNameDesc(this System.Enum instance)
        {
            if (instance == null)
                return new Meow.Model.IdNameDesc<int?>(null, string.Empty, string.Empty);
            return Meow.Helper.Enum.ToIdNameDesc(instance.GetType(), instance);
        }
    }
}
