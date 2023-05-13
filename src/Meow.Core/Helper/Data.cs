using Meow.Model;
using System.Collections.Generic;
using System.Data;
using Meow.Extension;
using System.Data.Common;

namespace Meow.Helper;

/// <summary>
/// 数据操作
/// </summary>
public static class Data
{
    /// <summary>
    /// 转换为数据表集合
    /// </summary>
    /// <param name="dataSet">数据集</param>
    public static List<Meow.Model.DataTable> ToDataTableList(DataSet dataSet)
    {
        List<Meow.Model.DataTable> result = new List<Meow.Model.DataTable>();
        if (dataSet == null)
            return result;
        for (int i = 0; i < dataSet.Tables.Count; i++)
        {
            List<Meow.Model.DataRow> rows = ToDataRowList(dataSet.Tables[i]);
            result.Add(new Meow.Model.DataTable(dataSet.Tables[i].TableName, rows));
        }
        return result;
    }

    /// <summary>
    /// 转换为数据行集合
    /// </summary>
    /// <param name="data">数据表</param>
    public static List<Meow.Model.DataRow> ToDataRowList(System.Data.DataTable data)
    {
        List<Meow.Model.DataRow> result = new List<Meow.Model.DataRow>();
        if (data == null)
            return result;
        DataRowCollection dataRow = data.Rows;
        for (int i = 0; i < dataRow.Count; i++)
        {
            List<KeyValue> columns = new List<KeyValue>();
            for (int j = 0; j < data.Columns.Count; j++)
            {
                string key = data.Columns[j].ColumnName;
                string value = dataRow[i][j].SafeString();
                columns.Add(new KeyValue(key, value));
            }
            result.Add(new Meow.Model.DataRow(i + 1, columns));
        }
        return result;
    }

    /// <summary>
    /// 获取列值集合
    /// </summary>
    /// <param name="data">数据表</param>
    /// <param name="columnNo">第几列，从1开始</param>
    public static List<string> GetColumnValueList(System.Data.DataTable data, int columnNo)
    {
        return GetColumnValueList<string>(data, columnNo);
    }

    /// <summary>
    /// 获取列值集合
    /// </summary>
    /// <typeparam name="TResult">结果元素类型</typeparam>
    /// <param name="data">数据表</param>
    /// <param name="columnNo">第几列，从1开始</param>
    public static List<TResult> GetColumnValueList<TResult>(System.Data.DataTable data, int columnNo)
    {
        var result = new List<TResult>();
        if (data == null)
            return result;
        if (columnNo < 1 || columnNo > data.Columns.Count)
            return result;
        DataRowCollection dataRow = data.Rows;
        for (int i = 0; i < dataRow.Count; i++)
        {
            string value = dataRow[i][columnNo - 1].SafeString();
            if (value.IsEmpty())
                continue;
            result.Add(value.To<TResult>());
        }
        return result;
    }

    /// <summary>
    /// 获取列值集合
    /// </summary>
    /// <param name="data">数据表</param>
    /// <param name="columnName">列名</param>
    /// <param name="isFuzzy">是否模糊大小写</param>
    public static List<string> GetColumnValueList(System.Data.DataTable data, string columnName, bool isFuzzy = true)
    {
        return GetColumnValueList<string>(data, columnName, isFuzzy);
    }

    /// <summary>
    /// 获取列值集合
    /// </summary>
    /// <typeparam name="TResult">结果元素类型</typeparam>
    /// <param name="data">数据表</param>
    /// <param name="columnName">列名</param>
    /// <param name="isFuzzy">是否模糊大小写</param>
    public static List<TResult> GetColumnValueList<TResult>(System.Data.DataTable data, string columnName, bool isFuzzy = true)
    {
        var result = new List<TResult>();
        if (data == null)
            return result;
        DataRowCollection dataRow = data.Rows;
        columnName = isFuzzy ? columnName.ToLower() : columnName;
        for (int i = 0; i < dataRow.Count; i++)
        {
            for (int j = 0; j < data.Columns.Count; j++)
            {
                string key = isFuzzy ? data.Columns[j].ColumnName.ToLower() : data.Columns[j].ColumnName;
                if (key != columnName)
                    continue;
                string value = dataRow[i][j].SafeString();
                if (value.IsEmpty())
                    continue;
                result.Add(value.To<TResult>());
            }
        }
        return result;
    }

    /// <summary>
    /// 转换为键值对集合
    /// </summary>
    /// <param name="dataReader">DataReader对象</param>
    public static List<KeyValue> ToKeyValueList(DbDataReader dataReader)
    {
        List<KeyValue> result = new List<KeyValue>();
        if (dataReader == null)
            return result;
        while (dataReader.Read())
        {
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                string key = dataReader.GetName(i);
                string value = dataReader[i].SafeString();
                result.Add(new KeyValue(key, value));
            }
        }
        dataReader.Close();
        return result;
    }
}