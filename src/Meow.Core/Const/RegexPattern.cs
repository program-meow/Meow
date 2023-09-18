namespace Meow.Const;

/// <summary>
/// 正则表达式模式
/// </summary>
public static class RegexPattern {
    #region 字符串

    /// <summary>
    /// 空白行
    /// </summary>
    public const string BlankLine = @"\n\s*\r";

    /// <summary>
    /// 首尾空白
    /// </summary>
    public const string BeginEndBlank = @"^\s*|\s*$";

    /// <summary>
    /// 双字节字符
    /// </summary>
    public const string Dbcs = @"[^\x00-\xff]";

    /// <summary>
    /// 字母开头,数字和字母组合 一般用于 业务编码
    /// </summary>
    public const string Code = @"^[a-zA-Z0-9][a-zA-Z0-9-_]{1,100}$";

    /// <summary>
    /// 屏蔽特殊字符
    /// </summary>
    public const string ShieldSpecialCode = @"^[^<>.,?;:'()!~%\-@#/*""\s]+$";

    /// <summary>
    /// 中文
    /// </summary>
    public const string Cn = @"[\u4e00-\u9fa5]";

    /// <summary>
    /// 中文包含
    /// </summary>
    public const string ContainsCn = @"[\u4e00-\u9fa5]+";

    /// <summary>
    /// 中文、字母、数字、横线、下划线
    /// </summary>
    public const string CnEnNum = @"^[\u4e00-\u9fa5_a-zA-Z0-9_-]+$";

    /// <summary>
    /// 可以输入任意中文，字母，数字，空格组合 主要用于屏蔽特殊字符 
    /// 错误信息规定为：可以输入任意中文，空格，字母，数字组合
    /// </summary>
    public const string CnEnNumSpace = @"^(?:[\u4e00-\u9fa5]*\w*\s*)+$";

    /// <summary>
    /// 可以输入任意中文，字母，数字，空格和少部分特殊字符组合 主要用于屏蔽特殊字符
    /// 错误信息规定为：可以输入任意中文，空格，字母，数字和少部分特殊字符组合
    /// </summary>
    public const string InputText2 = @"^(?:[\-\_\+\$\&\@\#\(\)\&\#\u4e00-\u9fa5]*\w*\s*)+$";

    /// <summary>
    /// 可以输入任意中文，字母，数字，空格和部分特殊字符组合  主要用于备注和评论等场景
    /// 错误信息规定为：请不要输入特殊字符
    /// </summary>
    public const string InputText3 = @"^(?:[\@\~\#\$\%\(\)\{\}\*\/\【\】\[\]\《\》\+\-\_\、\，\。\；\：\‘\’\！\,\.\;\:\'\!\?\u3002\uff1b\uff0c\uff1a\u201c\u201d\uff08\uff09\u3001\uff1f\u300a\u300b\u4e00-\u9fa5A-Za-z0-9]*\w*\s*)+$";

    #endregion

    #region 用户信息

    /// <summary>
    /// 用户名
    /// </summary>
    public const string UserName = @"[A-Za-z0-9_\-\u4e00-\u9fa5]+";

    /// <summary>
    /// 身份证号
    /// </summary>
    public const string IdCard = @"\d{17}[\d|x]|\d{15}";

    /// <summary>
    /// 邮件
    /// </summary>
    public const string Email = @"\w[-\w.+]*@([A-Za-z0-9][-A-Za-z0-9]+\.)+[A-Za-z]{2,14}";

    /// <summary>
    /// 手机号码
    /// </summary>
    public const string Phone = @"0?(13|14|15|17|18|19)[0-9]{9}";

    /// <summary>
    /// 座机号码（国内）
    /// </summary>
    public const string Landline = @"[0-9-()（）]{7,18}";

    /// <summary>
    /// QQ号码
    /// </summary>
    public const string QQ = @"[1-9]([0-9]{5,11})";

    #endregion

    #region 地址

    /// <summary>
    /// 域名
    /// </summary>
    public const string Domain = @"[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(/.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+/.?";

    /// <summary>
    /// 网址Url
    /// </summary>
    public const string Url = @"[a-zA-z]+://[^\s]*";

    /// <summary>
    /// 邮政编码
    /// </summary>
    public const string Post = @"\d{6}";

    /// <summary>
    /// IP地址
    /// </summary>
    public const string IP = @"(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)\.(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)\.(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)\.(25[0-5]|2[0-4]\d|[0-1]\d{2}|[1-9]?\d)";

    #endregion

    #region 数字

    /// <summary>
    /// 数字
    /// </summary>
    public const string Number = @"^(-?\d*)(\.\d+)?$";

    /// <summary>
    /// 数字包含
    /// </summary>
    public const string ContainsNumber = @"[0-9]+";

    /// <summary>
    /// 匹配整数
    /// </summary>
    public const string MatchInteger = @"-?[1-9]\d*";

    /// <summary>
    /// 正整数
    /// </summary>
    public const string PositiveInteger = @"[1-9]\d*";

    /// <summary>
    /// 负整数
    /// </summary>
    public const string NegativeInteger = @"-[1-9]\d*";

    /// <summary>
    /// 正浮点数
    /// </summary>
    public const string PositiveFloat = @"[1-9]\d*.\d*|0.\d*[1-9]\d*";

    /// <summary>
    /// 负浮点数
    /// </summary>
    public const string NegativeFloat = @"-([1-9]\d*.\d*|0.\d*[1-9]\d*)";

    /// <summary>
    /// 金额
    /// </summary>
    public const string Money = @"^(?!0+(?:\.0+)?$)(?:[1-9]\d*|0)(?:\.\d{1,2})?$";

    /// <summary>
    /// 数字 - 小数点两位  
    /// </summary>
    public const string Float = @"^-?(([1 - 9]{1}\\d*)|-?([0]{1}))(\\.(\\d){ 0,2})?$";

    /// <summary>
    /// 正数 - 小数点2位
    /// </summary>
    public const string PositiveNumber = @"^((0{1}\.\d{1,2})|([1-9]\d*\.{1}\d{1,2})|([1-9]+\d*)|0)$";

    /// <summary>
    /// 正数不包含0 - 小数点2位
    /// </summary>
    public const string PositiveNumberWithOut0 = @"^((0{1}\.\d{1,2})|([1-9]\d*\.{1}\d{1,2})|([1-9]+\d*))$";

    /// <summary>
    /// 正整数不包含0
    /// </summary>
    public const string PositiveIntegerWithOut0 = @"^[1-9]\d*$";

    /// <summary>
    /// 0-1包含1
    /// </summary>
    public const string Discount = @"^(0\.\d+|1)$";

    /// <summary>
    /// 0-1不包含1
    /// </summary>
    public const string DiscountWithOut0 = @"^(0\.\d+)$";

    #endregion

    #region 时间

    /// <summary>
    /// 日期格式
    /// </summary>
    public const string DateFormat = @"\d{4}(\-|\/|.)\d{1,2}\1\d{1,2}";

    #endregion

    #region 格式

    /// <summary>
    /// 版本号
    /// </summary>
    public const string Version = @"^([1-9]\d|[0-9])(.([1-9]\d|\d)){2}$";

    /// <summary>
    /// Windows文件/文件夹命名规则
    /// </summary>
    public const string FileName = @"(?!((^(con)$)|^(con)/..*|(^(prn)$)|^(prn)/..*|(^(aux)$)|^(aux)/..*|(^(nul)$)|^(nul)/..*|(^(com)[1-9]$)|^(com)[1-9]/..*|(^(lpt)[1-9]$)|^(lpt)[1-9]/..*)|^/s+|.*/s$)(^[^/////:/*/?/""/</>/|]{1,255}$)";


    #endregion

    #region 认证方式

    /// <summary>
    /// 验证码
    /// </summary>
    public const string ValidateCode = @"^[0-9]{6}$";

    /// <summary>
    /// 6位支付密码
    /// </summary>
    public const string PayPassword = @"^[0-9]{6}$";

    /// <summary>
    /// 简易密码 ---  允许8-16字符，允许字母数字部分特殊字符[_@!.?=*]
    /// </summary>
    public const string SimplePassword = @"^[0-9a-zA-Z_@!.?=*]{8,16}$";

    /// <summary>
    /// 强密码 必须包含大小写字母和数字的组合，不能使用特殊字符，长度在8-16之间
    /// </summary>
    public const string StrongPassword = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,16}$";

    #endregion

}