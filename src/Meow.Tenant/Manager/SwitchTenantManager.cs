﻿namespace Meow.Tenant.Manager;

/// <summary>
/// 切换租户管理器
/// </summary>
public class SwitchTenantManager : ISwitchTenantManager {
    /// <summary>
    /// 切换租户键名
    /// </summary>
    public const string Key = "x-switch-tenant";

    /// <inheritdoc />
    public string GetSwitchTenantId() {
        return Meow.Helper.Web.GetCookie( Key );
    }

    /// <inheritdoc />
    public Task SwitchTenantAsync( string tenantId ) {
        Meow.Helper.Web.SetCookie( Key , tenantId );
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task ResetTenantAsync() {
        Meow.Helper.Web.RemoveCookie( Key );
        return Task.CompletedTask;
    }
}