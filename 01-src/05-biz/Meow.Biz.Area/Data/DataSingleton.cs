using System.Collections.Generic;
using Meow.Extension.Helper;

namespace Meow.Biz.Area.Data
{
    /// <summary>
    /// 数据单例
    /// </summary>
    public class DataSingleton
    {
        // 定义一个静态变量来保存类的实例
        private static List<Model.Area> _data;

        /// <summary>
        /// 锁
        /// </summary>
        private static readonly object _locker = new object();

        /// <summary>
        /// 初始化数据单例
        /// </summary>
        private DataSingleton()
        {
        }

        /// <summary>
        /// 获取数据实例
        /// </summary>
        public static List<Model.Area> GetInstance()
        {
            if (_data.IsNotNull())
                return _data;
            lock (_locker)
            {
                if (_data.IsNotNull())
                    return _data;
                var decompressData = DataSource.Data.Decompress();
                _data = decompressData.ToObject<List<Model.Area>>();
            }
            return _data;
        }
    }
}
