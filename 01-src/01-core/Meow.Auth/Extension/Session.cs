using System.Collections.Generic;
using Meow.Auth.Claim;
using Meow.Auth.Session;
using Meow.Extension.Helper;
using Guid = System.Guid;
using Identity = Meow.Auth.Helper.Identity;

namespace Meow.Auth.Extension
{
    /// <summary>
    /// 用户会话扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 获取当前操作人标识
        /// </summary>
        /// <param name="session">用户会话</param>
        public static Guid GetUserId(this ISession session)
        {
            return session.UserId.ToGuid();
        }

        /// <summary>
        /// 获取当前操作人标识
        /// </summary>
        /// <param name="session">用户会话</param>
        public static T GetUserId<T>(this ISession session)
        {
            return Meow.Helper.Common.To<T>(session.UserId);
        }

        /// <summary>
        /// 获取当前操作人用户名
        /// </summary>
        /// <param name="session">用户会话</param>
        public static string GetUserName(this ISession session)
        {
            var result = Identity.Instance.GetValue(ClaimType.Name);
            return string.IsNullOrWhiteSpace(result) ? Identity.Instance.GetValue(System.Security.Claims.ClaimTypes.Name) : result;
        }

        /// <summary>
        /// 获取当前操作人姓名
        /// </summary>
        /// <param name="session">用户会话</param>
        public static string GetFullName(this ISession session)
        {
            return Identity.Instance.GetValue(ClaimType.FullName);
        }

        /// <summary>
        /// 获取当前操作人电子邮件
        /// </summary>
        /// <param name="session">用户会话</param>
        public static string GetEmail(this ISession session)
        {
            var result = Identity.Instance.GetValue(ClaimType.Email);
            return string.IsNullOrWhiteSpace(result) ? Identity.Instance.GetValue(System.Security.Claims.ClaimTypes.Email) : result;
        }

        /// <summary>
        /// 获取当前操作人手机号
        /// </summary>
        /// <param name="session">用户会话</param>
        public static string GetMobile(this ISession session)
        {
            var result = Identity.Instance.GetValue(ClaimType.PhoneNumber);
            return string.IsNullOrWhiteSpace(result) ? Identity.Instance.GetValue(System.Security.Claims.ClaimTypes.MobilePhone) : result;
        }

        /// <summary>
        /// 获取当前应用程序标识
        /// </summary>
        /// <param name="session">用户会话</param>
        public static Guid GetApplicationId(this ISession session)
        {
            return Identity.Instance.GetValue(ClaimType.ApplicationId).ToGuid();
        }

        /// <summary>
        /// 获取当前应用程序标识
        /// </summary>
        /// <param name="session">用户会话</param>
        public static T GetApplicationId<T>(this ISession session)
        {
            return Meow.Helper.Common.To<T>(Identity.Instance.GetValue(ClaimType.ApplicationId));
        }

        /// <summary>
        /// 获取当前应用程序编码
        /// </summary>
        /// <param name="session">用户会话</param>
        public static string GetApplicationCode(this ISession session)
        {
            return Identity.Instance.GetValue(ClaimType.ApplicationCode);
        }

        /// <summary>
        /// 获取当前应用程序名称
        /// </summary>
        /// <param name="session">用户会话</param>
        public static string GetApplicationName(this ISession session)
        {
            return Identity.Instance.GetValue(ClaimType.ApplicationName);
        }

        /// <summary>
        /// 获取当前租户标识
        /// </summary>
        /// <param name="session">用户会话</param>
        public static Guid GetTenantId(this ISession session)
        {
            return Identity.Instance.GetValue(ClaimType.TenantId).ToGuid();
        }

        /// <summary>
        /// 获取当前租户标识
        /// </summary>
        /// <param name="session">用户会话</param>
        public static T GetTenantId<T>(this ISession session)
        {
            return Meow.Helper.Common.To<T>(Identity.Instance.GetValue(ClaimType.TenantId));
        }

        /// <summary>
        /// 获取当前租户编码
        /// </summary>
        /// <param name="session">用户会话</param>
        public static string GetTenantCode(this ISession session)
        {
            return Identity.Instance.GetValue(ClaimType.TenantCode);
        }

        /// <summary>
        /// 获取当前租户名称
        /// </summary>
        /// <param name="session">用户会话</param>
        public static string GetTenantName(this ISession session)
        {
            return Identity.Instance.GetValue(ClaimType.TenantName);
        }

        /// <summary>
        /// 获取当前操作人角色标识列表
        /// </summary>
        /// <param name="session">用户会话</param>
        public static List<Guid> GetRoleIds(this ISession session)
        {
            return session.GetRoleIds<Guid>();
        }

        /// <summary>
        /// 获取当前操作人角色标识列表
        /// </summary>
        /// <param name="session">用户会话</param>
        public static List<T> GetRoleIds<T>(this ISession session)
        {
            return Meow.Helper.String.ToList<T>(Identity.Instance.GetValue(ClaimType.RoleIds));
        }

        /// <summary>
        /// 获取当前操作人角色名
        /// </summary>
        /// <param name="session">用户会话</param>
        public static string GetRoleName(this ISession session)
        {
            return Identity.Instance.GetValue(ClaimType.RoleName);
        }
    }
}