using System.Collections.Generic;

namespace Meow.Model
{
    /// <summary>
    /// 数据行
    /// </summary>
    public class DataRow
    {
        /// <summary>
        /// 初始化数据行
        /// </summary>
        /// <param name="no">行号</param>
        /// <param name="columns">列数据</param>
        public DataRow(int no, List<KeyValue> columns)
        {
            No = no;
            Columns = columns;
        }

        /// <summary>
        /// 行号
        /// </summary>
        public int No { get; }

        /// <summary>
        /// 列数据
        /// </summary>
        public List<KeyValue> Columns { get; }
    }
}
