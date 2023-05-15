using System.Collections.Generic;
using SystemType = System.Type;

namespace Meow.Reflection;

/// <summary>
/// 类型查找器
/// </summary>
public interface ITypeFinder
{
    /// <summary>
    /// 查找类型列表
    /// </summary>
    /// <typeparam name="T">查找类型</typeparam>
    List<SystemType> Find<T>();
    /// <summary>
    /// 查找类型列表
    /// </summary>
    /// <param name="findType">查找类型</param>
    List<SystemType> Find(SystemType findType);
}