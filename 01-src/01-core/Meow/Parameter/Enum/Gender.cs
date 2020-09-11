﻿using System.ComponentModel;

namespace Meow.Parameter.Enum
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// 女
        /// </summary>        
        [Description("女")]
        Female = 1,
        /// <summary>
        /// 男
        /// </summary>
        [Description("男")]
        Male = 2
    }
}