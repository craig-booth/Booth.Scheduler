using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public interface IDateScheduleTemplate
    {
    }
    
    public abstract class DateScheduleTemplate
    {
        abstract internal DateTime GetPeriodStart(DateTime date);
        abstract internal DateTime GetStartOfNextPeriod(DateTime startOfCurrentPeriod);
        abstract internal bool GetNextDateInPeriod(DateTime currentDate, bool firstTime, out DateTime nextDate);
    }
}
