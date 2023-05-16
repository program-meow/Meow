﻿using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using Meow.Extension;

namespace Meow.Helper;

/// <summary>
/// Ip地址操作
/// </summary>
public static class Ip
{
    /// <summary>
    /// Ip地址
    /// </summary>
    private static readonly AsyncLocal<string> _ip = new();

    /// <summary>
    /// 设置Ip地址
    /// </summary>
    /// <param name="ip">Ip地址</param>
    public static void SetIp(string ip)
    {
        _ip.Value = ip;
    }

    /// <summary>
    /// 重置Ip地址
    /// </summary>
    public static void Reset()
    {
        _ip.Value = null;
    }

    /// <summary>
    /// 获取客户端Ip地址
    /// </summary>
    public static string GetIp()
    {
        if (string.IsNullOrWhiteSpace(_ip.Value) == false)
            return _ip.Value;
        string[] list = new[] { "127.0.0.1", "::1" };
        string result = Web.HttpContext?.Connection.RemoteIpAddress.SafeString();
        if (string.IsNullOrWhiteSpace(result) || list.Contains(result))
            result = Meow.Helper.Application.IsWindows ? GetLanIp() : GetLanIp(NetworkInterfaceType.Ethernet);
        return result;
    }

    /// <summary>
    /// 获取局域网IP
    /// </summary>
    private static string GetLanIp()
    {
        foreach (IPAddress hostAddress in Dns.GetHostAddresses(Dns.GetHostName()))
        {
            if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                return hostAddress.ToString();
        }
        return string.Empty;
    }

    /// <summary>
    /// 获取局域网IP
    /// </summary>
    /// <param name="type">网络接口类型</param>
    private static string GetLanIp(NetworkInterfaceType type)
    {
        try
        {
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType != type || item.OperationalStatus != OperationalStatus.Up)
                    continue;
                IPInterfaceProperties ipProperties = item.GetIPProperties();
                if (ipProperties.GatewayAddresses.FirstOrDefault() == null)
                    continue;
                foreach (UnicastIPAddressInformation ip in ipProperties.UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        return ip.Address.ToString();
                }
            }
        }
        catch
        {
            return string.Empty;
        }
        return string.Empty;
    }
}