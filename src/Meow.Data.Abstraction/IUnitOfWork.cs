﻿namespace Meow.Data;

/// <summary>
/// 工作单元
/// </summary>
[Ignore]
public interface IUnitOfWork : IDisposable, IFilterOperation {
    /// <summary>
    /// 提交,返回影响的行数
    /// </summary>
    Task<int> CommitAsync();
}