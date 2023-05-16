using System;
using System.Runtime.InteropServices;

namespace Meow.Helper
{
    /// <summary>
    /// 应用操作
    /// </summary>
    public static class Application
    {
        /// <summary>
        /// 获取当前应用程序基路径
        /// </summary>
        public static string BaseDirectory => AppContext.BaseDirectory;
        /// <summary>
        /// 是否Linux操作系统
        /// </summary>
        public static bool IsLinux => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        /// <summary>
        /// 是否Windows操作系统
        /// </summary>
        public static bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    }
}
