using Meow.Domain.Base.Interface;

namespace Meow.Domain.Base
{
    /// <summary>
    /// 领域层顶级基类
    /// </summary>
    public abstract class DomainBase<T> : IDomain where T : IDomain
    {
    }
}
