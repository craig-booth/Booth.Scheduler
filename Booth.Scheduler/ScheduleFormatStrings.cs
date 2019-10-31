using System;
using System.Collections.Generic;
using System.Linq;

namespace Booth.Scheduler
{
    static class ScheduleFormatStrings
    {
        public static string ToOrdinalString(this int number)
        {
            var lastDigit = number % 10;

            if (lastDigit == 1)
                return number.ToString() + "st";
            else if (lastDigit == 2)
                return number.ToString() + "nd";
            else if (lastDigit == 3)
                return number.ToString() + "rd";
            else
                return number.ToString() + "th";

        }

        public static string ToCommaList(this IList<string> values)
        {
            if (values.Count == 0)
                return "";
            else if (values.Count == 1)
                return values[0];
            else
            {
                return string.Join(", ", values.Take(values.Count - 1)) + " and " + values[values.Count - 1];
            }
        }

    }
}
