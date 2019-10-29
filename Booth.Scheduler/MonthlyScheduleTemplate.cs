using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public enum Occurance { First, Second, Third, Fourth, Last }
    public enum OccuranceType { None, DayOfWeek, Day, Weekday }
    public class MonthlyScheduleTemplate : DateScheduleTemplate, IDateScheduleTemplate
    {
        public int Every { get; set; }

        public int DayNumber { get; set; }

        public OccuranceType OccuranceType { get; set; }
        public Occurance Occurance { get; set; }
        public DayOfWeek Day { get; set; }

        public MonthlyScheduleTemplate() : this(1) { }

        public MonthlyScheduleTemplate(int every)
        {
            Every = every;
        }

        protected override DateTime GetPeriodStart(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 01);
        }

        protected override DateTime GetStartOfNextPeriod(DateTime startOfCurrentPeriod)
        {
            return startOfCurrentPeriod.AddMonths(1);
        }

        protected override bool GetNextDateInPeriod(DateTime currentDate, bool firstTime, out DateTime nextDate)
        {
            if (OccuranceType == OccuranceType.None)
                return GetNextDateInPeriodDayNumber(currentDate, firstTime, out nextDate);
            else if (OccuranceType == OccuranceType.DayOfWeek)
                return GetNextDateInPeriodDayOfWeekOccurance(currentDate, firstTime, out nextDate);
            else if (OccuranceType == OccuranceType.Day)
                return GetNextDateInPeriodDayOccurance(currentDate, firstTime, out nextDate);
            else
            {
                nextDate = currentDate;
                return true;
            }
                
        }

        private bool GetNextDateInPeriodDayNumber(DateTime currentDate, bool firstTime, out DateTime nextDate)
        {
            if (currentDate.Day < DayNumber)
                nextDate = new DateTime(currentDate.Year, currentDate.Month, DayNumber);
            else
                nextDate = currentDate;

            if (nextDate.Day > DayNumber)
                return false;
            else if ((nextDate.Day == DayNumber) && firstTime)
                return false;
            else
                return true;
        }

        private bool GetNextDateInPeriodDayOccurance(DateTime currentDate, bool firstTime, out DateTime nextDate)
        {
            throw new NotSupportedException();
        }
        private bool GetNextDateInPeriodDayOfWeekOccurance(DateTime currentDate, bool firstTime, out DateTime nextDate)
        {
            throw new NotSupportedException();
        }
    }
}
