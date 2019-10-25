using System;
using System.Collections;
using System.Collections.Generic;

namespace Booth.Scheduler
{
    public class WeeklyScheduleTemplate : IDateScheduleTemplate
    {
        public int Every { get; set; }
        private bool[] _Days { get; } = new bool[7];

        public WeeklyScheduleTemplate() : this(1) { }

        public WeeklyScheduleTemplate(int every)
        {
            Every = every;
        }

        public bool this[DayOfWeek dayOfWeek]
        {
            get { return _Days[(int)dayOfWeek]; }
            set { _Days[(int)dayOfWeek] = value; }
        }
        public IEnumerable<DateTime> Schedule(DateTime start)
        {
            return new WeelySchedule(this, start);
        }
    }


    public class WeelySchedule : IEnumerable<DateTime>
    {
        private WeeklyScheduleTemplate _Template;
        private DateTime _StartDate;
        public WeelySchedule(WeeklyScheduleTemplate template, DateTime start)
        {
            _Template = template;
            _StartDate = start;
        }
        public IEnumerator<DateTime> GetEnumerator()
        {
            return new WeeklyScheduleEnumerator(_Template, _StartDate);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new WeeklyScheduleEnumerator(_Template, _StartDate);
        }
    }

    class WeeklyScheduleEnumerator : IEnumerator<DateTime>
    {
        private WeeklyScheduleTemplate _Template;
        private DateTime _StartDate;
        private DateTime _PeriodStart;
        private DateTime _FirstDate;
        private bool _Initialized;
        private bool _NewPeriodStarted;
        public DateTime Current { get; private set; }

        object IEnumerator.Current => Current;

        public WeeklyScheduleEnumerator(WeeklyScheduleTemplate template, DateTime start)
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
                SetFirstDate();
                _Initialized = true;
            }
            else
                SetNextDate();
            return true;
        }

        public void Reset()
        {
            _Initialized = false;
        }

        private void SetFirstDate()
        {
            SetPeriodStart(_StartDate);

            while (true)
            {
                SetNextDate();

                if (Current >= _StartDate)
                    return;
            }
        }

        private void SetNextDate()
        {
            if (GetNextDateInPeriod())
                return;

            MoveToNextPeriod();

            GetNextDateInPeriod();
        }

        private bool GetNextDateInPeriod()
        {
            while (Current.DayOfWeek < DayOfWeek.Saturday)
            {
                if (!_NewPeriodStarted)
                    Current = Current.AddDays(1);
                else
                    _NewPeriodStarted = false;

                if (_Template[Current.DayOfWeek])
                    return true;
            }

            return false;
        }
        
        private void MoveToNextPeriod()
        {
            _PeriodStart = _PeriodStart.AddDays(_Template.Every * 7);
            Current = _PeriodStart;
            _NewPeriodStarted = true;
        }
        private void SetPeriodStart(DateTime date)
        {
            _PeriodStart = date.AddDays(-(int)date.DayOfWeek);
            Current = _PeriodStart;
            _NewPeriodStarted = true;
        }
    }
}
