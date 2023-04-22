using Meow.Models;
using System.Collections.Generic;

namespace Meow.Extensions
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
            return Meow.Helpers.Enum.Parse<TEnum>(member);
        }

        /// <summary>
        /// 获取成员名
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static string GetName(this System.Enum instance)
        {
            if (instance == null)
                return string.Empty;
            return Meow.Helpers.Enum.GetName(instance.GetType(), instance);
        }

        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static int? GetValue(this System.Enum instance)
        {
            if (instance == null)
                return null;
            return Meow.Helpers.Enum.GetValue(instance.GetType(), instance);
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
            return Meow.Helpers.Convert.To<TResult>(GetValue(instance));
        }

        /// <summary>
        /// 获取枚举描述,使用System.ComponentModel.Description特性设置描述
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static string GetDescription(this System.Enum instance)
        {
            if (instance == null)
                return string.Empty;
            return Meow.Helpers.Enum.GetDescription(instance.GetType(), instance);
        }

        /// <summary>
        /// 获取项集合,文本设置为Description，值为Value
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static List<Item> GetItems(this System.Enum instance)
        {
            if (instance == null)
                return new List<Item>();
            return Meow.Helpers.Enum.GetItems(instance.GetType());
        }

        /// <summary>
        /// 获取名称集合
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static List<string> GetNames(this System.Enum instance)
        {
            if (instance == null)
                return new List<string>();
            return Meow.Helpers.Enum.GetNames(instance.GetType());
        }
    }
}
