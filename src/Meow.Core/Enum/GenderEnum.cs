using System.ComponentModel;

namespace Meow.Enum;

/// <summary>
/// 性别
/// </summary>
public enum GenderEnum
{
    /// <summary>
    /// 女
    /// </summary>        
    [Description( "meow.female" )]
    Female = 1,
    /// <summary>
    /// 男
    /// </summary>
    [Description( "meow.male" )]
    Male = 2
}