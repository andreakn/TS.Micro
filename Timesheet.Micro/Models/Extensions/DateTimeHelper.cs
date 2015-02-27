using System;
using System.Globalization;

namespace Timesheet.Micro.Models.Extensions
{
    public static class DateTimeHelper
    {
        public static DateTime FirstDateOfWeek(int year, int weekNum)
        {
            return FirstDateOfWeek(year, weekNum, DateTimeFormatInfo.CurrentInfo.CalendarWeekRule);
        }

        public static DateTime FirstDateOfWeek(int year, int weekNum, CalendarWeekRule rule)
        {
            var jan1 = new DateTime(year, 1, 1);
            var daysOffset = 
                (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            var firstMonday = jan1.AddDays(daysOffset);
            var firstWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(jan1, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule, 
                CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

            if (firstWeek <= 1 || (firstWeek >= 52 && (int)jan1.DayOfWeek < 4))
            {
                weekNum -= 1;
            }
            return firstMonday.AddDays(weekNum * 7);
        }

        public static DateTime FirstDateOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime LastDateOfMonth(this DateTime date)
        {
            int daysinMonth = DateTime.DaysInMonth(date.Year, date.Month);
            return new DateTime(date.Year, date.Month, daysinMonth);
        }

        public static string ToShortWeekName(this DateTime date)
        {
            return date.ToString("dddd", CultureInfo.CurrentCulture).ToProperCase().Substring(0, 3);
        }

        public static string ToFriendly(this DateTime? date)
        {
            if(date.HasValue)
                return date.Value.ToFriendly();
            return "?";
        }

        public static string ToFriendly(this DateTime date)
        {
           return date.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
        }

        public static string ToShortDate(this DateTime? date)
        {
            if (date != null)
            {
                return date.Value.ToShortDateString();
            }
            return string.Empty;
        }
        
        public static string ToYearDateString(this DateTime date)
        {
            string ret = date.ToShortDateString();
            string sep = DateTimeFormatInfo.CurrentInfo.DateSeparator;
            ret = ret.Substring(0, ret.LastIndexOf(sep, StringComparison.CurrentCulture));
            return ret;
        }

        public static int GetDayOfWeek(this DateTime date)
        {
            return (int)CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(date);
        }

        public static int GetWeeksInYear(int year)
        {
            var cal = CultureInfo.CurrentCulture.Calendar;
            return cal.GetWeekOfYear(new DateTime(year, 12, 31), DateTimeFormatInfo.CurrentInfo.CalendarWeekRule, DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);
        }
        
        public static int GetWeekForDate(this DateTime date)
        {
            var cal = CultureInfo.CurrentCulture.Calendar;
            return cal.GetWeekOfYear(date, DateTimeFormatInfo.CurrentInfo.CalendarWeekRule, DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);
        }

        public static int GetWeekForDate(this DateTime date, CalendarWeekRule rule)
        {
            var cal = CultureInfo.CurrentCulture.Calendar;
            return cal.GetWeekOfYear(date, rule, DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek); 
        }

        public static bool CompareDate(this DateTime date1, DateTime date2)
        {
            return date1.Year.Equals(date2.Year) && date1.Month.Equals(date2.Month)
                   && date1.Day.Equals(date2.Day);
        }
    }
}
