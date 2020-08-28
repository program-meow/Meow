using System;

namespace Meow.Domain.Auditing.Interface
{
    /// <summary>
    /// 创建操作审计
    /// </summary>
    public interface ICreationAudited : ICreationAudited<Guid?>
    {
    }

    /// <summary>
    /// 创建操作审计
    /// </summary>
    /// <typeparam name="TKey">创建人标识类型</typeparam>
    public interface ICreationAudited<TKey> : ICreation<TKey>, ICreationTime
    {
    }
}
