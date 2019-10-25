using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public class DailyScheduleTemplate : IDateScheduleTemplate
    {
        public int Every { get; set; }

        public DailyScheduleTemplate() : this(1) { }

        public DailyScheduleTemplate(int every)
        {
            Every = every;
        }

        public IDateScheduleEnumerator ScheduleEnumerator(DateTime start)
        {
            return new DailyScheduleEnumerator(this, start);
        }
    }

    class DailyScheduleEnumerator : IDateScheduleEnumerator
    {
        private DailyScheduleTemplate _Template;
        private DateTime _StartDate;
        public DateTime Current { get; private set; }

        object IEnumerator.Current => Current;

        public DailyScheduleEnumerator(DailyScheduleTemplate template, DateTime start)
        {
            _Template = template;
            SetFirstDate(start);
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            Current.AddDays(_Template.Every);
            return true;
        }

        public void Reset()
        {
            Current = _StartDate;
        }

        private void SetFirstDate(DateTime start)
        {
            _StartDate = start.Date;
            Current = _StartDate;
        }
    }
}
