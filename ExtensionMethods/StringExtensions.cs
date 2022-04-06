using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    public static class StringExtensions
    {
        public static DateTime? MutedDateTime(this string datetimeString)
        {
            try
            {
                var d = DateTime.Parse(datetimeString);
                return d;
            }
            catch
            {
                return null;
            }
        }

        public static int MutedToInt(this string valueString)
        {
            try
            {
                return Int32.Parse(valueString);
            }
            catch
            {
                return 0;
            }
        }

        public static string MutedOnlyDate(this string dateTimeString) 
        {
            try
            {
                var d = DateTime.Parse(dateTimeString);
                return d.ToString("yyyy-MM-dd");
            }
            catch
            {
                return String.Empty;
            }
        }

    }
}
