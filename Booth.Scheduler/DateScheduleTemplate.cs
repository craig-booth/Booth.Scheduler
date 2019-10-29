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
                var found = GetNextDateInPeriod(currentDate, newPeriodStarted, out var nextDate);

                if (found)
                {
                    currentDate = nextDate;
                    newPeriodStarted = false;
                }
                else
                {
                    periodStart = GetStartOfNextPeriod(periodStart);
                    currentDate = periodStart;

                    found = GetNextDateInPeriod(currentDate, true, out nextDate);
                    newPeriodStarted = false;
                    if (found)
                        currentDate = nextDate;
                }

                if (currentDate >= start.Date)
                    yield return currentDate;
            }
        }

        abstract protected DateTime GetPeriodStart(DateTime date);
        abstract protected DateTime GetStartOfNextPeriod(DateTime startOfCurrentPeriod);
        abstract protected bool GetNextDateInPeriod(DateTime currentDate, bool firstTime, out DateTime nextDate);

    }
}
