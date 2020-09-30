namespace Meow.Extension.Helper
{
    /// <summary>
    /// 布尔值扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="value">布尔值</param>
        public static string Description(this bool value)
        {
            return value ? "是" : "否";
        }

        /// <summary>
        /// 获取描述
        /// </summary>
        /// <param name="value">布尔值</param>
        public static string Description(this bool? value)
        {
            return value == null ? "" : Description(value.Value);
        }
    }
}