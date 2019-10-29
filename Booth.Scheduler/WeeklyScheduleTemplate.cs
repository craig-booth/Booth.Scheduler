using System;
using System.Collections;
using System.Collections.Generic;

namespace Booth.Scheduler
{
    public class WeeklyScheduleTemplate : DateScheduleTemplate
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

        protected override DateTime GetPeriodStart(DateTime date)
        {
            return date.AddDays(-(int)date.DayOfWeek);
        }

        protected override DateTime GetStartOfNextPeriod(DateTime startOfCurrentPeriod)
        {
            return startOfCurrentPeriod.AddDays(Every * 7);
        }

        protected override bool GetNextDateInPeriod(DateTime currentDate, bool firstTime, out DateTime nextDate)
        {
            nextDate = currentDate;

            while (nextDate.DayOfWeek < DayOfWeek.Saturday)
            {
                if (!firstTime)
                    nextDate = nextDate.AddDays(1);
                else
                    firstTime = false;

                if (this[nextDate.DayOfWeek])
                    return true;
            }

            return false;
        }
    }

}
