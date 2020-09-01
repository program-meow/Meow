using Meow.Extension.Helper;

namespace Meow.Helper
{
    /// <summary>
    /// 32位整型操作
    /// </summary>
    public static class Init
    {
        /// <summary>
        /// 转换为32位整型
        /// </summary>
        /// <param name="value">值</param>
        public static int ToInt(object value)
        {
            return ToIntOrNull(value) ?? 0;
        }

        /// <summary>
        /// 转换为32位可空整型
        /// </summary>
        /// <param name="value">值</param>
        public static int? ToIntOrNull(object value)
        {
            var success = int.TryParse(value.SafeString(), out var result);
            if (success)
                return result;
            try
            {
                var temp = Double.ToDoubleOrNull(value, 0);
                if (temp == null)
                    return null;
                return System.Convert.ToInt32(temp);
            }
            catch
            {
                return null;
            }
        }
    }
}