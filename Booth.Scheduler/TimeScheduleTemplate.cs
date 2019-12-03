using System;
using System.Collections.Generic;
using System.Text;

using Booth.Common;

namespace Booth.Scheduler
{
    public interface ITimeScheduleTemplate 
    {
        IEnumerable<Time> GetTimes();

        IEnumerable<string> Validate();
    }
}
