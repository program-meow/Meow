namespace Meow.Application.Domain.Core.Model
{
    /// <summary>
    /// 通过对象比较获取变更属性集
    /// </summary>
    /// <typeparam name="T">领域对象类型</typeparam>
    public interface ICompareChange<in T> where T : IDomain
    {
        /// <summary>
        /// 获取变更属性
        /// </summary>
        /// <param name="other">其它领域对象</param>
        ChangeValueCollection GetChanges(T other);
    }
}