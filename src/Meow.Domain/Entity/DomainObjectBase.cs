using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Meow.Domain.Compare;
using Meow.Extension;
using Meow.Validation;

namespace Meow.Domain.Entity;

/// <summary>
/// 领域对象基类
/// </summary>
public abstract class DomainObjectBase<T> : IDomainObject, ICompareChange<T> where T : IDomainObject
{

    #region 字段

    /// <summary>
    /// 验证规则集合
    /// </summary>
    private readonly List<IValidationRule> _rules;
    /// <summary>
    /// 验证处理器
    /// </summary>
    private IValidationHandler _handler;
    /// <summary>
    /// 变更值集合
    /// </summary>
    private ChangeValueCollection _changeValues;

    #endregion

    #region 构造方法

    /// <summary>
    /// 初始化领域对象基类
    /// </summary>
    protected DomainObjectBase()
    {
        _rules = new List<IValidationRule>();
        _handler = new ThrowHandler();
    }

    #endregion

    #region Validate  [验证]

    /// <summary>
    /// 设置验证处理器
    /// </summary>
    /// <param name="handler">验证处理器</param>
    public void SetValidationHandler(IValidationHandler handler)
    {
        if (handler == null)
            return;
        _handler = handler;
    }

    /// <summary>
    /// 添加验证规则列表
    /// </summary>
    /// <param name="rules">验证规则列表</param>
    public void AddValidationRules(IEnumerable<IValidationRule> rules)
    {
        if (rules == null)
            return;
        foreach (IValidationRule rule in rules)
            AddValidationRule(rule);
    }

    /// <summary>
    /// 添加验证规则
    /// </summary>
    /// <param name="rule">验证规则</param>
    public void AddValidationRule(IValidationRule rule)
    {
        if (rule == null)
            return;
        _rules.Add(rule);
    }

    /// <summary>
    /// 验证
    /// </summary>
    public virtual ValidationResultCollection Validate()
    {
        ValidationResultCollection result = GetValidationResults();
        HandleValidationResults(result);
        return result;
    }

    /// <summary>
    /// 获取验证结果
    /// </summary>
    private ValidationResultCollection GetValidationResults()
    {
        ValidationResultCollection result = DataAnnotationValidation.Validate(this);
        Validate(result);
        foreach (IValidationRule rule in _rules)
            result.Add(rule.Validate());
        return result;
    }

    /// <summary>
    /// 自定义验证钩子方法
    /// </summary>
    /// <param name="results">验证结果集合</param>
    protected virtual void Validate(ValidationResultCollection results)
    {
    }

    /// <summary>
    /// 处理验证结果
    /// </summary>
    protected virtual void HandleValidationResults(ValidationResultCollection results)
    {
        if (results.IsValid)
            return;
        _handler.Handle(results);
    }

    #endregion

    #region GetChanges  [获取变更属性]

    /// <summary>
    /// 获取变更属性
    /// </summary>
    /// <param name="newEntity">新对象</param>
    public ChangeValueCollection GetChanges(T newEntity)
    {
        if (Equals(newEntity, null))
            return new ChangeValueCollection();
        _changeValues = CreateChangeValueCollection(newEntity);
        AddChanges(newEntity);
        return _changeValues;
    }

    /// <summary>
    /// 创建变更值集合
    /// </summary>
    protected virtual ChangeValueCollection CreateChangeValueCollection(T newEntity)
    {
        string description = Meow.Helper.Reflection.GetDisplayNameOrDescription(newEntity.GetType());
        return new ChangeValueCollection(newEntity.GetType().ToString(), description);
    }

    /// <summary>
    /// 添加变更列表
    /// </summary>
    /// <param name="newEntity">新对象</param>
    protected virtual void AddChanges(T newEntity)
    {
    }

    /// <summary>
    /// 添加变更
    /// </summary>
    /// <param name="expression">属性表达式,范例：t => t.Name</param>
    /// <param name="newValue">新值,范例：newEntity.Name</param>
    protected void AddChange<TProperty, TValue>(Expression<Func<T, TProperty>> expression, TValue newValue)
    {
        MemberExpression member = Meow.Helper.Expression.GetMemberExpression(expression);
        string name = Meow.Helper.Expression.GetMemberName(member);
        string description = Meow.Helper.Reflection.GetDisplayNameOrDescription(member.Member);
        object value = member.Member.GetPropertyValue(this);
        AddChange(name, description, Meow.Helper.Convert.To<TValue>(value), newValue);
    }

    /// <summary>
    /// 添加变更
    /// </summary>
    /// <param name="propertyName">属性名</param>
    /// <param name="description">描述</param>
    /// <param name="oldValue">旧值,范例：this.Name</param>
    /// <param name="newValue">新值,范例：newEntity.Name</param>
    protected void AddChange<TValue>(string propertyName, string description, TValue oldValue, TValue newValue)
    {
        if (Equals(oldValue, newValue))
            return;
        string oldValueString = oldValue.SafeString().ToLower().Trim();
        string newValueString = newValue.SafeString().ToLower().Trim();
        if (oldValueString == newValueString)
            return;
        _changeValues.Add(propertyName, description, oldValueString, newValueString);
    }

    /// <summary>
    /// 添加变更
    /// </summary>
    /// <param name="oldObject">旧对象</param>
    /// <param name="newObject">新对象</param>
    protected void AddChange<TDomainObject>(ICompareChange<TDomainObject> oldObject, TDomainObject newObject) where TDomainObject : IDomainObject
    {
        if (Equals(oldObject, null))
            return;
        if (Equals(newObject, null))
            return;
        _changeValues.AddRange(oldObject.GetChanges(newObject));
    }

    /// <summary>
    /// 添加变更
    /// </summary>
    /// <param name="oldObjects">旧对象列表</param>
    /// <param name="newObjects">新对象列表</param>
    protected void AddChange<TDomainObject>(IEnumerable<ICompareChange<TDomainObject>> oldObjects, IEnumerable<TDomainObject> newObjects) where TDomainObject : IDomainObject
    {
        if (Equals(oldObjects, null))
            return;
        if (Equals(newObjects, null))
            return;
        List<ICompareChange<TDomainObject>> oldList = oldObjects.ToList();
        List<TDomainObject> newList = newObjects.ToList();
        for (int i = 0; i < oldList.Count; i++)
        {
            if (newList.Count <= i)
                return;
            AddChange(oldList[i], newList[i]);
        }
    }

    #endregion
}