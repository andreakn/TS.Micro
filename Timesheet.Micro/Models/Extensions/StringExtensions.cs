using System;
using System.Globalization;

namespace Timesheet.Micro.Models.Extensions
{
    public static class StringExtensions
    {
        public static string ToProperCase(this string str)
        {
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(str);
        }

        public static string FormatWith(this string format, params object[] args)
        {
            return FormatWith(format, CultureInfo.InvariantCulture, args);
        } 
        
        public static string FormatWith(this string format, IFormatProvider provider, params object[] args)
        {
            if (format == null)    
                throw new ArgumentNullException("format"); 
            return string.Format(provider, format, args);
        }
    }
}
