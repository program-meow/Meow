using System;
using Meow.Extension;
using Meow.Helper;

namespace Meow.Domain.Auditing;

/// <summary>
/// 修改操作审计设置器
/// </summary>
public class ModificationAuditedSetter
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
    /// 初始化修改操作审计设置器
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="userId">用户标识</param>
    private ModificationAuditedSetter(object entity, string userId)
    {
        _entity = entity;
        _userId = userId;
    }

    /// <summary>
    /// 设置修改审计属性
    /// </summary>
    /// <param name="entity">实体</param>
    /// <param name="userId">用户标识</param>
    public static void Set(object entity, string userId)
    {
        new ModificationAuditedSetter(entity, userId).Init();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        if (_entity == null)
            return;
        if (_entity is IModificationAudited<Guid> entityByGuid)
        {
            entityByGuid.LastModificationTime = Time.Now;
            entityByGuid.LastModifierId = _userId.ToGuid();
            return;
        }
        if (_entity is IModificationAudited<Guid?> entityByGuidOrNull)
        {
            entityByGuidOrNull.LastModificationTime = Time.Now;
            entityByGuidOrNull.LastModifierId = _userId.ToGuidOrNull();
            return;
        }
        if (_entity is IModificationAudited<int> entityByInt)
        {
            entityByInt.LastModificationTime = Time.Now;
            entityByInt.LastModifierId = _userId.ToInt();
            return;
        }
        if (_entity is IModificationAudited<int?> entityByIntOrNull)
        {
            entityByIntOrNull.LastModificationTime = Time.Now;
            entityByIntOrNull.LastModifierId = _userId.ToIntOrNull();
            return;
        }
        if (_entity is IModificationAudited<string> entityByString)
        {
            entityByString.LastModificationTime = Time.Now;
            entityByString.LastModifierId = _userId.SafeString();
            return;
        }
        if (_entity is IModificationAudited<long> entityByLong)
        {
            entityByLong.LastModificationTime = Time.Now;
            entityByLong.LastModifierId = _userId.ToLong();
            return;
        }
        if (_entity is IModificationAudited<long?> entityByLongOrNull)
        {
            entityByLongOrNull.LastModificationTime = Time.Now;
            entityByLongOrNull.LastModifierId = _userId.ToLongOrNull();
            return;
        }
    }
}