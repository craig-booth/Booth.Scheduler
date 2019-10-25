using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public interface ITimeScheduleEnumerator : IEnumerator<DateTime> { }
    public interface ITimeScheduleTemplate
    {
        ITimeScheduleEnumerator ScheduleEnumerator(DateTime date);
    }

}
