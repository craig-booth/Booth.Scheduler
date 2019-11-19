using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Booth.Scheduler
{
    public class ScheduleInstance : IEnumerable<DateTime>
    {
        private readonly ScheduleTemplate _Template;
        private readonly DateTime _Start;

        public ScheduleInstance(ScheduleTemplate template, DateTime start)
        {
            _Template = template;
            _Start = start;
        }

        public IEnumerator<DateTime> GetEnumerator()
        {
            var errors = _Template.Validate();
            if (errors.Any())
            {
                throw new Exception(errors.First());
            }

            var dates = _Template.DateTemplate.GetDates(_Start);
            var times = _Template.TimeTemplate.GetTimes();

            foreach (var date in dates)
            {
                foreach (var time in times)
                {
                    yield return date.Add(time);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() 
        {
            return this.GetEnumerator();
        }     
    }

}
