namespace Meow.Parameter.Message
{
    /// <summary>
    /// 数据消息
    /// </summary>
    public static class Data
    {
        /// <summary>
        /// 并发消息
        /// </summary>
        public const string ConcurrencyExceptionMessage = "当前操作的数据已被其他人修改，请刷新后重试";
    }
}
