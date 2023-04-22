using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using Meow.Extensions;
using System.Globalization;

namespace Meow.Helpers
{
    /// <summary>
    /// 时间操作
    /// </summary>
    public static class Time
    {
        /// <summary>
        /// 日期
        /// </summary>
        private static readonly AsyncLocal<DateTime?> _dateTime = new();
        /// <summary>
        /// 是否使用Utc日期
        /// </summary>
        private static readonly AsyncLocal<bool?> _isUseUtc = new();
        /// <summary>
        /// 是否使用Utc日期
        /// </summary>
        private static bool IsUseUtc => _isUseUtc.Value != null ? Meow.Helpers.Common.SafeValue(_isUseUtc.Value) : Meow.Options.TimeOptions.IsUseUtc;

        /// <summary>
        /// 设置时间
        /// </summary>
        /// <param name="dateTime">时间</param>
        public static void SetTime(DateTime? dateTime)
        {
            _dateTime.Value = dateTime;
        }

        /// <summary>
        /// 设置时间
        /// </summary>
        /// <param name="dateTime">时间</param>
        public static void SetTime(string dateTime)
        {
            SetTime(Meow.Helpers.Convert.ToDateTimeOrNull(dateTime));
        }

        /// <summary>
        /// 设置使用Utc日期
        /// </summary>
        /// <param name="isUseUtc">是否使用Utc日期,默认值: true</param>
        public static void UseUtc(bool? isUseUtc = true)
        {
            _isUseUtc.Value = isUseUtc;
        }

        /// <summary>
        /// 重置时间和Utc标志
        /// </summary>
        public static void Reset()
        {
            _dateTime.Value = null;
            _isUseUtc.Value = null;
        }

        /// <summary>
        /// 获取当前日期时间
        /// </summary>
        public static DateTime Now
        {
            get
            {
                if (_dateTime.Value != null)
                    return _dateTime.Value.Value;
                return IsUseUtc ? DateTime.UtcNow : DateTime.Now;
            }
        }

        #region 方法

        /// <summary>
        /// 转换为标准化日期
        /// </summary>
        /// <param name="date">日期</param>
        public static DateTime? Normalize(DateTime? date)
        {
            if (date == null)
                return null;
            return Normalize(date.Value);
        }

        /// <summary>
        /// 转换为标准化日期
        /// </summary>
        /// <param name="date">日期</param>
        public static DateTime Normalize(DateTime date)
        {
            if (IsUseUtc)
                return ToUniversalTime(date);
            return ToLocalTime(date);
        }

        /// <summary>
        /// 转换为UTC日期
        /// </summary>
        /// <param name="date">日期</param>
        public static DateTime ToUniversalTime(DateTime date)
        {
            if (date == DateTime.MinValue)
                return DateTime.MinValue;
            switch (date.Kind)
            {
                case DateTimeKind.Local:
                    return date.ToUniversalTime();
                case DateTimeKind.Unspecified:
                    return DateTime.SpecifyKind(date, DateTimeKind.Local).ToUniversalTime();
                default:
                    return date;
            }
        }

        /// <summary>
        /// 转换为本地化日期
        /// </summary>
        /// <param name="date">日期</param>
        public static DateTime ToLocalTime(DateTime date)
        {
            if (date == DateTime.MinValue)
                return DateTime.MinValue;
            switch (date.Kind)
            {
                case DateTimeKind.Utc:
                    return date.ToLocalTime();
                case DateTimeKind.Unspecified:
                    return DateTime.SpecifyKind(date, DateTimeKind.Local);
                default:
                    return date;
            }
        }

        /// <summary>
        /// Utc日期转换为本地化日期
        /// </summary>
        /// <param name="date">日期</param>
        public static DateTime UtcToLocalTime(DateTime date)
        {
            if (date == DateTime.MinValue)
                return DateTime.MinValue;
            if (date.Kind == DateTimeKind.Utc)
                return date.ToLocalTime();
            if (date.Kind == DateTimeKind.Local)
                return date;
            if (IsUseUtc)
                return DateTime.SpecifyKind(date, DateTimeKind.Utc).ToLocalTime();
            return date;
        }

        /// <summary>
        /// 获取Unix时间戳
        /// </summary>
        public static long GetUnixTimestamp()
        {
            return GetUnixTimestamp(DateTime.Now);
        }

        /// <summary>
        /// 获取Unix时间戳
        /// </summary>
        /// <param name="time">时间</param>
        public static long GetUnixTimestamp(DateTime? time)
        {
            return GetUnixTimestamp(Meow.Helpers.Common.SafeValue(time));
        }

        /// <summary>
        /// 获取Unix时间戳
        /// </summary>
        /// <param name="time">时间</param>
        public static long GetUnixTimestamp(DateTime time)
        {
            DateTime start = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            long ticks = (time - start.Add(new TimeSpan(8, 0, 0))).Ticks;
            return Meow.Helpers.Convert.ToLong(ticks / TimeSpan.TicksPerSecond);
        }

        /// <summary>
        /// 从Unix时间戳获取时间
        /// </summary>
        /// <param name="timestamp">Unix时间戳</param>
        public static DateTime GetTimeFromUnixTimestamp(long? timestamp)
        {
            return GetTimeFromUnixTimestamp(Meow.Helpers.Common.SafeValue(timestamp));
        }

        /// <summary>
        /// 从Unix时间戳获取时间
        /// </summary>
        /// <param name="timestamp">Unix时间戳</param>
        public static DateTime GetTimeFromUnixTimestamp(long timestamp)
        {
            DateTime start = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            TimeSpan span = new TimeSpan(long.Parse(timestamp + "0000000"));
            return start.Add(span).Add(new TimeSpan(8, 0, 0));
        }

        #region 获取一周数字第几天

        /// <summary>
        /// 获取一周数字第几天 - 国内：周一为第一天
        /// </summary>
        /// <param name="date">日期</param>
        public static int GetNumberDayOfWeekByCn(DateTime? date)
        {
            return GetNumberDayOfWeekByCn(Meow.Helpers.Common.SafeValue(date));
        }

        /// <summary>
        /// 获取一周数字第几天 - 国内：周一为第一天
        /// </summary>
        /// <param name="date">日期</param>
        public static int GetNumberDayOfWeekByCn(DateTime date)
        {
            DayOfWeek week = date.DayOfWeek;
            int value = (int)week;
            if (value == 0)
                return 7;
            return value;
        }

        /// <summary>
        /// 获取一周数字第几天 - 国外：周日为第一天
        /// </summary>
        /// <param name="date">日期</param>
        public static int GetNumberDayOfWeekByEn(DateTime? date)
        {
            return GetNumberDayOfWeekByEn(Meow.Helpers.Common.SafeValue(date));
        }

        /// <summary>
        /// 获取一周数字第几天 - 国外：周日为第一天
        /// </summary>
        /// <param name="date">日期</param>
        public static int GetNumberDayOfWeekByEn(DateTime date)
        {
            DayOfWeek week = date.DayOfWeek;
            return (int)week + 1;
        }

        #endregion

        #region 获取星期几

        /// <summary>
        /// 获取中文星期几
        /// </summary>
        /// <param name="data">日期</param>
        public static string GetWeekNameByZh(DateTime? data)
        {
            return GetWeekNameByZh(Meow.Helpers.Common.SafeValue(data));
        }

        /// <summary>
        /// 获取中文星期几
        /// </summary>
        /// <param name="data">日期</param>
        public static string GetWeekNameByZh(DateTime data)
        {
            switch (data.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "星期日";
                case DayOfWeek.Monday:
                    return "星期一";
                case DayOfWeek.Tuesday:
                    return "星期二";
                case DayOfWeek.Wednesday:
                    return "星期三";
                case DayOfWeek.Thursday:
                    return "星期四";
                case DayOfWeek.Friday:
                    return "星期五";
                case DayOfWeek.Saturday:
                    return "星期六";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// 获取英文星期几
        /// </summary>
        /// <param name="data">日期</param>
        public static string GetWeekNameByEn(DateTime? data)
        {
            return GetWeekNameByEn(Meow.Helpers.Common.SafeValue(data));
        }

        /// <summary>
        /// 获取英文星期几
        /// </summary>
        /// <param name="data">日期</param>
        public static string GetWeekNameByEn(DateTime data)
        {
            return data.DayOfWeek.ToString();
        }

        #endregion

        #region 获取12小时制的时间

        /// <summary>
        /// 获取12小时制的中文时间。例：2023-04-22(星期六)  下午 11:22:24
        /// </summary>
        /// <param name="data">日期</param>
        public static string GetTwelveHoursTimeByZh(DateTime? data)
        {
            return GetTwelveHoursTimeByZh(Meow.Helpers.Common.SafeValue(data));
        }

        /// <summary>
        /// 获取12小时制的中文时间。例：2023-04-22(星期六)  下午 11:22:24
        /// </summary>
        /// <param name="data">日期</param>
        public static string GetTwelveHoursTimeByZh(DateTime data)
        {
            string dateTime = data.ToString("yyyy-MM-dd");
            dateTime += $"({GetWeekNameByZh(data)})";
            string time = string.Format("{0:  tt hh:mm:ss }", data);
            time = time.Replace("am", "上午").Replace("pm", "下午");
            dateTime += time;
            return dateTime;
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
            return GetDiffDays(Meow.Helpers.Common.SafeValue(firstTime), Meow.Helpers.Common.SafeValue(secondTime));
        }

        /// <summary>
        /// 获取两个日期之间相差的天数
        /// </summary>
        /// <param name="firstTime">第一个日期参数</param>
        /// <param name="secondTime">第二个日期参数</param>
        public static int GetDiffDays(DateTime firstTime, DateTime secondTime)
        {
            TimeSpan ts = (firstTime < secondTime) ? secondTime - firstTime : firstTime - secondTime;
            return ts.Days;
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
            return GetDiffHours(Meow.Helpers.Common.SafeValue(firstTime), Meow.Helpers.Common.SafeValue(secondTime));
        }

        /// <summary>
        /// 获取两个日期之间相差的小时数
        /// </summary>
        /// <param name="firstTime">第一个日期参数</param>
        /// <param name="secondTime">第二个日期参数</param>
        public static int GetDiffHours(DateTime firstTime, DateTime secondTime)
        {
            TimeSpan ts = (firstTime < secondTime) ? secondTime - firstTime : firstTime - secondTime;
            return Meow.Helpers.Convert.ToInt(ts.TotalHours);
        }

        #endregion

        #region 获取两个日期之间相差的分钟数

        /// <summary>
        /// 获取两个日期之间相差的分钟数
        /// </summary>
        /// <param name="firstTime">第一个日期参数</param>
        /// <param name="secondTime">第二个日期参数</param>
        public static int GetDiffMinutes(DateTime? firstTime, DateTime? secondTime)
        {
            return GetDiffMinutes(Meow.Helpers.Common.SafeValue(firstTime), Meow.Helpers.Common.SafeValue(secondTime));
        }

        /// <summary>
        /// 获取两个日期之间相差的分钟数
        /// </summary>
        /// <param name="firstTime">第一个日期参数</param>
        /// <param name="secondTime">第二个日期参数</param>
        public static int GetDiffMinutes(DateTime firstTime, DateTime secondTime)
        {
            TimeSpan ts = (firstTime < secondTime) ? secondTime - firstTime : firstTime - secondTime;
            return Meow.Helpers.Convert.ToInt(ts.TotalMinutes);
        }

        #endregion

        #region 获取年有多少天

        /// <summary>
        /// 获取年有多少天
        /// </summary>
        /// <param name="date">日期</param>
        public static int GetDaysOfYear(DateTime? date)
        {
            return GetDaysOfYear(Meow.Helpers.Common.SafeValue(date));
        }

        /// <summary>
        /// 获取年有多少天
        /// </summary>
        /// <param name="date">日期</param>
        public static int GetDaysOfYear(DateTime date)
        {
            return GetDaysOfYear(date.Year);
        }

        /// <summary>
        /// 获取本年有多少天
        /// </summary>
        /// <param name="year">年份</param>
        public static int GetDaysOfYear(int year)
        {
            return IsLeapYear(year) ? 366 : 365;
        }

        #endregion

        #region 获取月有多少天

        /// <summary>
        /// 获取月有多少天
        /// </summary>
        /// <param name="date">日期</param>
        public static int GetDaysOfMonth(DateTime? date)
        {
            return GetDaysOfMonth(Meow.Helpers.Common.SafeValue(date));
        }

        /// <summary>
        /// 获取月有多少天
        /// </summary>
        /// <param name="date">日期</param>
        public static int GetDaysOfMonth(DateTime date)
        {
            return GetDaysOfMonth(date.Year, date.Month);
        }

        /// <summary>
        /// 获取月有多少天
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        public static int GetDaysOfMonth(int year, int month)
        {
            switch (month)
            {
                case 1:
                    return 31;
                case 2:
                    return IsLeapYear(year) ? 29 : 28;
                case 3:
                    return 31;
                case 4:
                    return 30;
                case 5:
                    return 31;
                case 6:
                    return 30;
                case 7:
                    return 31;
                case 8:
                    return 31;
                case 9:
                    return 30;
                case 10:
                    return 31;
                case 11:
                    return 30;
                case 12:
                    return 31;
                default:
                    return 0;
            }
        }

        #endregion

        #region 获取年有多少周

        /// <summary>
        /// 获取年有多少周
        /// </summary>
        /// <param name="date">日期</param>
        public static int GetWeeksOfYear(DateTime? date)
        {
            return GetWeeksOfYear(Meow.Helpers.Common.SafeValue(date));
        }

        /// <summary>
        /// 获取年有多少周
        /// </summary>
        /// <param name="date">日期</param>
        public static int GetWeeksOfYear(DateTime date)
        {
            return GetWeeksOfYear(date.Year);
        }

        /// <summary>
        /// 获取年有多少周
        /// </summary>
        /// <param name="year">年份</param>
        public static int GetWeeksOfYear(int year)
        {
            //该年最后一天
            DateTime end = new DateTime(year, 12, 31);
            GregorianCalendar gc = new GregorianCalendar();
            //该年星期数
            return gc.GetWeekOfYear(end, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        #endregion

        #region 获取某一日期是该年中的第几周

        /// <summary>
        /// 获取某一日期是该年中的第几周
        /// </summary>
        /// <param name="date">日期</param>
        public static int GetWeekOfYear(DateTime? date)
        {
            return GetWeekOfYear(Meow.Helpers.Common.SafeValue(date));
        }

        /// <summary>
        /// 获取某一日期是该年中的第几周
        /// </summary>
        /// <param name="date">日期</param>
        public static int GetWeekOfYear(DateTime date)
        {
            GregorianCalendar gc = new GregorianCalendar();
            return gc.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        #endregion

        #region 根据年的第几周获取这周的起止日期

        /// <summary>
        /// 根据年的第几周获取这周的起止日期 - 国内：周一为第一天
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="firstDate">周开始日期</param>
        /// <param name="lastDate">周结束日期</param>
        public static void GetWeekRangeByCn(DateTime? date, out DateTime firstDate, out DateTime lastDate)
        {
            GetWeekRangeByCn(Meow.Helpers.Common.SafeValue(date), out firstDate, out lastDate);
        }

        /// <summary>
        /// 根据年的第几周获取这周的起止日期 - 国内：周一为第一天
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="firstDate">周开始日期</param>
        /// <param name="lastDate">周结束日期</param>
        public static void GetWeekRangeByCn(DateTime date, out DateTime firstDate, out DateTime lastDate)
        {
            GetWeekRangeByCn(date.Year, GetWeekOfYear(date), out firstDate, out lastDate);
        }

        /// <summary>
        /// 根据年的第几周获取这周的起止日期 - 国内：周一为第一天
        /// </summary>
        /// <param name="year">某年</param>
        /// <param name="week">第几周</param>
        /// <param name="firstDate">周开始日期</param>
        /// <param name="lastDate">周结束日期</param>
        public static void GetWeekRangeByCn(int year, int week, out DateTime firstDate, out DateTime lastDate)
        {
            //当年的第一天
            DateTime firstDay = new DateTime(year, 1, 1);

            //当年的第一天是星期几
            int firstOfWeek = Meow.Helpers.Convert.ToInt(firstDay.DayOfWeek);

            //计算当年第一周的起止日期，可能跨年
            int dayDiff = (-1) * firstOfWeek + 1;
            int dayAdd = 7 - firstOfWeek;

            firstDate = firstDay.AddDays(dayDiff).Date;
            lastDate = firstDay.AddDays(dayAdd).Date;

            //如果不是要求计算第一周
            if (week == 1)
                return;
            int addDays = (week - 1) * 7;
            firstDate = firstDate.AddDays(addDays);
            lastDate = lastDate.AddDays(addDays);
        }

        #endregion








        #endregion

        #region 判断

        #region IsSameWeek  [是否在同一周}

        /// <summary>
        /// 是否同一周 - 国内：周一为第一天
        /// </summary>
        /// <param name="times">时间集合</param>
        public static bool IsSameWeekByCn(params DateTime?[] times)
        {
            if (times == null)
                return false;
            return IsSameWeekByCn(Meow.Helpers.Common.SafeValue(times));
        }

        /// <summary>
        /// 是否同一周 - 国内：周一为第一天
        /// </summary>
        /// <param name="times">时间集合</param>
        public static bool IsSameWeekByCn(params DateTime[] times)
        {
            if (times == null)
                return false;
            return IsSameWeekByCn(times.ToList());
        }

        /// <summary>
        /// 是否同一周 - 国内：周一为第一天
        /// </summary>
        /// <param name="times">时间集合</param>
        public static bool IsSameWeekByCn(IEnumerable<DateTime?> times)
        {
            if (times == null)
                return false;
            return IsSameWeekByCn(Meow.Helpers.Common.SafeValue(times));
        }

        /// <summary>
        /// 是否同一周 - 国内：周一为第一天
        /// </summary>
        /// <param name="times">时间集合</param>
        public static bool IsSameWeekByCn(IEnumerable<DateTime> times)
        {
            if (times == null)
                return false;
            DateTime[] values = times.ToArray();
            if (values.Length < 2)
                return true;
            for (int i = 0; i < values.Length; i++)
            {
                for (int j = 0; j < values.Length; j++)
                {
                    if ((i - j) > 0)
                        continue;
                    if (IsSameWeekByCn(values[i], values[j]))
                        continue;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 是否同一周 - 国内：周一为第一天
        /// </summary>
        /// <param name="firstTime">第一个日期参数</param>
        /// <param name="secondTime">第二个日期参数</param>
        private static bool IsSameWeekByCn(DateTime firstTime, DateTime secondTime)
        {
            TimeSpan ts = (firstTime < secondTime) ? secondTime - firstTime : firstTime - secondTime;
            double dbl = ts.TotalDays;
            int intDow = Meow.Helpers.Convert.ToInt((firstTime < secondTime) ? secondTime.DayOfWeek : firstTime.DayOfWeek);
            if (intDow == 0)
                intDow = 7;
            if (dbl >= 7 || dbl >= intDow)
                return false;
            return true;
        }

        /// <summary>
        /// 是否同一周 - 国外：周日为第一天
        /// </summary>
        /// <param name="times">时间集合</param>
        public static bool IsSameWeekByEn(params DateTime?[] times)
        {
            if (times == null)
                return false;
            return IsSameWeekByEn(Meow.Helpers.Common.SafeValue(times));
        }

        /// <summary>
        /// 是否同一周 - 国外：周日为第一天
        /// </summary>
        /// <param name="times">时间集合</param>
        public static bool IsSameWeekByEn(params DateTime[] times)
        {
            if (times == null)
                return false;
            return IsSameWeekByEn(times.ToList());
        }

        /// <summary>
        /// 是否同一周 - 国外：周日为第一天
        /// </summary>
        /// <param name="times">时间集合</param>
        public static bool IsSameWeekByEn(IEnumerable<DateTime?> times)
        {
            if (times == null)
                return false;
            return IsSameWeekByEn(Meow.Helpers.Common.SafeValue(times));
        }

        /// <summary>
        /// 是否同一周 - 国外：周日为第一天
        /// </summary>
        /// <param name="times">时间集合</param>
        public static bool IsSameWeekByEn(IEnumerable<DateTime> times)
        {
            if (times == null)
                return false;
            DateTime[] values = times.ToArray();
            if (values.Length < 2)
                return true;
            for (int i = 0; i < values.Length; i++)
            {
                for (int j = 0; j < values.Length; j++)
                {
                    if ((i - j) > 0)
                        continue;
                    if (IsSameWeekByEn(values[i], values[j]))
                        continue;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 是否同一周 - 国外：周日为第一天
        /// </summary>
        /// <param name="firstTime">第一个日期参数</param>
        /// <param name="secondTime">第二个日期参数</param>
        private static bool IsSameWeekByEn(DateTime firstTime, DateTime secondTime)
        {
            TimeSpan ts = (firstTime < secondTime) ? secondTime - firstTime : firstTime - secondTime;
            double dbl = ts.TotalDays;
            int intDow = Meow.Helpers.Convert.ToInt((firstTime < secondTime) ? secondTime.DayOfWeek : firstTime.DayOfWeek) + 1;
            if (dbl >= 7 || dbl >= intDow)
                return false;
            return true;
        }

        #endregion

        #region IsLeapYear  [是否是闰年]

        /// <summary>
        /// 是否是闰年
        /// </summary>
        /// <param name="date">日期</param>
        public static bool IsLeapYear(DateTime? date)
        {
            return IsLeapYear(Meow.Helpers.Common.SafeValue(date));
        }

        /// <summary>
        /// 是否是闰年
        /// </summary>
        /// <param name="date">日期</param>
        public static bool IsLeapYear(DateTime date)
        {
            return IsLeapYear(date.Year);
        }

        /// <summary>
        /// 是否是闰年
        /// </summary>
        /// <param name="year">年</param>
        public static bool IsLeapYear(int year)
        {
            return (year % 400 == 0) || (year % 4 == 0 && year % 100 != 0);
        }

        #endregion





        #endregion

    }
}
