using System.Collections.Generic;
using Meow.Extension.Helper;

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
        public Tree() : this(default(T))
        {
        }

        /// <summary>
        /// 初始化树
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="sortId">排序号</param>
        public Tree(T data, int? sortId = 1) : this(data, new List<Tree<T>>(), sortId)
        {
        }

        /// <summary>
        /// 初始化树
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="subsets">子集集合</param>
        /// <param name="sortId">排序号</param>
        public Tree(T data, IEnumerable<Tree<T>> subsets, int? sortId = 1)
        {
            Subsets = new List<Tree<T>>();
            Data = data;
            AddSubset(subsets);
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
            Subsets.AddNoNull(subset);
            return this;
        }

        /// <summary>
        /// 添加子集
        /// </summary>
        /// <param name="subsets">子集集合</param>
        public Tree<T> AddSubset(IEnumerable<Tree<T>> subsets)
        {
            if (subsets.IsNull())
                return this;
            foreach (var item in subsets)
                AddSubset(item);
            return this;
        }
    }
}