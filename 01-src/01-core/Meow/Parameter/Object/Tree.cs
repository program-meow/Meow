using System.Collections.Generic;

namespace Meow.Parameter.Object
{
    /// <summary>
    /// 树
    /// </summary>
    public class Tree<T>
    {
        /// <summary>
        /// 初始化树
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="sortId">排序号</param>
        public Tree(T data, int? sortId = null) : this(data, null, sortId)
        {
        }

        /// <summary>
        /// 初始化树
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="subsets">子集</param>
        /// <param name="sortId">排序号</param>
        public Tree(T data, List<Tree<T>> subsets, int? sortId = null)
        {
            Data = data;
            Subsets = subsets ?? new List<Tree<T>>();
            SortId = sortId;
        }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 子集
        /// </summary>
        public List<Tree<T>> Subsets { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int? SortId { get; set; }

        /// <summary>
        /// 添加子集
        /// </summary>
        /// <param name="subset">子集</param>
        public Tree<T> AddSubset(Tree<T> subset)
        {
            Subsets.Add(subset);
            return this;
        }

        /// <summary>
        /// 添加子集
        /// </summary>
        /// <param name="subsets">子集集合</param>
        public Tree<T> AddSubset(List<Tree<T>> subsets)
        {
            Subsets.AddRange(subsets);
            return this;
        }
    }
}