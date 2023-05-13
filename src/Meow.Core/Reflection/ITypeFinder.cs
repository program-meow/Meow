using System.Collections.Generic;

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
    List<System.Type> Find<T>();
    /// <summary>
    /// 查找类型列表
    /// </summary>
    /// <param name="findType">查找类型</param>
    List<System.Type> Find(System.Type findType);
}