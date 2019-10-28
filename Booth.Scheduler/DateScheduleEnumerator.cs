using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public class DateScheduleEnumerator : IEnumerator<DateTime>
    {
        private DateScheduleTemplate _Template;
        private DateTime _StartDate;
        private DateTime _PeriodStart;
        private bool _Initialized;
        private bool _NewPeriodStarted;
        public DateTime Current { get; private set; }

        object IEnumerator.Current => Current;

        public DateScheduleEnumerator(DateScheduleTemplate template, DateTime start)
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
            if (!_Initialized)
            {
                _Initialized = true;

                _PeriodStart = _Template.GetPeriodStart(_StartDate);
                Current = _PeriodStart;
                _NewPeriodStarted = true;
            }

            while (true)
            {
                SetNextDate();

                if (Current >= _StartDate)
                    break;
            }

            return true;
        }

        public void Reset()
        {
            _Initialized = false;
        }

        private void SetNextDate()
        {
            if (!GetNextDateInPeriod())
            {
                _PeriodStart = _Template.GetStartOfNextPeriod(_PeriodStart);
                Current = _PeriodStart;
                _NewPeriodStarted = true;

                GetNextDateInPeriod();
            }
        }

        private bool GetNextDateInPeriod()
        {
            var found = _Template.GetNextDateInPeriod(Current, _NewPeriodStarted, out var nextDate);
            if (found)
                Current = nextDate;

            _NewPeriodStarted = false;
            return found;
        }
    }

}
