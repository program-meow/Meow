using System.Collections.Generic;

namespace Meow.Extensions
{
    /// <summary>
    /// 随机数扩展
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// 从集合中随机获取值
        /// </summary>
        /// <param name="array">集合</param>
        public static T GetRandomValue<T>(this IEnumerable<T> array)
        {
            return Meow.Helpers.Random.GetValue<T>(array);
        }

        /// <summary>
        /// 对集合随机排序
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="array">集合</param>
        public static List<T> OrderByRandom<T>(this IEnumerable<T> array)
        {
            return Meow.Helpers.Random.Order<T>(array);
        }
    }
}
