using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public interface IDateScheduleTemplate 
    {
        IEnumerable<DateTime> GetDates(DateTime start);

        IEnumerable<string> Validate();
    }
}
