using System;
using Meow.Extension;
using Meow.Helper;

namespace Meow.Domain.Auditing;

/// <summary>
/// 创建操作审计设置器
/// </summary>
public class CreationAuditedSetter
{
    /// <summary>
    /// 实体
    /// </summary>
    private readonly object _entity;
    /// <summary>
    /// 用户标识
    /// </summary>
    private readonly string _userId;

    /// <summary>
    /// 初始化创建操作审计设置器
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="userId">用户标识</param>
    private CreationAuditedSetter(object entity, string userId)
    {
        _entity = entity;
        _userId = userId;
    }

    /// <summary>
    /// 设置创建审计属性
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="userId">用户标识</param>
    public static void Set(object entity, string userId)
    {
        new CreationAuditedSetter(entity, userId).Init();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        if (_entity == null)
            return;
        if (_entity is ICreationAudited<Guid> entityByGuid)
        {
            entityByGuid.CreationTime = Time.Now;
            entityByGuid.CreatorId = _userId.ToGuid();
            return;
        }
        if (_entity is ICreationAudited<Guid?> entityByGuidOrNull)
        {
            entityByGuidOrNull.CreationTime = Time.Now;
            entityByGuidOrNull.CreatorId = _userId.ToGuidOrNull();
            return;
        }
        if (_entity is ICreationAudited<int> entityByInt)
        {
            entityByInt.CreationTime = Time.Now;
            entityByInt.CreatorId = _userId.ToInt();
            return;
        }
        if (_entity is ICreationAudited<int?> entityByIntOrNull)
        {
            entityByIntOrNull.CreationTime = Time.Now;
            entityByIntOrNull.CreatorId = _userId.ToIntOrNull();
            return;
        }
        if (_entity is ICreationAudited<string> entityByString)
        {
            entityByString.CreationTime = Time.Now;
            entityByString.CreatorId = _userId.SafeString();
            return;
        }
        if (_entity is ICreationAudited<long> entityByLong)
        {
            entityByLong.CreationTime = Time.Now;
            entityByLong.CreatorId = _userId.ToLong();
            return;
        }
        if (_entity is ICreationAudited<long?> entityByLongOrNull)
        {
            entityByLongOrNull.CreationTime = Time.Now;
            entityByLongOrNull.CreatorId = _userId.ToLongOrNull();
            return;
        }
    }
}