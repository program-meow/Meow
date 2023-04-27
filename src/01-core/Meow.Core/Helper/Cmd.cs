using System.Diagnostics;
using System.IO;

namespace Meow.Helper
{
    /// <summary>
    /// Dos cmd命令执行操作
    /// </summary>
    public static class Cmd
    {
        /// <summary>
        /// 运行dos命令
        /// </summary>
        /// <Param name="command">命令</Param>
        public static string Run(string command)
        {
            string result = string.Empty;
            if (Validation.IsEmpty(command))
                return result;
            ProcessStartInfo startInfo = new ProcessStartInfo("cmd", "/c " + command)
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
            using (Process process = Process.Start(startInfo))
            {
                using (StreamReader reader = process.StandardOutput)
                    result = reader.ReadToEnd();
                process.WaitForExit();
            }
            return result.Trim();
        }
    }
}
