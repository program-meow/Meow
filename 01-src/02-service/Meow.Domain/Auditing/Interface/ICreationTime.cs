using System;

namespace Meow.Domain.Auditing.Interface
{
    /// <summary>
    /// 创建时间审计
    /// </summary>
    public interface ICreationTime
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime? CreationTime { get; set; }
    }
}
