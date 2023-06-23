using System;
using System.Globalization;

namespace Directum.Core
{
    public static class StringExtensions
    {
        public static bool TryDateTimeParse(this string s, out DateTime outTime)
        {
            return DateTime.TryParseExact(s, "d.M.yyyy_HH:mm",null, DateTimeStyles.None, out outTime);
        }
        public static bool TryTimeSpanParse(this string s, out TimeSpan outTimeSpan)
        {
            if (!TimeSpan.TryParseExact(s, @"hh\:mm", null, TimeSpanStyles.None, out outTimeSpan)) return false;
            return outTimeSpan.Hours < 24;
        }
    }
}