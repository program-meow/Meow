namespace Meow.Extension
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
        public static Meow.Type.TypeEnum? GetTypeEnum(this System.Type type)
        {
            return Meow.Helper.Reflection.GetTypeEnum(type);
        }
    }
}
