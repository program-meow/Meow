namespace Meow.Tenant.Manager;

/// <summary>
/// 查看租户管理器
/// </summary>
public class ViewAllTenantManager : IViewAllTenantManager {
    /// <summary>
    /// 查看所有租户键名
    /// </summary>
    public const string Key = "x-view-all-tenant";

    /// <inheritdoc />
    public bool IsDisableTenantFilter() {
        string result = Meow.Helper.Web.GetCookie( Key );
        if( result.IsEmpty() )
            return false;
        return result.ToBool();
    }

    /// <inheritdoc />
    public Task EnableViewAllAsync() {
        Meow.Helper.Web.SetCookie( Key , "true" );
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task DisableViewAllAsync() {
        Meow.Helper.Web.RemoveCookie( Key );
        return Task.CompletedTask;
    }
}