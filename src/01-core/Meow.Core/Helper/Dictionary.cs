using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Meow.Extension;

namespace Meow.Helper
{
    /// <summary>
    /// 字典操作
    /// </summary>
    public static class Dictionary
    {
        /// <summary>
        /// 将字典连接为带分隔符的字符串
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <typeparam name="TValue">字典值元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="quotes">引号，默认不带引号，范例：单引号 "'"</param>
        /// <param name="separator">分隔符，默认使用逗号分隔</param>
        public static string Join<TKey, TValue>(IDictionary<TKey, TValue> array, string quotes = "", string separator = ",")
        {
            if (array == null)
                return string.Empty;
            StringBuilder result = new StringBuilder();
            foreach (KeyValuePair<TKey, TValue> each in array)
                result.AppendFormat("{0}{1}{0}{2}", quotes, each.Value, separator);
            return String.RemoveEnd(result.ToString(), separator);
        }

        #region Remove

        /// <summary>
        /// 移除null值
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <typeparam name="TValue">字典值元素类型</typeparam>
        /// <param name="array">集合</param>
        public static IDictionary<TKey, TValue> RemoveNull<TKey, TValue>(IDictionary<TKey, TValue> array)
        {
            if (array == null)
                return default(IDictionary<TKey, TValue>);
            IEnumerable<KeyValuePair<TKey, TValue>> valueNullList = array.Where(t => t.Value != null);
            foreach (KeyValuePair<TKey, TValue> each in valueNullList)
                array.Remove(each.Key);
            return array;
        }

        /// <summary>
        /// 移除空值
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <param name="array">集合</param>
        public static IDictionary<TKey, string> RemoveEmpty<TKey>(IDictionary<TKey, string> array)
        {
            if (array == null)
                return default(IDictionary<TKey, string>);
            IEnumerable<KeyValuePair<TKey, string>> valueNullList = array.Where(t => t.Value.IsEmpty());
            foreach (KeyValuePair<TKey, string> each in valueNullList)
                array.Remove(each.Key);
            return array;
        }

        /// <summary>
        /// 移除空值
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <param name="array">集合</param>
        public static IDictionary<TKey, Guid> RemoveEmpty<TKey>(IDictionary<TKey, Guid> array)
        {
            if (array == null)
                return default(IDictionary<TKey, Guid>);
            IEnumerable<KeyValuePair<TKey, Guid>> valueNullList = array.Where(t => t.Value.IsEmpty());
            foreach (KeyValuePair<TKey, Guid> each in valueNullList)
                array.Remove(each.Key);
            return array;
        }

        /// <summary>
        /// 移除空值
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <param name="array">集合</param>
        public static IDictionary<TKey, Guid?> RemoveEmpty<TKey>(IDictionary<TKey, Guid?> array)
        {
            if (array == null)
                return default(IDictionary<TKey, Guid?>);
            IEnumerable<KeyValuePair<TKey, Guid?>> valueNullList = array.Where(t => t.Value.IsEmpty());
            foreach (KeyValuePair<TKey, Guid?> each in valueNullList)
                array.Remove(each.Key);
            return array;
        }

        /// <summary>
        /// 移除空值
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <param name="array">集合</param>
        public static IDictionary<TKey, DateTime> RemoveEmpty<TKey>(IDictionary<TKey, DateTime> array)
        {
            if (array == null)
                return default(IDictionary<TKey, DateTime>);
            IEnumerable<KeyValuePair<TKey, DateTime>> valueNullList = array.Where(t => t.Value.IsEmpty());
            foreach (KeyValuePair<TKey, DateTime> each in valueNullList)
                array.Remove(each.Key);
            return array;
        }

        /// <summary>
        /// 移除空值
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <param name="array">集合</param>
        public static IDictionary<TKey, DateTime?> RemoveEmpty<TKey>(IDictionary<TKey, DateTime?> array)
        {
            if (array == null)
                return default(IDictionary<TKey, DateTime?>);
            IEnumerable<KeyValuePair<TKey, DateTime?>> valueNullList = array.Where(t => t.Value.IsEmpty());
            foreach (KeyValuePair<TKey, DateTime?> each in valueNullList)
                array.Remove(each.Key);
            return array;
        }

        #endregion

        #region Add 和 AddRange 扩展

        /// <summary>
        /// 添加字典
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <typeparam name="TValue">字典值元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
        public static IDictionary<TKey, TValue> Add<TKey, TValue>(IDictionary<TKey, TValue> array, TKey key, TValue value, bool isReplace = true)
        {
            array ??= new Dictionary<TKey, TValue>();
            if (key == null)
                return array;
            if (!array.ContainsKey(key))
            {
                array.Add(key, value);
                return array;
            }
            if (!isReplace)
                return array;
            array.Remove(key);
            array.Add(key, value);
            return array;
        }

        /// <summary>
        /// 添加字典集合
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <typeparam name="TValue">字典值元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="values">值集合</param>
        /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
        public static IDictionary<TKey, TValue> AddRange<TKey, TValue>(IDictionary<TKey, TValue> array, IDictionary<TKey, TValue> values, bool isReplace = true)
        {
            array ??= new Dictionary<TKey, TValue>();
            values ??= new Dictionary<TKey, TValue>();
            foreach (KeyValuePair<TKey, TValue> itemValue in values)
                Add(array, itemValue.Key, itemValue.Value, isReplace);
            return array;
        }

        /// <summary>
        /// 添加字典集合
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <typeparam name="TValue">字典值元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="values">值集合</param>
        /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
        public static IDictionary<TKey, TValue> AddRange<TKey, TValue>(IDictionary<TKey, TValue> array, IEnumerable<KeyValuePair<TKey, TValue>> values, bool isReplace = true)
        {
            array ??= new Dictionary<TKey, TValue>();
            values ??= new Dictionary<TKey, TValue>();
            foreach (KeyValuePair<TKey, TValue> itemValue in values)
                Add(array, itemValue.Key, itemValue.Value, isReplace);
            return array;
        }

        /// <summary>
        /// 添加值不为null字典
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <typeparam name="TValue">字典值元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
        public static IDictionary<TKey, TValue> AddNotNull<TKey, TValue>(IDictionary<TKey, TValue> array, TKey key, TValue value, bool isReplace = true)
        {
            array ??= new Dictionary<TKey, TValue>();
            if (value == null)
                return array;
            return Add(array, key, value, isReplace);
        }

        /// <summary>
        /// 添加值不为null字典集合
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <typeparam name="TValue">字典值元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="values">值集合</param>
        /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
        public static IDictionary<TKey, TValue> AddRangeNotNull<TKey, TValue>(IDictionary<TKey, TValue> array, IDictionary<TKey, TValue> values, bool isReplace = true)
        {
            array ??= new Dictionary<TKey, TValue>();
            values ??= new Dictionary<TKey, TValue>();
            IEnumerable<KeyValuePair<TKey, TValue>> valueNotNullList = values.Where(t => t.Value != null);
            foreach (var itemValue in valueNotNullList)
                AddNotNull(array, itemValue.Key, itemValue.Value, isReplace);
            return array;
        }

        /// <summary>
        /// 添加值不为null字典集合
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <typeparam name="TValue">字典值元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="values">值集合</param>
        /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
        public static IDictionary<TKey, TValue> AddRangeNotNull<TKey, TValue>(IDictionary<TKey, TValue> array, IEnumerable<KeyValuePair<TKey, TValue>> values, bool isReplace = true)
        {
            array ??= new Dictionary<TKey, TValue>();
            values ??= new Dictionary<TKey, TValue>();
            IEnumerable<KeyValuePair<TKey, TValue>> valueNotNullList = values.Where(t => t.Value != null);
            foreach (var itemValue in valueNotNullList)
                AddNotNull(array, itemValue.Key, itemValue.Value, isReplace);
            return array;
        }

        /// <summary>
        /// 添加值有效字典
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
        public static IDictionary<TKey, string> AddNotEmpty<TKey>(IDictionary<TKey, string> array, TKey key, string value, bool isReplace = true)
        {
            array ??= new Dictionary<TKey, string>();
            if (value.IsEmpty())
                return array;
            return Add(array, key, value, isReplace);
        }

        /// <summary>
        /// 添加值有效字典集合
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="values">值集合</param>
        /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
        public static IDictionary<TKey, string> AddRangeNotEmpty<TKey>(IDictionary<TKey, string> array, IDictionary<TKey, string> values, bool isReplace = true)
        {
            array ??= new Dictionary<TKey, string>();
            values ??= new Dictionary<TKey, string>();
            IEnumerable<KeyValuePair<TKey, string>> valueNotNullList = values.Where(t => !t.Value.IsEmpty());
            foreach (var itemValue in valueNotNullList)
                AddNotEmpty(array, itemValue.Key, itemValue.Value, isReplace);
            return array;
        }

        /// <summary>
        /// 添加值有效字典集合
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="values">值集合</param>
        /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
        public static IDictionary<TKey, string> AddRangeNotEmpty<TKey>(IDictionary<TKey, string> array, IEnumerable<KeyValuePair<TKey, string>> values, bool isReplace = true)
        {
            array ??= new Dictionary<TKey, string>();
            values ??= new Dictionary<TKey, string>();
            IEnumerable<KeyValuePair<TKey, string>> valueNotNullList = values.Where(t => !t.Value.IsEmpty());
            foreach (var itemValue in valueNotNullList)
                AddNotEmpty(array, itemValue.Key, itemValue.Value, isReplace);
            return array;
        }

        /// <summary>
        /// 添加值有效字典
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
        public static IDictionary<TKey, Guid> AddNotEmpty<TKey>(IDictionary<TKey, Guid> array, TKey key, Guid value, bool isReplace = true)
        {
            array ??= new Dictionary<TKey, Guid>();
            if (value.IsEmpty())
                return array;
            return Add(array, key, value, isReplace);
        }

        /// <summary>
        /// 添加值有效字典集合
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="values">值集合</param>
        /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
        public static IDictionary<TKey, Guid> AddRangeNotEmpty<TKey>(IDictionary<TKey, Guid> array, IDictionary<TKey, Guid> values, bool isReplace = true)
        {
            array ??= new Dictionary<TKey, Guid>();
            values ??= new Dictionary<TKey, Guid>();
            IEnumerable<KeyValuePair<TKey, Guid>> valueNotNullList = values.Where(t => !t.Value.IsEmpty());
            foreach (var itemValue in valueNotNullList)
                AddNotEmpty(array, itemValue.Key, itemValue.Value, isReplace);
            return array;
        }

        /// <summary>
        /// 添加值有效字典集合
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="values">值集合</param>
        /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
        public static IDictionary<TKey, Guid> AddRangeNotEmpty<TKey>(IDictionary<TKey, Guid> array, IEnumerable<KeyValuePair<TKey, Guid>> values, bool isReplace = true)
        {
            array ??= new Dictionary<TKey, Guid>();
            values ??= new Dictionary<TKey, Guid>();
            IEnumerable<KeyValuePair<TKey, Guid>> valueNotNullList = values.Where(t => !t.Value.IsEmpty());
            foreach (var itemValue in valueNotNullList)
                AddNotEmpty(array, itemValue.Key, itemValue.Value, isReplace);
            return array;
        }

        /// <summary>
        /// 添加值有效字典
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
        public static IDictionary<TKey, Guid?> AddNotEmpty<TKey>(IDictionary<TKey, Guid?> array, TKey key, Guid? value, bool isReplace = true)
        {
            array ??= new Dictionary<TKey, Guid?>();
            if (value.IsEmpty())
                return array;
            return Add(array, key, value, isReplace);
        }

        /// <summary>
        /// 添加值有效字典集合
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="values">值集合</param>
        /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
        public static IDictionary<TKey, Guid?> AddRangeNotEmpty<TKey>(IDictionary<TKey, Guid?> array, IDictionary<TKey, Guid?> values, bool isReplace = true)
        {
            array ??= new Dictionary<TKey, Guid?>();
            values ??= new Dictionary<TKey, Guid?>();
            IEnumerable<KeyValuePair<TKey, Guid?>> valueNotNullList = values.Where(t => !t.Value.IsEmpty());
            foreach (var itemValue in valueNotNullList)
                AddNotEmpty(array, itemValue.Key, itemValue.Value, isReplace);
            return array;
        }

        /// <summary>
        /// 添加值有效字典集合
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="values">值集合</param>
        /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
        public static IDictionary<TKey, Guid?> AddRangeNotEmpty<TKey>(IDictionary<TKey, Guid?> array, IEnumerable<KeyValuePair<TKey, Guid?>> values, bool isReplace = true)
        {
            array ??= new Dictionary<TKey, Guid?>();
            values ??= new Dictionary<TKey, Guid?>();
            IEnumerable<KeyValuePair<TKey, Guid?>> valueNotNullList = values.Where(t => !t.Value.IsEmpty());
            foreach (var itemValue in valueNotNullList)
                AddNotEmpty(array, itemValue.Key, itemValue.Value, isReplace);
            return array;
        }

        /// <summary>
        /// 添加值有效字典
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
        public static IDictionary<TKey, DateTime> AddNotEmpty<TKey>(IDictionary<TKey, DateTime> array, TKey key, DateTime value, bool isReplace = true)
        {
            array ??= new Dictionary<TKey, DateTime>();
            if (value.IsEmpty())
                return array;
            return Add(array, key, value, isReplace);
        }

        /// <summary>
        /// 添加值有效字典集合
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="values">值集合</param>
        /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
        public static IDictionary<TKey, DateTime> AddRangeNotEmpty<TKey>(IDictionary<TKey, DateTime> array, IDictionary<TKey, DateTime> values, bool isReplace = true)
        {
            array ??= new Dictionary<TKey, DateTime>();
            values ??= new Dictionary<TKey, DateTime>();
            IEnumerable<KeyValuePair<TKey, DateTime>> valueNotNullList = values.Where(t => !t.Value.IsEmpty());
            foreach (var itemValue in valueNotNullList)
                AddNotEmpty(array, itemValue.Key, itemValue.Value, isReplace);
            return array;
        }

        /// <summary>
        /// 添加值有效字典集合
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="values">值集合</param>
        /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
        public static IDictionary<TKey, DateTime> AddRangeNotEmpty<TKey>(IDictionary<TKey, DateTime> array, IEnumerable<KeyValuePair<TKey, DateTime>> values, bool isReplace = true)
        {
            array ??= new Dictionary<TKey, DateTime>();
            values ??= new Dictionary<TKey, DateTime>();
            IEnumerable<KeyValuePair<TKey, DateTime>> valueNotNullList = values.Where(t => !t.Value.IsEmpty());
            foreach (var itemValue in valueNotNullList)
                AddNotEmpty(array, itemValue.Key, itemValue.Value, isReplace);
            return array;
        }

        /// <summary>
        /// 添加值有效字典
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
        public static IDictionary<TKey, DateTime?> AddNotEmpty<TKey>(IDictionary<TKey, DateTime?> array, TKey key, DateTime? value, bool isReplace = true)
        {
            array ??= new Dictionary<TKey, DateTime?>();
            if (value.IsEmpty())
                return array;
            return Add(array, key, value, isReplace);
        }

        /// <summary>
        /// 添加值有效字典集合
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="values">值集合</param>
        /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
        public static IDictionary<TKey, DateTime?> AddRangeNotEmpty<TKey>(IDictionary<TKey, DateTime?> array, IDictionary<TKey, DateTime?> values, bool isReplace = true)
        {
            array ??= new Dictionary<TKey, DateTime?>();
            values ??= new Dictionary<TKey, DateTime?>();
            IEnumerable<KeyValuePair<TKey, DateTime?>> valueNotNullList = values.Where(t => !t.Value.IsEmpty());
            foreach (var itemValue in valueNotNullList)
                AddNotEmpty(array, itemValue.Key, itemValue.Value, isReplace);
            return array;
        }

        /// <summary>
        /// 添加值有效字典集合
        /// </summary>
        /// <typeparam name="TKey">字典键元素类型</typeparam>
        /// <param name="array">集合</param>
        /// <param name="values">值集合</param>
        /// <param name="isReplace">是否替换，默认true，新值替换旧值</param>
        public static IDictionary<TKey, DateTime?> AddRangeNotEmpty<TKey>(IDictionary<TKey, DateTime?> array, IEnumerable<KeyValuePair<TKey, DateTime?>> values, bool isReplace = true)
        {
            array ??= new Dictionary<TKey, DateTime?>();
            values ??= new Dictionary<TKey, DateTime?>();
            IEnumerable<KeyValuePair<TKey, DateTime?>> valueNotNullList = values.Where(t => !t.Value.IsEmpty());
            foreach (var itemValue in valueNotNullList)
                AddNotEmpty(array, itemValue.Key, itemValue.Value, isReplace);
            return array;
        }

        #endregion
    }
}
