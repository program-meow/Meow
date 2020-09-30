using Meow.Parameter.Enum;
using Microsoft.EntityFrameworkCore;

namespace Meow.Application.Data.Ef.Core.Map
{
    /// <summary>
    /// 映射
    /// </summary>
    public interface IMap
    {
        /// <summary>
        /// 映射配置
        /// </summary>
        /// <param name="databaseType">数据库类型</param>
        /// <param name="builder">模型生成器</param>
        void Map(Database databaseType, ModelBuilder builder);
    }
}