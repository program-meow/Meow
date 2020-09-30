using System.ComponentModel;

namespace Meow.Parameter.Enum
{
    /// <summary>
    /// 邮箱协议
    /// </summary>
    public enum EmailAgreement
    {
        /// <summary>
        /// SMTP协议
        /// </summary>
        [Description("SMTP协议")]
        Smtp = 1,
        /// <summary>
        /// POP3协议
        /// </summary>
        [Description("POP3协议")]
        Pop3 = 2,
        /// <summary>
        /// IMAP协议
        /// </summary>
        [Description("IMAP协议")]
        Imap = 3,
    }
}