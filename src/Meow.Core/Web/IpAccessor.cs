using Meow.Helper;

namespace Meow.Web;

/// <summary>
/// IP访问器
/// </summary>
public class IpAccessor : IIpAccessor {
    /// <inheritdoc />
    public string GetIp() {
        return Ip.GetIp();
    }
}