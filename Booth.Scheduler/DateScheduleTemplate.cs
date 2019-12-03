using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Booth.Common;

namespace Booth.Scheduler
{
    public interface IDateScheduleTemplate 
    {
        IEnumerable<Date> GetDates(Date start);

        IEnumerable<string> Validate();
    }
}
