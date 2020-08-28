using System.Collections.Generic;
using Meow.Extensions.Helpers;

namespace Meow.Helpers
{
    /// <summary>
    /// Guid操作
    /// </summary>
    public class Guid
    {
        /// <summary>
        /// 转换为Guid
        /// </summary>
        /// <param name="input">输入值</param>
        public static System.Guid ToGuid(object input)
        {
            return ToGuidOrNull(input) ?? System.Guid.Empty;
        }

        /// <summary>
        /// 转换为可空Guid
        /// </summary>
        /// <param name="input">输入值</param>
        public static System.Guid? ToGuidOrNull(object input)
        {
            return System.Guid.TryParse(input.SafeString(), out var result) ? (System.Guid?)result : null;
        }

        /// <summary>
        /// 转换为Guid集合
        /// </summary>
        /// <param name="input">以逗号分隔的Guid集合字符串，范例:83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A</param>
        public static List<System.Guid> ToGuidList(string input)
        {
            return Meow.Helpers.String.ToList<System.Guid>(input);
        }
    }
}
