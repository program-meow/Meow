using Meow.Exception;
using Meow.Extension.Helper;
using Meow.Parameter.Enum;

namespace Meow.Extension.Parameter.Enum
{
    /// <summary>
    /// 类型枚举扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 转换类型中精度枚举
        /// </summary>
        /// <param name="value">类型高精度枚举</param>
        public static TypeMediumPrecision ToMedium(this TypeHighPrecision value)
        {
            switch (value)
            {
                default:
                    throw new Warning("超出范围");
            }
        }

        /// <summary>
        /// 转换类型中精度枚举
        /// </summary>
        /// <param name="value">类型高精度枚举</param>
        public static TypeMediumPrecision? ToMedium(this TypeHighPrecision? value)
        {
            if (value.IsNull())
                return null;
            return value.SafeValue().ToMedium();
        }

        /// <summary>
        /// 转换类型低精度枚举
        /// </summary>
        /// <param name="value">类型高精度枚举</param>
        public static TypeLowPrecision ToLow(this TypeHighPrecision value)
        {
            switch (value)
            {
                
                default:
                    throw new Warning("超出范围");
            }
        }

        /// <summary>
        /// 转换类型低精度枚举
        /// </summary>
        /// <param name="value">类型高精度枚举</param>
        public static TypeLowPrecision? ToLow(this TypeHighPrecision? value)
        {
            if (value.IsNull())
                return null;
            return value.SafeValue().ToLow();
        }


    }
}