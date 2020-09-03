using Microsoft.EntityFrameworkCore;

namespace Meow.Data.Ef.Core.Interface
{
    /// <summary>
    /// 映射
    /// </summary>
    public interface IMap
    {
        /// <summary>
        /// 映射配置
        /// </summary>
        /// <param name="builder">模型生成器</param>
        void Map(ModelBuilder builder);
    }
}
