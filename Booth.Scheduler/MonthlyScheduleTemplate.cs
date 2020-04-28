using System;
using System.Collections.Generic;
using System.Linq;

using Booth.Common;

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

        public IEnumerable<Date> GetDates(Date start)
        {
            Date nextDate;
            var startDate = start;
            var startOfPeriod = new Date(startDate.Year, startDate.Month, 01);

            // Get first date     
            while (true)
            {
                if (OccuranceType == OccuranceType.None)
                    nextDate = new Date(startOfPeriod.Year, startOfPeriod.Month, DayNumber);
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
                {
                    var daysInMonth = DateTime.DaysInMonth(startOfPeriod.Year, startOfPeriod.Month);
                    nextDate = new Date(startOfPeriod.Year, startOfPeriod.Month, Math.Min(DayNumber, daysInMonth));
                }
                else
                    nextDate = GetOccurance(startOfPeriod);

                yield return nextDate;
            }
        }

        private Date GetOccurance(Date startOfPeriod)
        {
            Date nextDate;
            int direction;
            int count;

            if (Occurance == Occurance.Last)
            {
                nextDate = new Date(startOfPeriod.Year, startOfPeriod.Month, DateTime.DaysInMonth(startOfPeriod.Year, startOfPeriod.Month));
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

        private bool ValidDate(Date date)
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
        public IEnumerable<string> Validate()
        {
            if (Every < 1)
                yield return "Monthly schedule must occur atleast every 1 months";

            if (OccuranceType == OccuranceType.None)
            {
                if ((DayNumber < 1) || (DayNumber > 31))
                    yield return "Day number must be between 1 and 31";
            }
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
