using Meow.Authentication.Extension;
using Meow.Authentication.Helper;
using Meow.Extension;

namespace Meow.Authentication.Session;

/// <summary>
/// 用户会话
/// </summary>
public class UserSession : ISession {
    /// <summary>
    /// 空用户会话
    /// </summary>
    public static readonly ISession Null = NullSession.Instance;

    /// <summary>
    /// 用户会话实例
    /// </summary>
    public static readonly ISession Instance = new UserSession();

    /// <inheritdoc />
    public virtual IServiceProvider ServiceProvider => Meow.Helper.Web.ServiceProvider;

    /// <inheritdoc />
    public virtual bool IsAuthenticated => Web.Identity.IsAuthenticated;

    /// <inheritdoc />
    public virtual string UserId {
        get {
            string result = Web.Identity.GetValue( ClaimTypes.UserId );
            return result.IsEmpty() ? Web.Identity.GetValue( System.Security.Claims.ClaimTypes.NameIdentifier ) : result;
        }
    }

    /// <inheritdoc />
    public virtual string TenantId => Web.Identity.GetValue( ClaimTypes.TenantId );
}