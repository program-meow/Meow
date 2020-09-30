using System.ComponentModel;

namespace Meow.Parameter.Enum
{
    /// <summary>
    /// 邮箱  暂只录入网易163邮箱，配合邮件发送
    /// </summary>
    public enum Email
    {
        /// <summary>
        /// 网易163邮箱
        /// </summary>
        [Description("网易163邮箱")]
        NetEase163 = 1,
    }
}