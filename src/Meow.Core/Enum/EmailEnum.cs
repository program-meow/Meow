using System.ComponentModel;

namespace Meow.Enum
{
    /// <summary>
    /// 邮箱  暂只录入网易163邮箱，配合邮件发送
    /// </summary>
    public enum EmailEnum
    {
        /// <summary>
        /// 网易163邮箱
        /// </summary>
        [Description("网易163邮箱")]
        NetEase163 = 1,
    }
}