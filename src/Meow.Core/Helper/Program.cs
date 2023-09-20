namespace Meow.Helper {
    /// <summary>
    /// 程序操作
    /// </summary>
    public static class Program {
        /// <summary>
        /// 获取当前基路径
        /// </summary>
        public static string BaseDirectory => AppContext.BaseDirectory;
        /// <summary>
        /// 是否Linux操作系统
        /// </summary>
        public static bool IsLinux => RuntimeInformation.IsOSPlatform( OSPlatform.Linux );
        /// <summary>
        /// 是否Windows操作系统
        /// </summary>
        public static bool IsWindows => RuntimeInformation.IsOSPlatform( OSPlatform.Windows );
    }
}
