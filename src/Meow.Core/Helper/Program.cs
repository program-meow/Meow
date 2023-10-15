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

        /// <summary>
        /// 获取物理路径
        /// </summary>
        /// <param name="relativePath">相对路径,范例:"test/a.txt" 或 "/test/a.txt"</param>
        /// <param name="basePath">基路径,默认为AppContext.BaseDirectory</param>
        public static string GetPhysicalPath( string relativePath , string basePath = null ) {
            if( relativePath.StartsWith( "~" ) )
                relativePath = relativePath.TrimStart( '~' );
            if( relativePath.StartsWith( "/" ) )
                relativePath = relativePath.TrimStart( '/' );
            if( relativePath.StartsWith( "\\" ) )
                relativePath = relativePath.TrimStart( '\\' );
            basePath ??= Meow.Helper.Program.BaseDirectory;
            return Path.Combine( basePath , relativePath );
        }

        /// <summary>
        /// 连接路径
        /// </summary>
        /// <param name="paths">路径列表</param>
        public static string JoinPath( params string[] paths ) {
            return Url.JoinPath( paths );
        }

        /// <summary>
        /// 获取当前目录路径
        /// </summary>
        public static string GetCurrentDirectory() {
            return Directory.GetCurrentDirectory();
        }

        /// <summary>
        /// 获取当前目录的上级路径
        /// </summary>
        /// <param name="depth">向上钻取的深度</param>
        public static string GetParentDirectory( int depth = 1 ) {
            var path = Directory.GetCurrentDirectory();
            for( int i = 0 ; i < depth ; i++ ) {
                var parent = Directory.GetParent( path );
                if( parent is { Exists: true } )
                    path = parent.FullName;
            }
            return path;
        }
    }
}
