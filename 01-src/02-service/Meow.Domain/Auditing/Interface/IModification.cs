namespace Meow.Domain.Auditing.Interface
{
    /// <summary>
    /// 修改人标识审计
    /// </summary>
    /// <typeparam name="TKey">创建人标识类型</typeparam>
    public interface IModification<TKey>
    {
        /// <summary>
        /// 创建人标识
        /// </summary>
        TKey LastModifierId { get; set; }
    }
}
