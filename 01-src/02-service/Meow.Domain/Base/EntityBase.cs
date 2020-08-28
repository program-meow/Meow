using System.ComponentModel.DataAnnotations;
using Meow.Domain.Base.Interface;
using Meow.Extension.Helper;
using Meow.Helper;
using Guid = System.Guid;

namespace Meow.Domain.Base
{
    /// <summary>
    /// 领域实体
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class EntityBase<TEntity> : EntityBase<TEntity, Guid> where TEntity : IEntity
    {
        /// <summary>
        /// 初始化领域实体
        /// </summary>
        /// <param name="id">标识</param>
        protected EntityBase(Guid id) : base(id)
        {
        }
    }

    /// <summary>
    /// 领域实体
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">标识类型</typeparam>
    public abstract class EntityBase<TEntity, TKey> : DomainBase<TEntity>, IEntity<TEntity, TKey> where TEntity : IEntity
    {
        /// <summary>
        /// 初始化领域实体
        /// </summary>
        /// <param name="id">标识</param>
        protected EntityBase(TKey id)
        {
            Id = id;
        }

        /// <summary>
        /// 标识
        /// </summary>
        [Key]
        public TKey Id { get; private set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Init()
        {
            InitId();
        }

        /// <summary>
        /// 初始化标识
        /// </summary>
        protected virtual void InitId()
        {
            if (typeof(TKey) == typeof(int) || typeof(TKey) == typeof(long))
                return;
            if (string.IsNullOrWhiteSpace(Id.SafeString()) || Id.Equals(default(TKey)))
                Id = CreateId();
        }

        /// <summary>
        /// 创建标识
        /// </summary>
        protected virtual TKey CreateId()
        {
            return Common.To<TKey>(Guid.NewGuid());
        }
    }
}
