using System.Collections.Generic;
using Meow.Extension.Helper;

namespace Meow.Helper
{
    /// <summary>
    /// Guid操作
    /// </summary>
    public static class Guid
    {
        /// <summary>
        /// 转换为Guid
        /// </summary>
        /// <param name="value">值</param>
        public static System.Guid ToGuid(object value)
        {
            return ToGuidOrNull(value) ?? System.Guid.Empty;
        }

        /// <summary>
        /// 转换为可空Guid
        /// </summary>
        /// <param name="value">值</param>
        public static System.Guid? ToGuidOrNull(object value)
        {
            return System.Guid.TryParse(value.SafeString(), out var result) ? (System.Guid?)result : null;
        }

        /// <summary>
        /// 转换为Guid集合
        /// </summary>
        /// <param name="value">以逗号分隔的Guid集合字符串，范例:83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A</param>
        public static List<System.Guid> ToGuidList(string value)
        {
            return String.ToList<System.Guid>(value);
        }
    }
}