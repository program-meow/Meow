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
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">值</param>
        public static T ToEnum<T>(this string value) where T : Enum
        {
            return Meow.Helper.Enum.Parse<T>(value);
        }

        /// <summary>
        /// 转换为枚举
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">值</param>
        public static T ToEnum<T>(this int value) where T : Enum
        {
            return Meow.Helper.Enum.Parse<T>(value);
        }

        /// <summary>
        /// 转换为枚举
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">值</param>
        public static T ToEnum<T>(this int? value) where T : Enum
        {
            return Meow.Helper.Enum.Parse<T>(value);
        }

        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <param name="value">值</param>
        public static int Value(this System.Enum value)
        {
            if (value == null)
                return 0;
            return Meow.Helper.Enum.GetValue(value.GetType(), value);
        }

        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="value">值</param>
        public static T Value<T>(this System.Enum value)
        {
            if (value == null)
                return default(T);
            return Meow.Helper.Common.To<T>(Value(value));
        }

        /// <summary>
        /// 获取枚举描述,使用System.ComponentModel.Description特性设置描述
        /// </summary>
        /// <param name="value">值</param>
        public static string Description(this System.Enum value)
        {
            if (value == null)
                return string.Empty;
            return Meow.Helper.Enum.GetDescription(value.GetType(), value);
        }
    }
}