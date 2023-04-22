using System;
using System.Collections.Generic;
using System.Globalization;

namespace Meow.Extensions
{
    /// <summary>
    /// 时间扩展
    /// </summary>
    public static class TimeExtensions
    {
        #region 方法

        /// <summary>
        /// 转换为标准化日期
        /// </summary>
        /// <param name="date">日期</param>
        public static DateTime? ToNormalize(this DateTime? date)
        {
            return Meow.Helpers.Time.Normalize(date);
        }

        /// <summary>
        /// 转换为标准化日期
        /// </summary>
        /// <param name="date">日期</param>
        public static DateTime ToNormalize(this DateTime date)
        {
            return Meow.Helpers.Time.Normalize(date);
        }

        /// <summary>
        /// 转换为UTC日期
        /// </summary>
        /// <param name="date">日期</param>
        public static DateTime ToUniversalTime(this DateTime date)
        {
            return Meow.Helpers.Time.ToUniversalTime(date);
        }

        /// <summary>
        /// 转换为本地化日期
        /// </summary>
        /// <param name="date">日期</param>
        public static DateTime ToLocalTime(this DateTime date)
        {
            return Meow.Helpers.Time.ToLocalTime(date);
        }

        /// <summary>
        /// Utc日期转换为本地化日期
        /// </summary>
        /// <param name="date">日期</param>
        public static DateTime UtcToLocalTime(this DateTime date)
        {
            return Meow.Helpers.Time.UtcToLocalTime(date);
        }

        /// <summary>
        /// 获取Unix时间戳
        /// </summary>
        /// <param name="time">时间</param>
        public static long GetUnixTimestamp(this DateTime? time)
        {
            return Meow.Helpers.Time.GetUnixTimestamp(time);
        }

        /// <summary>
        /// 获取Unix时间戳
        /// </summary>
        /// <param name="time">时间</param>
        public static long GetUnixTimestamp(this DateTime time)
        {
            return Meow.Helpers.Time.GetUnixTimestamp(time);
        }

        /// <summary>
        /// 从Unix时间戳获取时间
        /// </summary>
        /// <param name="timestamp">Unix时间戳</param>
        public static DateTime GetTimeFromUnixTimestamp(this long? timestamp)
        {
            return Meow.Helpers.Time.GetTimeFromUnixTimestamp(timestamp);
        }

        /// <summary>
        /// 从Unix时间戳获取时间
        /// </summary>
        /// <param name="timestamp">Unix时间戳</param>
        public static DateTime GetTimeFromUnixTimestamp(this long timestamp)
        {
            return Meow.Helpers.Time.GetTimeFromUnixTimestamp(timestamp);
        }

        #region 获取一周数字第几天

        /// <summary>
        /// 获取一周数字第几天 - 国内：周一为第一天
        /// </summary>
        /// <param name="date">日期</param>
        public static int GetNumberDayOfWeekByCn(this DateTime? date)
        {
            return Meow.Helpers.Time.GetNumberDayOfWeekByCn(date);
        }

        /// <summary>
        /// 获取一周数字第几天 - 国内：周一为第一天
        /// </summary>
        /// <param name="date">日期</param>
        public static int GetDayOfWeekByCn(this DateTime date)
        {
            return Meow.Helpers.Time.GetNumberDayOfWeekByCn(date);
        }

        /// <summary>
        /// 获取一周数字第几天 - 国外：周日为第一天
        /// </summary>
        /// <param name="date">日期</param>
        public static int GetNumberDayOfWeekByEn(this DateTime? date)
        {
            return Meow.Helpers.Time.GetNumberDayOfWeekByEn(date);
        }

        /// <summary>
        /// 获取一周数字第几天 - 国外：周日为第一天
        /// </summary>
        /// <param name="date">日期</param>
        public static int GetNumberDayOfWeekByEn(this DateTime date)
        {
            return Meow.Helpers.Time.GetNumberDayOfWeekByEn(date);
        }

        #endregion

        #region 获取星期几

        /// <summary>
        /// 获取中文星期几
        /// </summary>
        /// <param name="data">日期</param>
        public static string GetWeekNameByZh(this DateTime? data)
        {
            return Meow.Helpers.Time.GetWeekNameByZh(data);
        }

        /// <summary>
        /// 获取中文星期几
        /// </summary>
        /// <param name="data">日期</param>
        public static string GetWeekNameByZh(this DateTime data)
        {
            return Meow.Helpers.Time.GetWeekNameByZh(data);
        }

        /// <summary>
        /// 获取英文星期几
        /// </summary>
        /// <param name="data">日期</param>
        public static string GetWeekNameByEn(this DateTime? data)
        {
            return Meow.Helpers.Time.GetWeekNameByEn(data);
        }

        /// <summary>
        /// 获取英文星期几
        /// </summary>
        /// <param name="data">日期</param>
        public static string GetWeekNameByEn(this DateTime data)
        {
            return Meow.Helpers.Time.GetWeekNameByEn(data);
        }

        #endregion

        #region 获取12小时制的时间

        /// <summary>
        /// 获取12小时制的中文时间。例：2023-04-22(星期六)  下午 11:22:24
        /// </summary>
        /// <param name="data">日期</param>
        public static string GetTwelveHoursTimeByZh(this DateTime? data)
        {
            return Meow.Helpers.Time.GetTwelveHoursTimeByZh(data);
        }

        /// <summary>
        /// 获取12小时制的中文时间。例：2023-04-22(星期六)  下午 11:22:24
        /// </summary>
        /// <param name="data">日期</param>
        public static string GetTwelveHoursTimeByZh(this DateTime data)
        {
            return Meow.Helpers.Time.GetTwelveHoursTimeByZh(data);
        }

        #endregion

        #region 获取两个日期之间相差的天数

        /// <summary>
        /// 获取两个日期之间相差的天数
        /// </summary>
        /// <param name="firstTime">第一个日期参数</param>
        /// <param name="secondTime">第二个日期参数</param>
        public static int GetDiffDays(DateTime? firstTime, DateTime? secondTime)
        {
            return Meow.Helpers.Time.GetDiffDays(firstTime, secondTime);
        }

        /// <summary>
        /// 获取两个日期之间相差的天数
        /// </summary>
        /// <param name="firstTime">第一个日期参数</param>
        /// <param name="secondTime">第二个日期参数</param>
        public static int GetDiffDays(DateTime firstTime, DateTime secondTime)
        {
            return Meow.Helpers.Time.GetDiffDays(firstTime, secondTime);
        }

        #endregion

        #region 获取两个日期之间相差的小时数

        /// <summary>
        /// 获取两个日期之间相差的小时数
        /// </summary>
        /// <param name="firstTime">第一个日期参数</param>
        /// <param name="secondTime">第二个日期参数</param>
        public static int GetDiffHours(DateTime? firstTime, DateTime? secondTime)
        {
            return Meow.Helpers.Time.GetDiffHours(firstTime, secondTime);
        }

        /// <summary>
        /// 获取两个日期之间相差的小时数
        /// </summary>
        /// <param name="firstTime">第一个日期参数</param>
        /// <param name="secondTime">第二个日期参数</param>
        public static int GetDiffHours(DateTime firstTime, DateTime secondTime)
        {
            return Meow.Helpers.Time.GetDiffHours(firstTime, secondTime);
        }

        #endregion

        #region 获取两个日期之间相差的分钟数

        /// <summary>
        /// 获取两个日期之间相差的分钟数
        /// </summary>
        /// <param name="firstTime">第一个日期参数</param>
        /// <param name="secondTime">第二个日期参数</param>
        public static int GetDiffMinutes(this DateTime? firstTime, DateTime? secondTime)
        {
            return Meow.Helpers.Time.GetDiffMinutes(firstTime, secondTime);
        }

        /// <summary>
        /// 获取两个日期之间相差的分钟数
        /// </summary>
        /// <param name="firstTime">第一个日期参数</param>
        /// <param name="secondTime">第二个日期参数</param>
        public static int GetDiffMinutes(this DateTime firstTime, DateTime secondTime)
        {
            return Meow.Helpers.Time.GetDiffMinutes(firstTime, secondTime);
        }

        #endregion

        #region 获取年有多少天

        /// <summary>
        /// 获取年有多少天
        /// </summary>
        /// <param name="date">日期</param>
        public static int GetDaysOfYear(this DateTime? date)
        {
            return Meow.Helpers.Time.GetDaysOfYear(date);
        }

        /// <summary>
        /// 获取年有多少天
        /// </summary>
        /// <param name="date">日期</param>
        public static int GetDaysOfYear(this DateTime date)
        {
            return Meow.Helpers.Time.GetDaysOfYear(date);
        }

        #endregion

        #region 获取月有多少天

        /// <summary>
        /// 获取月有多少天
        /// </summary>
        /// <param name="date">日期</param>
        public static int GetDaysOfMonth(this DateTime? date)
        {
            return Meow.Helpers.Time.GetDaysOfMonth(date);
        }

        /// <summary>
        /// 获取月有多少天
        /// </summary>
        /// <param name="date">日期</param>
        public static int GetDaysOfMonth(this DateTime date)
        {
            return Meow.Helpers.Time.GetDaysOfMonth(date);
        }

        #endregion

        #region 获取年有多少周

        /// <summary>
        /// 获取年有多少周
        /// </summary>
        /// <param name="date">日期</param>
        public static int GetWeeksOfYear(this DateTime? date)
        {
            return Meow.Helpers.Time.GetWeeksOfYear(date);
        }

        /// <summary>
        /// 获取年有多少周
        /// </summary>
        /// <param name="date">日期</param>
        public static int GetWeeksOfYear(this DateTime date)
        {
            return Meow.Helpers.Time.GetWeeksOfYear(date);
        }

        #endregion

        #region 获取某一日期是该年中的第几周

        /// <summary>
        /// 获取某一日期是该年中的第几周
        /// </summary>
        /// <param name="date">日期</param>
        public static int GetWeekOfYear(this DateTime? date)
        {
            return Meow.Helpers.Time.GetWeekOfYear(date);
        }

        /// <summary>
        /// 获取某一日期是该年中的第几周
        /// </summary>
        /// <param name="date">日期</param>
        public static int GetWeekOfYear(this DateTime date)
        {
            return Meow.Helpers.Time.GetWeekOfYear(date);
        }

        #endregion

        #region 根据年的第几周获取这周的起止日期

        /// <summary>
        /// 根据年的第几周获取这周的起止日期 - 国内：周一为第一天
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="firstDate">周开始日期</param>
        /// <param name="lastDate">周结束日期</param>
        public static void GetWeekRangeByCn(this DateTime? date, out DateTime firstDate, out DateTime lastDate)
        {
            Meow.Helpers.Time.GetWeekRangeByCn(date, out firstDate, out lastDate);
        }

        /// <summary>
        /// 根据年的第几周获取这周的起止日期 - 国内：周一为第一天
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="firstDate">周开始日期</param>
        /// <param name="lastDate">周结束日期</param>
        public static void GetWeekRangeByCn(DateTime date, out DateTime firstDate, out DateTime lastDate)
        {
            Meow.Helpers.Time.GetWeekRangeByCn(date, out firstDate, out lastDate);
        }

        #endregion



        #endregion

        #region 判断

        #region IsSameWeek  [是否在同一周}

        /// <summary>
        /// 是否同一周 - 国内：周一为第一天
        /// </summary>
        public static bool IsSameWeekCn(this DateTime? date, params DateTime?[] times)
        {
            List<DateTime?> value = new List<DateTime?> { date };
            value.AddRange(times);
            return Meow.Helpers.Time.IsSameWeekByCn(value);
        }

        /// <summary>
        /// 是否同一周 - 国内：周一为第一天
        /// </summary>
        public static bool IsSameWeekByCn(this DateTime date, params DateTime[] times)
        {
            List<DateTime> value = new List<DateTime> { date };
            value.AddRange(times);
            return Meow.Helpers.Time.IsSameWeekByCn(value);
        }

        /// <summary>
        /// 是否同一周 - 国内：周一为第一天
        /// </summary>
        public static bool IsSameWeekByCn(this IEnumerable<DateTime?> array)
        {
            return Meow.Helpers.Time.IsSameWeekByCn(array);
        }

        /// <summary>
        /// 是否同一周 - 国内：周一为第一天
        /// </summary>
        public static bool IsSameWeekByCn(this IEnumerable<DateTime> array)
        {
            return Meow.Helpers.Time.IsSameWeekByCn(array);
        }

        /// <summary>
        /// 是否同一周 - 国外：周日为第一天
        /// </summary>
        public static bool IsSameWeekByEn(this DateTime? date, params DateTime?[] times)
        {
            List<DateTime?> value = new List<DateTime?> { date };
            value.AddRange(times);
            return Meow.Helpers.Time.IsSameWeekByEn(value);
        }

        /// <summary>
        /// 是否同一周 - 国外：周日为第一天
        /// </summary>
        public static bool IsSameWeekByEn(this DateTime date, params DateTime[] times)
        {
            List<DateTime> value = new List<DateTime> { date };
            value.AddRange(times);
            return Meow.Helpers.Time.IsSameWeekByEn(value);
        }

        /// <summary>
        /// 是否同一周 - 国外：周日为第一天
        /// </summary>
        public static bool IsSameWeekByEn(this IEnumerable<DateTime?> array)
        {
            return Meow.Helpers.Time.IsSameWeekByEn(array);
        }

        /// <summary>
        /// 是否同一周 - 国外：周日为第一天
        /// </summary>
        public static bool IsSameWeekByEn(this IEnumerable<DateTime> array)
        {
            return Meow.Helpers.Time.IsSameWeekByEn(array);
        }

        #endregion

        /// <summary>
        /// 是否是闰年
        /// </summary>
        /// <param name="date">日期</param>
        public static bool IsLeapYear(this DateTime? date)
        {
            return Meow.Helpers.Time.IsLeapYear(date);
        }

        /// <summary>
        /// 是否是闰年
        /// </summary>
        /// <param name="date">日期</param>
        public static bool IsLeapYear(this DateTime date)
        {
            return Meow.Helpers.Time.IsLeapYear(date);
        }

        #endregion
    }
}
