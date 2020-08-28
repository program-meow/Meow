using System.Collections.Generic;

namespace Meow.Extension.Mathematics
{
    /// <summary>
    /// 集合类型扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 求数组中n个元素的组合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">所求数组</param>
        /// <param name="n">元素个数</param>
        public static List<List<T>> Combination<T>(this IEnumerable<T> list, int n)
        {
            var result = Meow.Mathematics.Discrete.Combination(list, n);
            return result;

        }

        /// <summary>
        /// 求数组中n个元素的组合,组合项连接为带分隔符的字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="n">元素个数</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static List<string> CombinationJoin<T>(this IEnumerable<T> list, int n, string quotes = "", string separator = ",")
        {
            var result = Meow.Mathematics.Discrete.CombinationJoin(list, n, quotes, separator);
            return result;
        }

        /// <summary>
        /// 求数组中所有元素的组合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">所求数组</param>
        public static List<List<T>> Combination<T>(this IEnumerable<T> list)
        {
            var result = Meow.Mathematics.Discrete.Combination(list);
            return result;
        }

        /// <summary>
        /// 求数组中所有元素的组合,组合项连接为带分隔符的字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static List<string> CombinationJoin<T>(this IEnumerable<T> list, string quotes = "", string separator = ",")
        {
            var result = Meow.Mathematics.Discrete.CombinationJoin(list, quotes, separator);
            return result;
        }

        /// <summary>
        /// 求从起始标号到结束标号的排列，其余元素不变
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">所求数组</param>
        /// <param name="startIndex">起始标号</param>
        /// <param name="endIndex">结束标号</param>
        public static List<List<T>> Permutation<T>(this IEnumerable<T> list, int startIndex, int endIndex)
        {
            var result = Meow.Mathematics.Discrete.Permutation(list, startIndex, endIndex);
            return result;
        }

        /// <summary>
        /// 求从起始标号到结束标号的排列，其余元素不变,组合项连接为带分隔符的字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="startIndex">起始标号</param>
        /// <param name="endIndex">结束标号</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static List<string> PermutationJoin<T>(this IEnumerable<T> list, int startIndex, int endIndex, string quotes = "", string separator = ",")
        {
            var result = Meow.Mathematics.Discrete.PermutationJoin(list, startIndex, endIndex, quotes, separator);
            return result;
        }

        /// <summary>
        /// 返回数组所有元素的全排列
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">所求数组</param>
        public static List<List<T>> Permutation<T>(this IEnumerable<T> list)
        {
            var result = Meow.Mathematics.Discrete.Permutation(list);
            return result;
        }

        /// <summary>
        /// 返回数组所有元素的全排列,组合项连接为带分隔符的字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static List<string> PermutationJoin<T>(this IEnumerable<T> list, string quotes = "", string separator = ",")
        {
            var result = Meow.Mathematics.Discrete.PermutationJoin(list, quotes, separator);
            return result;
        }

        /// <summary>
        /// 求数组中n个元素的排列组合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">所求数组</param>
        /// <param name="n">元素个数</param>
        public static List<List<T>> PermutationCombination<T>(this IEnumerable<T> list, int n)
        {
            var result = Meow.Mathematics.Discrete.PermutationCombination(list, n);
            return result;
        }

        /// <summary>
        /// 求数组中n个元素的排列组合,组合项连接为带分隔符的字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="n">元素个数</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static List<string> PermutationCombinationJoin<T>(this IEnumerable<T> list, int n, string quotes = "", string separator = ",")
        {
            var result = Meow.Mathematics.Discrete.PermutationCombinationJoin(list, n, quotes, separator);
            return result;
        }

        /// <summary>
        /// 求数组中所有元素的排列组合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">所求数组</param>
        public static List<List<T>> PermutationCombination<T>(this IEnumerable<T> list)
        {
            var result = Meow.Mathematics.Discrete.PermutationCombination(list);
            return result;
        }

        /// <summary>
        /// 求数组中所有元素的排列组合,组合项连接为带分隔符的字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static List<string> PermutationCombinationJoin<T>(this IEnumerable<T> list, string quotes = "", string separator = ",")
        {
            var result = Meow.Mathematics.Discrete.PermutationCombinationJoin(list, quotes, separator);
            return result;
        }
    }
}
