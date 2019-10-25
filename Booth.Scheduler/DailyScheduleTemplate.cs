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

        public IEnumerable<DateTime> Schedule(DateTime start)
        {
            return new DailySchedule(this, start);
        }
    }

    public class DailySchedule : IEnumerable<DateTime>
    {
        private DailyScheduleTemplate _Template;
        private DateTime _StartDate;
        public DailySchedule(DailyScheduleTemplate template, DateTime start)
        {
            _Template = template;
            _StartDate = start;
        }
        public IEnumerator<DateTime> GetEnumerator()
        {
            return new DailyScheduleEnumerator(_Template, _StartDate);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new DailyScheduleEnumerator(_Template, _StartDate); 
        }
    }

    class DailyScheduleEnumerator : IEnumerator<DateTime>
    {
        private DailyScheduleTemplate _Template;
        private DateTime _StartDate;
        private DateTime _FirstDate;
        private bool _Initialized;
        public DateTime Current { get; private set; }

        object IEnumerator.Current => Current;

        public DailyScheduleEnumerator(DailyScheduleTemplate template, DateTime start)
        {
            _Template = template;
            _StartDate = start.Date;
            _Initialized = false;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (! _Initialized)
            {
                SetFirstDate();
                _Initialized = true;
            }
            else
                SetNextDate();
            return true;
        }

        public void Reset()
        {
            Current = _FirstDate;
        }

        private void SetFirstDate()
        {
            _FirstDate = _StartDate;
            Current = _FirstDate;
        }
        private void SetNextDate()
        {
            Current = Current.AddDays(_Template.Every);
        }
    }
}
