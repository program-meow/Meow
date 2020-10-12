using System.Security.Claims;
using Meow.Auth.Principal;
using Meow.Helper;

namespace Meow.Auth.Helper
{
    /// <summary>
    /// 身份认证操作
    /// </summary>
    public static class Identity
    {
        #region User(当前用户安全主体)

        /// <summary>
        /// 当前用户安全主体
        /// </summary>
        public static ClaimsPrincipal User
        {
            get
            {
                if (Web.HttpContext == null)
                    return UnauthenticatedPrincipal.Instance;
                if (Web.HttpContext.User is ClaimsPrincipal principal)
                    return principal;
                return UnauthenticatedPrincipal.Instance;
            }
        }

        #endregion

        #region Identity(当前用户身份)

        /// <summary>
        /// 当前用户身份
        /// </summary>
        public static ClaimsIdentity Instance
        {
            get
            {
                if (User.Identity is ClaimsIdentity identity)
                    return identity;
                return UnauthenticatedIdentity.Instance;
            }
        }

        #endregion

        #region AccessToken(获取访问令牌)

        /// <summary>
        /// 获取访问令牌
        /// </summary>
        public static string AccessToken => Web.AccessToken;

        #endregion
    }
}