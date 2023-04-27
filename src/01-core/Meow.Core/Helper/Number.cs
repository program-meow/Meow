using System.Linq;

namespace Meow.Helper
{
    /// <summary>
    /// 数字操作
    /// </summary>
    public static class Number
    {
        /// <summary>
        /// 取最小值
        /// </summary>
        /// <param name="number">数字</param>
        public static int? Minimum(params int[] number)
        {
            if (number == null)
                return null;
            return number.Min();
        }
    }
}
