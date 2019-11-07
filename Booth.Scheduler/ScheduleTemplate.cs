using System;
using System.Collections.Generic;
using System.Linq;

namespace Booth.Scheduler
{
    public class ScheduleTemplate
    {
        public IDateScheduleTemplate DateTemplate { get; internal set; }
        public ITimeScheduleTemplate TimeTemplate { get; internal set; }

        public ScheduleTemplate(IDateScheduleTemplate dateTemplate, ITimeScheduleTemplate timeTemplate)
        {
            DateTemplate = dateTemplate;
            TimeTemplate = timeTemplate;
        }

        public ScheduleTemplate(IDateScheduleTemplate dateTemplate)
        {
            DateTemplate = dateTemplate;
            TimeTemplate = new ExactTimeScheduleTemplate(0, 0);
        }

        public IEnumerable<string> Validate()
        {
            if (DateTemplate != null)
            {
                foreach (var error in DateTemplate.Validate())
                    yield return error;
            }
            else
                yield return "Date Template not provided.";

            if (TimeTemplate != null)
            {
                foreach (var error in TimeTemplate.Validate())
                    yield return error;
            }
            else
                yield return "Time Template not provided.";
        }

        public override string ToString()
        {
            return "Run " + DateTemplate.ToString() + ", " + TimeTemplate.ToString();
        }
    }
}
