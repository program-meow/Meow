using System;

namespace Meow.Application.Domain.Model.Auditing {
    /// <summary>
    /// 创建时间审计
    /// </summary>
    public interface ICreationTime {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime? CreationTime { get; set; }
    }
}
