using System;

namespace Meow.Extension.Helper
{
    /// <summary>
    /// 枚举扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 转换为枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">值</param>
        public static TEnum ToEnum<TEnum>(this string value) where TEnum : Enum
        {
            return Meow.Helper.Enum.Parse<TEnum>(value);
        }

        /// <summary>
        /// 转换为枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">值</param>
        public static TEnum ToEnum<TEnum>(this int value) where TEnum : Enum
        {
            return Meow.Helper.Enum.Parse<TEnum>(value);
        }

        /// <summary>
        /// 转换为枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="value">值</param>
        public static TEnum ToEnum<TEnum>(this int? value) where TEnum : Enum
        {
            return Meow.Helper.Enum.Parse<TEnum>(value);
        }

        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static int Value(this System.Enum instance)
        {
            if (instance == null)
                return 0;
            return Meow.Helper.Enum.GetValue(instance.GetType(), instance);
        }

        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="instance">枚举实例</param>
        public static TResult Value<TResult>(this System.Enum instance)
        {
            if (instance == null)
                return default(TResult);
            return Meow.Helper.Common.To<TResult>(Value(instance));
        }

        /// <summary>
        /// 获取枚举描述,使用System.ComponentModel.Description特性设置描述
        /// </summary>
        /// <param name="instance">枚举实例</param>
        public static string Description(this System.Enum instance)
        {
            if (instance == null)
                return string.Empty;
            return Meow.Helper.Enum.GetDescription(instance.GetType(), instance);
        }
    }
}
