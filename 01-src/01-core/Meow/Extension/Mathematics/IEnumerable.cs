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
        /// <param name="value">值</param>
        /// <param name="n">元素个数</param>
        public static List<List<T>> Combination<T>(this IEnumerable<T> value, int n)
        {
            return Meow.Mathematics.Discrete.Combination(value, n);
        }

        /// <summary>
        /// 求数组中n个元素的组合,组合项连接为带分隔符的字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="n">元素个数</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static List<string> CombinationJoin<T>(this IEnumerable<T> value, int n, string quotes = "", string separator = ",")
        {
            return Meow.Mathematics.Discrete.CombinationJoin(value, n, quotes, separator);
        }

        /// <summary>
        /// 求数组中所有元素的组合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="value">值</param>
        public static List<List<T>> Combination<T>(this IEnumerable<T> value)
        {
            return Meow.Mathematics.Discrete.Combination(value);
        }

        /// <summary>
        /// 求数组中所有元素的组合,组合项连接为带分隔符的字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static List<string> CombinationJoin<T>(this IEnumerable<T> value, string quotes = "", string separator = ",")
        {
            return Meow.Mathematics.Discrete.CombinationJoin(value, quotes, separator);
        }

        /// <summary>
        /// 求从起始标号到结束标号的排列，其余元素不变
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="startIndex">起始标号</param>
        /// <param name="endIndex">结束标号</param>
        public static List<List<T>> Permutation<T>(this IEnumerable<T> value, int startIndex, int endIndex)
        {
            return Meow.Mathematics.Discrete.Permutation(value, startIndex, endIndex);
        }

        /// <summary>
        /// 求从起始标号到结束标号的排列，其余元素不变,组合项连接为带分隔符的字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="startIndex">起始标号</param>
        /// <param name="endIndex">结束标号</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static List<string> PermutationJoin<T>(this IEnumerable<T> value, int startIndex, int endIndex, string quotes = "", string separator = ",")
        {
            return Meow.Mathematics.Discrete.PermutationJoin(value, startIndex, endIndex, quotes, separator);
        }

        /// <summary>
        /// 返回数组所有元素的全排列
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="value">值</param>
        public static List<List<T>> Permutation<T>(this IEnumerable<T> value)
        {
            return Meow.Mathematics.Discrete.Permutation(value);
        }

        /// <summary>
        /// 返回数组所有元素的全排列,组合项连接为带分隔符的字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static List<string> PermutationJoin<T>(this IEnumerable<T> value, string quotes = "", string separator = ",")
        {
            return Meow.Mathematics.Discrete.PermutationJoin(value, quotes, separator);
        }

        /// <summary>
        /// 求数组中n个元素的排列组合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="n">元素个数</param>
        public static List<List<T>> PermutationCombination<T>(this IEnumerable<T> value, int n)
        {
            return Meow.Mathematics.Discrete.PermutationCombination(value, n);
        }

        /// <summary>
        /// 求数组中n个元素的排列组合,组合项连接为带分隔符的字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="n">元素个数</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static List<string> PermutationCombinationJoin<T>(this IEnumerable<T> value, int n, string quotes = "", string separator = ",")
        {
            return Meow.Mathematics.Discrete.PermutationCombinationJoin(value, n, quotes, separator);
        }

        /// <summary>
        /// 求数组中所有元素的排列组合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="value">值</param>
        public static List<List<T>> PermutationCombination<T>(this IEnumerable<T> value)
        {
            return Meow.Mathematics.Discrete.PermutationCombination(value);
        }

        /// <summary>
        /// 求数组中所有元素的排列组合,组合项连接为带分隔符的字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static List<string> PermutationCombinationJoin<T>(this IEnumerable<T> value, string quotes = "", string separator = ",")
        {
            return Meow.Mathematics.Discrete.PermutationCombinationJoin(value, quotes, separator);
        }
    }
}