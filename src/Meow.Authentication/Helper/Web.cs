namespace Meow.Authentication.Helper;

/// <summary>
/// Web操作
/// </summary>
public static class Web {

    #region User  [当前用户安全主体]

    /// <summary>
    /// 当前用户安全主体
    /// </summary>
    public static ClaimsPrincipal User {
        get {
            if( Meow.Helper.Web.HttpContext?.User is { } principal )
                return principal;
            return UnauthenticatedPrincipal.Instance;
        }
    }

    #endregion

    #region Identity  [当前用户身份标识]

    /// <summary>
    /// 当前用户身份标识
    /// </summary>
    public static ClaimsIdentity Identity {
        get {
            if( User.Identity is ClaimsIdentity identity )
                return identity;
            return UnauthenticatedIdentity.Instance;
        }
    }

    #endregion
}