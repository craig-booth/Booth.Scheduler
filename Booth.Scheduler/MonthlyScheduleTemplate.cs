using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public enum Occurance { First, Second, Third, Fourth, Last }
    public enum OccuranceType { None, DayOfWeek, Day, Weekday }
    public class MonthlyScheduleTemplate : IDateScheduleTemplate
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

        public IEnumerable<DateTime> GetDates(DateTime start)
        {
            var startDate = start.Date;
            var startOfPeriod = new DateTime(startDate.Year, startDate.Month, 01);

            // Get first date
            DateTime nextDate;
            while (true)
            {
                if (OccuranceType == OccuranceType.None)
                    nextDate = GetDayNumber(startOfPeriod);
                else if (OccuranceType == OccuranceType.DayOfWeek)
                    nextDate = GetOccuranceDayOfWeek(startOfPeriod);
                else if (OccuranceType == OccuranceType.Day)
                    nextDate = GetOccuranceDay(startOfPeriod);
                else if (OccuranceType == OccuranceType.Weekday)
                    nextDate = GetOccuranceWeekDay(startOfPeriod);
                else
                    yield break;

                if (nextDate >= startDate)
                    break;

                startOfPeriod = startOfPeriod.AddMonths(1);
            }
            yield return nextDate;

            // Get subsequent dates
            while (true)
            {
                startOfPeriod = startOfPeriod.AddMonths(Every);

                if (OccuranceType == OccuranceType.None)
                    nextDate = GetDayNumber(startOfPeriod);
                else if (OccuranceType == OccuranceType.DayOfWeek)
                    nextDate = GetOccuranceDayOfWeek(startOfPeriod);
                else if (OccuranceType == OccuranceType.Day)
                    nextDate = GetOccuranceDay(startOfPeriod);
                else if (OccuranceType == OccuranceType.Weekday)
                    nextDate = GetOccuranceWeekDay(startOfPeriod);
                else
                    yield break;

                yield return nextDate;
            }
        }

        private DateTime GetDayNumber(DateTime startOfPeriod)
        {
            return new DateTime(startOfPeriod.Year, startOfPeriod.Month, DayNumber);
        }
        private DateTime GetOccuranceDayOfWeek(DateTime startOfPeriod)
        {
            DateTime nextDate;
            int count = 0;
            int direction;

            if (Occurance == Occurance.Last)
            {
                nextDate = new DateTime(startOfPeriod.Year, startOfPeriod.Month, DateTime.DaysInMonth(startOfPeriod.Year, startOfPeriod.Month));
                count = 1;
                direction = -1;
            }
            else
            {
                nextDate = startOfPeriod;
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

            return nextDate;
        }
        private DateTime GetOccuranceDay(DateTime startOfPeriod)
        {
            DateTime nextDate;
            int count = 0;

            if (Occurance == Occurance.Last)
            {
                nextDate = new DateTime(startOfPeriod.Year, startOfPeriod.Month, DateTime.DaysInMonth(startOfPeriod.Year, startOfPeriod.Month));
            }
            else
            {
                if (Occurance == Occurance.First)
                    count = 1;
                else if (Occurance == Occurance.Second)
                    count = 2;
                else if (Occurance == Occurance.Third)
                    count = 3;
                else if (Occurance == Occurance.Fourth)
                    count = 4;

                nextDate = new DateTime(startOfPeriod.Year, startOfPeriod.Month, count);
            }

            return nextDate;
        }
        private DateTime GetOccuranceWeekDay(DateTime startOfPeriod)
        {
            DateTime nextDate;
            int count = 0;
            int direction;

            if (Occurance == Occurance.Last)
            {
                nextDate = new DateTime(startOfPeriod.Year, startOfPeriod.Month, DateTime.DaysInMonth(startOfPeriod.Year, startOfPeriod.Month));
                count = 1;
                direction = -1;
            }
            else
            {
                nextDate = startOfPeriod;
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

            if ((nextDate.DayOfWeek != DayOfWeek.Saturday) && (nextDate.DayOfWeek != DayOfWeek.Sunday))
                count--;

            while (count > 0)
            {
                nextDate = nextDate.AddDays(direction);
                if ((nextDate.DayOfWeek != DayOfWeek.Saturday) && (nextDate.DayOfWeek != DayOfWeek.Sunday))
                    count--;
            }

            return nextDate;
        }
    }
}
