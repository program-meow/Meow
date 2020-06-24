using System.Collections.Generic;
using System.Linq;
using Meow.Extensions.Helpers;
using Meow.Extensions.Validates;

namespace Meow.Mathematics
{
    /// <summary>
    /// 离散数学
    /// </summary>
    public class Discrete
    {
        /// <summary>
        /// 求数组中n个元素的组合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">所求数组</param>
        /// <param name="n">元素个数</param>
        public static List<List<T>> Combination<T>(IEnumerable<T> list, int n)
        {
            var data = list.ToArray();
            var result = new List<List<T>>();
            if (data.IsEmpty() || n < 1 || data.Length < n)
                return result;
            var temp = new int[n];
            Combination(ref result, data, data.Length, n, temp, n);
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
        public static List<string> CombinationJoin<T>(IEnumerable<T> list, int n, string quotes = "", string separator = ",")
        {
            var permutations = Combination(list, n);
            var result = permutations.Select(item => item.Join(quotes, separator)).ToList();
            return result;
        }

        /// <summary>
        /// 求数组中所有元素的组合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">所求数组</param>
        public static List<List<T>> Combination<T>(IEnumerable<T> list)
        {
            var data = list.ToArray();
            if (data.IsEmpty())
                return new List<List<T>>();
            var result = new List<List<T>>();
            for (var i = 1; i <= data.Count(); i++)
            {
                var item = Combination(data, i);
                result.AddRange(item);
            }
            return result;
        }

        /// <summary>
        /// 求数组中所有元素的组合,组合项连接为带分隔符的字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static List<string> CombinationJoin<T>(IEnumerable<T> list, string quotes = "", string separator = ",")
        {
            var permutations = Combination(list);
            var result = permutations.Select(item => item.Join(quotes, separator)).ToList();
            return result;
        }

        /// <summary>
        /// 递归算法求数组的组合(私有成员)
        /// </summary>
        /// <param name="list">返回的范型</param>
        /// <param name="t">所求数组</param>
        /// <param name="n">辅助变量</param>
        /// <param name="m1">辅助变量</param>
        /// <param name="b">辅助数组</param>
        /// <param name="m2">辅助变量M</param>
        private static void Combination<T>(ref List<List<T>> list, T[] t, int n, int m1, int[] b, int m2)
        {
            for (var i = n; i >= m1; i--)
            {
                b[m1 - 1] = i - 1;
                if (m1 > 1)
                {
                    Combination(ref list, t, i - 1, m1 - 1, b, m2);
                }
                else
                {
                    if (list == null)
                    {
                        list = new List<List<T>>();
                    }
                    var temp = new T[m2];
                    for (var j = 0; j < b.Length; j++)
                    {
                        temp[j] = t[b[j]];
                    }
                    list.Add(temp.ToList());
                }
            }
        }

        /// <summary>
        /// 求从起始标号到结束标号的排列，其余元素不变
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">所求数组</param>
        /// <param name="startIndex">起始标号</param>
        /// <param name="endIndex">结束标号</param>
        public static List<List<T>> Permutation<T>(IEnumerable<T> list, int startIndex, int endIndex)
        {
            var data = list.ToArray();
            var result = new List<List<T>>();
            if (data.IsEmpty() || startIndex < 0 || endIndex > data.Length - 1)
                return result;
            Permutation(ref result, data, startIndex, endIndex);
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
        public static List<string> PermutationJoin<T>(IEnumerable<T> list, int startIndex, int endIndex, string quotes = "", string separator = ",")
        {
            var permutations = Permutation(list, startIndex, endIndex);
            var result = permutations.Select(item => item.Join(quotes, separator)).ToList();
            return result;
        }

        /// <summary>
        /// 返回数组所有元素的全排列
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">所求数组</param>
        public static List<List<T>> Permutation<T>(IEnumerable<T> list)
        {
            var data = list.ToArray();
            var result = Permutation(data, 0, data.Length - 1);
            return result;
        }

        /// <summary>
        /// 返回数组所有元素的全排列,组合项连接为带分隔符的字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static List<string> PermutationJoin<T>(IEnumerable<T> list, string quotes = "", string separator = ",")
        {
            var permutations = Permutation(list);
            var result = permutations.Select(item => item.Join(quotes, separator)).ToList();
            return result;
        }

        /// <summary>
        /// 递归算法求排列(私有成员)
        /// </summary>
        /// <param name="list">返回的列表</param>
        /// <param name="t">所求数组</param>
        /// <param name="startIndex">起始标号</param>
        /// <param name="endIndex">结束标号</param>
        private static void Permutation<T>(ref List<List<T>> list, T[] t, int startIndex, int endIndex)
        {
            if (startIndex == endIndex)
            {
                if (list == null)
                {
                    list = new List<List<T>>();
                }
                var temp = new T[t.Length];
                t.CopyTo(temp, 0);
                list.Add(temp.ToList());
            }
            else
            {
                for (var i = startIndex; i <= endIndex; i++)
                {
                    Meow.Mathematics.Common.Swap(ref t[startIndex], ref t[i]);
                    Permutation(ref list, t, startIndex + 1, endIndex);
                    Meow.Mathematics.Common.Swap(ref t[startIndex], ref t[i]);
                }
            }
        }

        /// <summary>
        /// 求数组中n个元素的排列组合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">所求数组</param>
        /// <param name="n">元素个数</param>
        public static List<List<T>> PermutationCombination<T>(IEnumerable<T> list, int n)
        {
            var data = list.ToArray();
            var result = new List<List<T>>();
            if (data.IsEmpty() || n > data.Count())
                return result;
            var combinations = Combination(data, n);
            foreach (var item in combinations)
            {
                var caches = new List<List<T>>();
                Permutation(ref caches, item.ToArray(), 0, n - 1);
                result.AddRange(caches);
            }
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
        public static List<string> PermutationCombinationJoin<T>(IEnumerable<T> list, int n, string quotes = "", string separator = ",")
        {
            var permutations = PermutationCombination(list, n);
            var result = permutations.Select(item => item.Join(quotes, separator)).ToList();
            return result;
        }

        /// <summary>
        /// 求数组中所有元素的排列组合
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">所求数组</param>
        public static List<List<T>> PermutationCombination<T>(IEnumerable<T> list)
        {
            var data = list.ToArray();
            if (data.IsEmpty())
                return new List<List<T>>();
            var result = new List<List<T>>();
            for (var i = 1; i <= data.Count(); i++)
            {
                var item = PermutationCombination(data, i);
                result.AddRange(item);
            }
            return result;
        }

        /// <summary>
        /// 求数组中所有元素的排列组合,组合项连接为带分隔符的字符串
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static List<string> PermutationCombinationJoin<T>(IEnumerable<T> list, string quotes = "", string separator = ",")
        {
            var permutations = PermutationCombination(list);
            var result = permutations.Select(item => item.Join(quotes, separator)).ToList();
            return result;
        }
    }
}
