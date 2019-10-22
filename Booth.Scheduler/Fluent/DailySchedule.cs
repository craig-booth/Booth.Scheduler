using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler.Fluent
{
    public class DailyScheduleBuilder
    {
        public DailyScheduleBuilder(int every)
            : base(new DateTemplate(every, DateUnit.Days))
        {

        }
    }
}
