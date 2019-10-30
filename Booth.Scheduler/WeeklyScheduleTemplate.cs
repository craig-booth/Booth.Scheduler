using System;
using System.Collections;
using System.Collections.Generic;

namespace Booth.Scheduler
{
    public class WeeklyScheduleTemplate : IDateScheduleTemplate
    {
        public int Every { get; set; }
        private bool[] _Days { get; } = new bool[7];

        public WeeklyScheduleTemplate() : this(1) { }

        public WeeklyScheduleTemplate(int every)
        {
            Every = every;
        }

        public bool this[DayOfWeek dayOfWeek]
        {
            get { return _Days[(int)dayOfWeek]; }
            set { _Days[(int)dayOfWeek] = value; }
        }

        public IEnumerable<DateTime> GetDates(DateTime start)
        {
            var startDate = start.Date;
            var startOfPeriod = startDate.AddDays(-(int)start.DayOfWeek);

            // Get first date
            var nextDate = startOfPeriod;
            while (true)
            {
                if (this[nextDate.DayOfWeek] && nextDate >= startDate)
                    break;

                nextDate = nextDate.AddDays(1);
                if (nextDate.DayOfWeek == DayOfWeek.Sunday)
                    startOfPeriod = nextDate;
            }
            yield return nextDate;

            // Get subsequent dates
            while (true)
            {
                while (nextDate.DayOfWeek < DayOfWeek.Saturday)
                {
                    nextDate = nextDate.AddDays(1);

                    if (this[nextDate.DayOfWeek])
                        yield return nextDate;
                }
                startOfPeriod = startOfPeriod.AddDays(7 * Every);
                nextDate = startOfPeriod;

                if (this[nextDate.DayOfWeek])
                    yield return nextDate;
            }
        }
    }

}
