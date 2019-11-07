﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public interface ITimeScheduleTemplate 
    {
        IEnumerable<TimeSpan> GetTimes();

        IEnumerable<string> Validate();
    }
}
