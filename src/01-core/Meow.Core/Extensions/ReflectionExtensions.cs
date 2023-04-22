using System;
using Meow.Types;

namespace Meow.Extensions
{
    /// <summary>
    /// 反射扩展
    /// </summary>
    public static class ReflectionExtensions
    {
        /// <summary>
        /// 获取类型枚举
        /// </summary>
        /// <param name="type">类型</param>
        public static TypeEnum? GetTypeEnum(this Type type)
        {
            return Meow.Helpers.Reflection.GetTypeEnum(type);
        }
    }
}
