using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Meow.Exception;
using Meow.Extension.Helper;

namespace Meow.Extension.Validation
{
    /// <summary>
    /// 检测为空扩展
    /// </summary>
    public static partial class Extension
    {
        /// <summary>
        /// 检测对象是否为null,为null则抛出<see cref="ArgumentNullException"/>异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckNull(this object value, string parameterName)
        {
            if (!value.IsNull())
                return;
            throw new ArgumentNullException(parameterName);
        }

        /// <summary>
        /// 检测值是否为null,为null则抛出异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="errorMessage">错误消息</param>
        public static async Task<T> CheckNullAsync<T>(this Task<T> value, string errorMessage)
        {
            var data = await value;
            if (!value.IsNull())
                return data;
            throw new Warning(errorMessage);
        }

        /// <summary>
        /// 检测是否为空,为空则抛出异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckEmpty(this string value, string parameterName = "")
        {
            if (!value.IsEmpty())
                return;
            throw new Warning($"{parameterName}不能为空");
        }

        /// <summary>
        /// 检测是否为空,为空则抛出异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckEmpty(this Guid value, string parameterName = "")
        {
            if (!value.IsEmpty())
                return;
            throw new Warning($"{parameterName}不能为空");
        }

        /// <summary>
        /// 检测是否为空,为空则抛出异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckEmpty(this Guid? value, string parameterName = "")
        {
            if (!value.IsEmpty())
                return;
            throw new Warning($"{parameterName}不能为空");
        }

        /// <summary>
        /// 检测是否为空,为空则抛出异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckEmpty<T>(this IEnumerable<T> value, string parameterName = "")
        {
            if (!value.IsEmpty())
                return;
            throw new Warning($"{parameterName}不能为空");
        }

        /// <summary>
        /// 检测自定义验证,检测不通过则抛出异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="regexString">正则表达式字符串</param>
        /// <param name="errorMessage">错误消息</param>
        public static void CheckMatch(this string value, string regexString, string errorMessage)
        {
            if (value.IsMatch(regexString))
                return;
            throw new Warning(errorMessage);
        }

        /// <summary>
        /// 检测是否数字,检测不通过则抛出异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckNumeric(this string value, string parameterName = "")
        {
            if (value.IsNumeric())
                return;
            throw new Warning($"{parameterName}数字格式不正确");
        }

        /// <summary>
        /// 检测是否数字（带正负号）,检测不通过则抛出异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckNumberSign(this string value, string parameterName = "")
        {
            if (value.IsNumberSign())
                return;
            throw new Warning($"{parameterName}数字格式不正确");
        }

        /// <summary>
        /// 检测是否是小数,检测不通过则抛出异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckDecimal(this string value, string parameterName = "")
        {
            if (value.IsDecimal())
                return;
            throw new Warning($"{parameterName}小数格式不正确");
        }

        /// <summary>
        /// 检测是否是小数（带正负号）,检测不通过则抛出异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckDecimalSign(this string value, string parameterName = "")
        {
            if (value.IsDecimalSign())
                return;
            throw new Warning($"{parameterName}小数格式不正确");
        }

        /// <summary>
        /// 检测是否中文,检测不通过则抛出异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckChinese(this string value, string parameterName = "")
        {
            if (value.IsChinese())
                return;
            throw new Warning($"{parameterName}中文格式不正确");
        }

        /// <summary>
        /// 检测是否是邮箱,检测不通过则抛出异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckEmail(this string value, string parameterName = "")
        {
            if (value.IsEmail())
                return;
            throw new Warning($"{parameterName}邮箱地址格式不正确");
        }

        /// <summary>
        /// 检测是否是邮政编码,检测不通过则抛出异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckPostcode(this string value, string parameterName = "")
        {
            if (value.IsPostcode())
                return;
            throw new Warning($"{parameterName}邮政编码格式不正确");
        }

        /// <summary>
        /// 检测是否是URL,检测不通过则抛出异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckUrl(this string value, string parameterName = "")
        {
            if (value.IsUrl())
                return;
            throw new Warning($"{parameterName}URL格式不正确");
        }

        /// <summary>
        /// 检测是否日期,检测不通过则抛出异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckDate(this string value, string parameterName = "")
        {
            if (value.IsDate())
                return;
            throw new Warning($"{parameterName}日期格式不正确");
        }

        /// <summary>
        /// 检测是否日期时间,检测不通过则抛出异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckDatetime(this string value, string parameterName = "")
        {
            if (value.IsDatetime())
                return;
            throw new Warning($"{parameterName}日期时间格式不正确");
        }

        /// <summary>
        /// 检测是否为合法的电话号码,检测不通过则抛出异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckTel(this string value, string parameterName = "")
        {
            if (value.IsTel())
                return;
            throw new Warning($"{parameterName}电话号码格式不正确");
        }

        /// <summary>
        /// 检测是否为合法的手机号码,检测不通过则抛出异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckMobile(this string value, string parameterName = "")
        {
            if (value.IsMobile())
                return;
            throw new Warning($"{parameterName}手机号格式不正确");
        }

        /// <summary>
        /// 检测是否为合法的用户名（限中文/英文/数字/减号/下划线）,检测不通过则抛出异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckUserName(this string value, string parameterName = "")
        {
            if (value.IsUserName())
                return;
            throw new Warning($"{parameterName}用户名格式不正确");
        }

        /// <summary>
        /// 检测是否为合法的IPv4地址,检测不通过则抛出异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckIp(this string value, string parameterName = "")
        {
            if (value.IsIp())
                return;
            throw new Warning($"{parameterName}IPv4地址格式不正确");
        }

        /// <summary>
        /// 检测是否合法身份证号,检测不通过则抛出异常
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckIdCard(this string value, string parameterName = "")
        {
            if (value.IsIdCard())
                return;
            throw new Warning($"{parameterName}身份证号格式不正确");
        }
    }
}