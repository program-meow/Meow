namespace Meow.Enum;

/// <summary>
/// 邮箱协议
/// </summary>
public enum EmailAgreementEnum {
    /// <summary>
    /// SMTP协议
    /// </summary>
    [Description( "SMTP协议" )]
    SMTP = 1,
    /// <summary>
    /// POP3协议
    /// </summary>
    [Description( "POP3协议" )]
    POP3 = 2,
    /// <summary>
    /// IMAP协议
    /// </summary>
    [Description( "IMAP协议" )]
    IMAP = 3,
}