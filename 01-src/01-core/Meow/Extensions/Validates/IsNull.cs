namespace Meow.Extensions.Validates
{
    /// <summary>
    /// 是否为NUll扩展
    /// </summary>
    public static partial class Extensions
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
