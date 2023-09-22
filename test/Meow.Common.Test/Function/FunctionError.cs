using Xunit.Sdk;

namespace Meow.Common.Test.Function {
    /// <summary>
    /// 错误函数
    /// </summary>
    public static class FunctionError {
        /// <summary>
        /// 无返回值
        /// </summary>
        public static void Void() {
        }

        /// <summary>
        /// 返Bool
        /// </summary>
        public static bool ReturnBool() {
            return false;
        }
    }
}
