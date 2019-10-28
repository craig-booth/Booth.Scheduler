using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public class TimeScheduleEnumerator : IEnumerator<TimeSpan>
    {
        private readonly ExactTimeScheduleTemplate _ExactTemplate;
        private readonly RecurringTimeScheduleTemplate _RecurringTemplate;
        private readonly bool _Recurring;
        private bool _Initialized;

        public TimeSpan Current { get; private set; }

        object IEnumerator.Current => Current;

        public TimeScheduleEnumerator(ITimeScheduleTemplate template)
        {
            if (template is ExactTimeScheduleTemplate)
            {
                _ExactTemplate = template as ExactTimeScheduleTemplate;
                _Recurring = false;
            }
            else
            {
                _RecurringTemplate = template as RecurringTimeScheduleTemplate;
                _Recurring = true;
            }
        }

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            if (_Recurring)
            {
                if (!_Initialized)
                {
                    _Initialized = true;
                    Current = _RecurringTemplate.FromTime;

                    return true;
                }
                else
                {
                    Current = Current.Add(_RecurringTemplate.TimeIncrement);
                    return (Current <= _RecurringTemplate.ToTime);
                }
            }
            else
            {
                if (_Initialized)
                    return false;

                _Initialized = true;
                Current = _ExactTemplate.Time;
                return true;
            }
        }

        public void Reset()
        {
            _Initialized = false;
        }
    }
}
