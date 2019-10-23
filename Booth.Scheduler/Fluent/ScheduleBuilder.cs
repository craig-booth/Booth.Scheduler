using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler.Fluent
{
    public class ScheduleBuilder
    {
        internal readonly ScheduleTemplate Template;
        public ScheduleBuilder()
        {
            Template = new ScheduleTemplate();
        }
    } 
}
