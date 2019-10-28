using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

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
            return new ScheduleEnumerator(_Template, _Start);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ScheduleEnumerator(_Template, _Start);
        }

        private class ScheduleEnumerator : IEnumerator<DateTime>
        {
            private bool _Initalized;
            private readonly IEnumerator<DateTime> _DateEnumerator;
            private readonly IEnumerator<TimeSpan> _TimeEnumerator;

            public DateTime Current { get; private set; }

            object IEnumerator.Current => Current;

            public ScheduleEnumerator(ScheduleTemplate template, DateTime start)
            {
                _DateEnumerator = new DateScheduleEnumerator(template.DateTemplate as DateScheduleTemplate, start);
                _TimeEnumerator = new TimeScheduleEnumerator(template.TimeTemplate);
                _Initalized = false;
            }

            public void Dispose()
            {
                _DateEnumerator.Dispose();
                _TimeEnumerator.Dispose();
            }

            public bool MoveNext()
            {
                if (!_Initalized)
                {
                    if (!_DateEnumerator.MoveNext())
                        return false;
                }

                if (_TimeEnumerator.MoveNext())
                {
                    Current = _DateEnumerator.Current.Add(_TimeEnumerator.Current);
                    return true;
                }
                
                if (_DateEnumerator.MoveNext())
                {
                    _TimeEnumerator.Reset();
                    _TimeEnumerator.MoveNext();
                    Current = _DateEnumerator.Current.Add(_TimeEnumerator.Current);
                    return true;
                }

                return false;
            }

            public void Reset()
            {
                _DateEnumerator.Reset();
                _TimeEnumerator.Reset();
                _Initalized = false;
            }
        }


        
    }

}
