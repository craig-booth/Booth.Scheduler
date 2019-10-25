using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public interface IDateScheduleEnumerator : IEnumerator<DateTime> { }
    public interface IDateScheduleTemplate
    {
        IDateScheduleEnumerator ScheduleEnumerator(DateTime start);
    }
}
