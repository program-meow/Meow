using System.Linq;
using System.Collections.Generic;
using Meow.Extension.Helper;
using Meow.Parameter.Object;

namespace Meow.Extension.Parameter.Object
{
    /// <summary>
    /// 树形对象扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 转换委托
        /// </summary>
        /// <typeparam name="TIn">输入元素类型</typeparam>
        /// <typeparam name="TOut">输出元素类型</typeparam>
        /// <param name="vale">值</param>
        public delegate TOut ToHandler<in TIn, out TOut>(TIn vale);

        /// <summary>
        /// 转换
        /// </summary>
        /// <typeparam name="TIn">输入元素类型</typeparam>
        /// <typeparam name="TOut">输出元素类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="handler">委托方法</param>
        public static Tree<TOut> To<TIn, TOut>(this Tree<TIn> value, ToHandler<TIn, TOut> handler)
        {
            if (value == null)
                return new Tree<TOut>();
            if (value.Subsets.IsEmpty())
                return new Tree<TOut>(handler(value.Data), null, value.SortId);
            var subsets = new List<Tree<TOut>>();
            foreach (var item in value.Subsets)
                subsets.Add(item.To(handler));
            return new Tree<TOut>(handler(value.Data), subsets.OrderBy(t => t.SortId).ToList(), value.SortId);
        }

        /// <summary>
        /// 转换
        /// </summary>
        /// <typeparam name="TIn">输入元素类型</typeparam>
        /// <typeparam name="TOut">输出元素类型</typeparam>
        /// <param name="value">值</param>
        /// <param name="handler">委托方法</param>
        public static List<Tree<TOut>> To<TIn, TOut>(this List<Tree<TIn>> value, ToHandler<TIn, TOut> handler)
        {
            if (value == null)
                return new List<Tree<TOut>>();
            return value.Select(t => t.To(handler)).OrderBy(t => t.SortId).ToList();
        }
    }
}