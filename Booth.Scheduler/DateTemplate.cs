using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public enum DateUnit { Days, Weeks, Months, Years }
    public enum Occurance { None, First, Second, Third, Fourth, Last }

    public class DateTemplate
    {
        public int Every { get; set; }
        public DateUnit Units { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public Occurance Occurance { get; set; }
        public bool[] Flags { get; } = new bool[31];

        public DateTemplate(int every, DateUnit units)
        {
            Every = every;
            Units = units;
            FromDate = new DateTime(0001, 01, 01);
            ToDate = new DateTime(9999, 12, 31);
        }
        public DateTemplate(int every, DateUnit units, DateTime fromDate, DateTime toDate)
        {
            Every = every;
            Units = units;
            FromDate = fromDate;
            ToDate = toDate;
        }
    }

    public class DailyScheduleTemplate
    {
        public int Every { get; set; }
    }

    public class WeeklyScheduleTemplate
    {
        public int Every { get; set; }
        public bool[] Days { get; } = new bool[7];
    }

    public class MonthlyScheduleTemplate
    {
        public int Every { get; set; }

        public int DayNumber { get; set; }
        public Occurance Occurance { get; set; }
        public DayOfWeek Day { get; set; }
    }




}
