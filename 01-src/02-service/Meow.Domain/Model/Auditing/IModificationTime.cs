﻿using System;

namespace Meow.Domain.Model.Auditing {
    /// <summary>
    /// 修改时间审计
    /// </summary>
    public interface IModificationTime {
        /// <summary>
        /// 最后修改时间
        /// </summary>
        DateTime? LastModificationTime { get; set; }
    }
}
