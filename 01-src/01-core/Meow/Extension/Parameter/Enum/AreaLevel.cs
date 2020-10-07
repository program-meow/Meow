using Meow.Exception;
using Meow.Extension.Helper;
using Meow.Extension.Validation;
using Meow.Parameter.Enum;

namespace Meow.Extension.Parameter.Enum
{
    /// <summary>
    /// 地区等级枚举扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 下级地区等级
        /// </summary>
        /// <param name="value">地区等级</param>
        public static AreaLevel LowerLevel(this AreaLevel value)
        {
            if (value == AreaLevel.Town)
                throw new Warning("已经是最低地区等级");
            return value + 1;
        }

        /// <summary>
        /// 下级地区等级
        /// </summary>
        /// <param name="value">地区等级</param>
        public static AreaLevel LowerLevel(this AreaLevel? value)
        {
            value.CheckNull(nameof(value));
            return value.SafeValue().LowerLevel();
        }

        /// <summary>
        /// 下级地区等级
        /// </summary>
        /// <param name="value">地区等级</param>
        /// <param name="result">下级地区等级</param>
        public static bool TryLowerLevel(this AreaLevel value, out AreaLevel? result)
        {
            if (value == AreaLevel.Town)
            {
                result = null;
                return false;
            }
            result = value.LowerLevel();
            return true;
        }

        /// <summary>
        /// 下级地区等级
        /// </summary>
        /// <param name="value">地区等级</param>
        /// <param name="result">下级地区等级</param>
        public static bool TryLowerLevel(this AreaLevel? value, out AreaLevel? result)
        {
            value.CheckNull(nameof(value));
            return value.SafeValue().TryLowerLevel(out result);
        }
    }
}