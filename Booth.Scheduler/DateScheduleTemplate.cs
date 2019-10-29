using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public interface IDateScheduleTemplate
    {
        IEnumerable<DateTime> GetDates(DateTime start);
    }
    
    public abstract class DateScheduleTemplate : IDateScheduleTemplate
    {
        public IEnumerable<DateTime> GetDates(DateTime start)
        {
            var periodStart = GetPeriodStart(start.Date);
            var currentDate = periodStart;
            var newPeriodStarted = true;

            while (true)
            {
                if (!GetNextDateInPeriod(currentDate, newPeriodStarted, out var nextDate))
                {
                    periodStart = GetStartOfNextPeriod(periodStart);
                    GetNextDateInPeriod(periodStart, true, out nextDate);               
                }
                
                if (nextDate >= start.Date)
                    yield return nextDate;

                currentDate = nextDate;
                newPeriodStarted = false;
            }
        }

        abstract protected DateTime GetPeriodStart(DateTime date);
        abstract protected DateTime GetStartOfNextPeriod(DateTime startOfCurrentPeriod);
        abstract protected bool GetNextDateInPeriod(DateTime currentDate, bool firstTime, out DateTime nextDate);

    }
}
