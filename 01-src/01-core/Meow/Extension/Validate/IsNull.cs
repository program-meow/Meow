namespace Meow.Extension.Validate
{
    /// <summary>
    /// 是否为NUll扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 是否为Null
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsNull(this object value)
        {
            return value == null;
        }
    }
}