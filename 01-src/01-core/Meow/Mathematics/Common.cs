namespace Meow.Mathematics
{
    /// <summary>
    /// 公共 - 数学
    /// </summary>
    public class Common
    {
        /// <summary>
        /// 交换两个变量
        /// </summary>
        /// <param name="a">变量1</param>
        /// <param name="b">变量2</param>
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }
}
