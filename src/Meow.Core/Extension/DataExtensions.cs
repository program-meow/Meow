using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Meow.Model;

namespace Meow.Extension
{
    /// <summary>
    /// 数据扩展
    /// </summary>
    public static class DataExtensions
    {
        /// <summary>
        /// 转换为数据表集合
        /// </summary>
        /// <param name="dataSet">数据集</param>
        public static List<Meow.Model.DataTable> ToDataTableList(this DataSet dataSet)
        {
            return Meow.Helper.Data.ToDataTableList(dataSet);
        }

        /// <summary>
        /// 转换为数据行集合
        /// </summary>
        /// <param name="data">数据表</param>
        public static List<Meow.Model.DataRow> ToDataRowList(this System.Data.DataTable data)
        {
            return Meow.Helper.Data.ToDataRowList(data);
        }

        /// <summary>
        /// 获取列值集合
        /// </summary>
        /// <param name="data">数据表</param>
        /// <param name="columnNo">第几列，从1开始</param>
        public static List<string> GetColumnValueList(this System.Data.DataTable data, int columnNo)
        {
            return Meow.Helper.Data.GetColumnValueList(data, columnNo);
        }

        /// <summary>
        /// 获取列值集合
        /// </summary>
        /// <typeparam name="TResult">结果元素类型</typeparam>
        /// <param name="data">数据表</param>
        /// <param name="columnNo">第几列，从1开始</param>
        public static List<TResult> GetColumnValueList<TResult>(this System.Data.DataTable data, int columnNo)
        {
            return Meow.Helper.Data.GetColumnValueList<TResult>(data, columnNo);
        }

        /// <summary>
        /// 获取列值集合
        /// </summary>
        /// <param name="data">数据表</param>
        /// <param name="columnName">列名</param>
        /// <param name="isFuzzy">是否模糊大小写</param>
        public static List<string> GetColumnValueList(this System.Data.DataTable data, string columnName, bool isFuzzy = true)
        {
            return Meow.Helper.Data.GetColumnValueList(data, columnName, isFuzzy);
        }

        /// <summary>
        /// 获取列值集合
        /// </summary>
        /// <typeparam name="TResult">结果元素类型</typeparam>
        /// <param name="data">数据表</param>
        /// <param name="columnName">列名</param>
        /// <param name="isFuzzy">是否模糊大小写</param>
        public static List<TResult> GetColumnValueList<TResult>(this System.Data.DataTable data, string columnName, bool isFuzzy = true)
        {
            return Meow.Helper.Data.GetColumnValueList<TResult>(data, columnName, isFuzzy);
        }

        /// <summary>
        /// 转换为键值对集合
        /// </summary>
        /// <param name="dataReader">DataReader对象</param>
        public static List<KeyValue> ToKeyValueList(this DbDataReader dataReader)
        {
            return Meow.Helper.Data.ToKeyValueList(dataReader);
        }
    }
}
