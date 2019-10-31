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
            DateTime nextDate;
            var startDate = start.Date;
            var startOfPeriod = new DateTime(startDate.Year, startDate.Month, 01);

            // Get first date     
            while (true)
            {
                if (OccuranceType == OccuranceType.None)
                    nextDate = new DateTime(startOfPeriod.Year, startOfPeriod.Month, DayNumber);
                else
                    nextDate = GetOccurance(startOfPeriod);

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
                    nextDate = new DateTime(startOfPeriod.Year, startOfPeriod.Month, DayNumber);
                else 
                    nextDate = GetOccurance(startOfPeriod);

                yield return nextDate;
            }
        }

        private DateTime GetOccurance(DateTime startOfPeriod)
        {
            DateTime nextDate;
            int direction;
            int count;

            if (Occurance == Occurance.Last)
            {
                nextDate = new DateTime(startOfPeriod.Year, startOfPeriod.Month, DateTime.DaysInMonth(startOfPeriod.Year, startOfPeriod.Month));
                count = 1;
                direction = -1;
            }
            else
            {
                nextDate = startOfPeriod;
                count = (int)Occurance + 1;
                direction = 1;
            }


            if (ValidDate(nextDate))
                count--;

            while (count > 0)
            {
                nextDate = nextDate.AddDays(direction);
                if (ValidDate(nextDate))
                    count--;
            }

            return nextDate;
        }

        private bool ValidDate(DateTime date)
        {
            if (OccuranceType == OccuranceType.None)
                return (date.Day == DayNumber);
            else if (OccuranceType == OccuranceType.DayOfWeek)
                return (date.DayOfWeek == Day);
            else if (OccuranceType == OccuranceType.Day)
                return true;
            else if (OccuranceType == OccuranceType.Weekday)
                return (date.DayOfWeek != DayOfWeek.Saturday) && (date.DayOfWeek != DayOfWeek.Sunday);
            else
                return false;
        }

        public override string ToString()
        {
            var text = "";

            if (OccuranceType == OccuranceType.None)
                text = "on the " + DayNumber.ToOrdinalString(); 
            else if (OccuranceType == OccuranceType.DayOfWeek)
                text = "on the " + Occurance.ToString().ToLower() + " " + Day.ToString();
            else if (OccuranceType == OccuranceType.Day)
                text = "on the " + Occurance.ToString().ToLower() + " day";
            else if (OccuranceType == OccuranceType.Weekday)
                text = "on the " + Occurance.ToString().ToLower() + " weekday";

            if (Every == 1)
                text += " of every month";
            else
                text += " of every " + Every.ToString() + " months";

            return text;

        }
    }
}
