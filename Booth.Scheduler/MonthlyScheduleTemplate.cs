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
            return startOfCurrentPeriod.AddMonths(Every);
        }

        protected override bool GetNextDateInPeriod(DateTime currentDate, bool firstTime, out DateTime nextDate)
        {
            if (OccuranceType == OccuranceType.None)
                return GetNextDateInPeriodDayNumber(currentDate, firstTime, out nextDate);
            else if (OccuranceType == OccuranceType.DayOfWeek)
                return GetNextDateInPeriodDayOfWeekOccurance(currentDate, firstTime, out nextDate);
            else if (OccuranceType == OccuranceType.Day)
                return GetNextDateInPeriodDayOccurance(currentDate, firstTime, out nextDate);
            else if (OccuranceType == OccuranceType.Weekday)
                return GetNextDateInPeriodWeekdayOccurance(currentDate, firstTime, out nextDate);
            else
            {
                nextDate = currentDate;
                return true;
            }
                
        }

        private bool GetNextDateInPeriodDayNumber(DateTime currentDate, bool firstTime, out DateTime nextDate)
        {
            nextDate = new DateTime(currentDate.Year, currentDate.Month, DayNumber);
            return firstTime;
        }

        private bool GetNextDateInPeriodDayOccurance(DateTime currentDate, bool firstTime, out DateTime nextDate)
        {
            throw new NotSupportedException();
        }
        private bool GetNextDateInPeriodDayOfWeekOccurance(DateTime currentDate, bool firstTime, out DateTime nextDate)
        {
            int count = 0;
            int direction;

            if (Occurance == Occurance.Last)
            {
                nextDate = new DateTime(currentDate.Year, currentDate.Month, DateTime.DaysInMonth(currentDate.Year, currentDate.Month));
                count = 1;
                direction = -1;
            }
            else
            {
                nextDate = new DateTime(currentDate.Year, currentDate.Month, 01);
                direction = 1;

                if (Occurance == Occurance.First)
                    count = 1;
                else if (Occurance == Occurance.Second)
                    count = 2;
                else if (Occurance == Occurance.Third)
                    count = 3;
                else if (Occurance == Occurance.Fourth)
                    count = 4;
            }

            if (nextDate.DayOfWeek == Day)
                count--;

            while (count > 0)
            {
                nextDate = nextDate.AddDays(direction);
                if (nextDate.DayOfWeek == Day)
                    count--;
            }

            if (firstTime && (nextDate >= currentDate))
                return true;
            else if (nextDate > currentDate)
                return true;
            else
                return false;
        }

        private bool GetNextDateInPeriodWeekdayOccurance(DateTime currentDate, bool firstTime, out DateTime nextDate)
        {
            int count = 0;
            int direction;

            if (Occurance == Occurance.Last)
            {
                nextDate = new DateTime(currentDate.Year, currentDate.Month, DateTime.DaysInMonth(currentDate.Year, currentDate.Month));
                count = 1;
                direction = -1;
            }
            else
            {
                nextDate = new DateTime(currentDate.Year, currentDate.Month, 01);
                direction = 1;

                if (Occurance == Occurance.First)
                    count = 1;
                else if (Occurance == Occurance.Second)
                    count = 2;
                else if (Occurance == Occurance.Third)
                    count = 3;
                else if (Occurance == Occurance.Fourth)
                    count = 4;
            }

            if ((nextDate.DayOfWeek != DayOfWeek.Saturday) || (nextDate.DayOfWeek != DayOfWeek.Saturday))
                count--;

            while (count > 0)
            {
                nextDate = nextDate.AddDays(direction);
                if ((nextDate.DayOfWeek != DayOfWeek.Saturday) || (nextDate.DayOfWeek != DayOfWeek.Saturday))
                    count--;
            }

            if (firstTime && (nextDate >= currentDate))
                return true;
            else if (nextDate > currentDate)
                return true;
            else
                return false;
        }
    }
}
