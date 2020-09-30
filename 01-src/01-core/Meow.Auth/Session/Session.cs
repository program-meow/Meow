using IdentityModel;
using Meow.Auth.Extension;
using Meow.Extension.Helper;
using Identity = Meow.Auth.Helper.Identity;

namespace Meow.Auth.Session {
    /// <summary>
    /// 用户会话
    /// </summary>
    public class Session : ISession {
        /// <summary>
        /// 空用户会话
        /// </summary>
        public static readonly ISession Null = NullSession.Instance;

        /// <summary>
        /// 用户会话
        /// </summary>
        public static readonly ISession Instance = new Session();

        /// <summary>
        /// 是否认证
        /// </summary>
        public bool IsAuthenticated => Identity.Instance.IsAuthenticated;

        /// <summary>
        /// 用户标识
        /// </summary>
        public string UserId {
            get {
                var result = Identity.Instance.GetValue( JwtClaimTypes.Subject );
                return string.IsNullOrWhiteSpace( result ) ? Identity.Instance.GetValue( System.Security.Claims.ClaimTypes.NameIdentifier ) : result;
            }
        }
    }
}