using System;

namespace Tacmin.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime FirstDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }

        public static int DaysInMonth(this DateTime value)
        {
            return DateTime.DaysInMonth(value.Year, value.Month);
        }

        public static DateTime LastDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.DaysInMonth());
        }

        public static DateTime AddSmartMonths(this DateTime value, int numberOfMonths)
        {
            var isEndDate = DateTime.DaysInMonth(value.Year, value.Month) == value.Day;
            if (isEndDate)
            {
                var newDateTime = value.AddMonths(numberOfMonths);
                return new DateTime(newDateTime.Year, newDateTime.Month, DateTime.DaysInMonth(newDateTime.Year, newDateTime.Month));
            }
            return value.AddMonths(numberOfMonths);
        }
    }
}
