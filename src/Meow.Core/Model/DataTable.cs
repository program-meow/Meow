using System.Collections.Generic;

namespace Meow.Model
{
    /// <summary>
    /// 数据表
    /// </summary>
    public class DataTable
    {
        /// <summary>
        /// 初始化数据表
        /// </summary>
        /// <param name="name">表名</param>
        /// <param name="rows">行数据</param>
        public DataTable(string name, List<DataRow> rows)
        {
            Name = name;
            Rows = rows;
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 行数据
        /// </summary>
        public List<DataRow> Rows { get; }
    }
}
