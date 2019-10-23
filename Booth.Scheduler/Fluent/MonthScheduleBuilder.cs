using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler.Fluent
{

    public class MonthScheduleBuilder 
    {
        private readonly ScheduleTemplate _Template;
        private readonly MonthlyScheduleTemplate _DateTemplate;
        public MonthScheduleBuilder(ScheduleTemplate template)
        {
            _Template = template;
            _DateTemplate = (MonthlyScheduleTemplate)template.DateTemplate;
        }

        public DayScheduleBuilder OnDay(int day)
        {
            _DateTemplate.DayNumber = day;

            return new DayScheduleBuilder(_Template);
        }

        public DayScheduleBuilder OnFirst(DayOfWeek day)
        {
            _DateTemplate.Occurance = Occurance.First;
            _DateTemplate.Day = day;

            return new DayScheduleBuilder(_Template);
        }

        public DayScheduleBuilder OnSecond(DayOfWeek day)
        {
            _DateTemplate.Occurance = Occurance.Second;
            _DateTemplate.Day = day;

            return new DayScheduleBuilder(_Template);
        }
        public DayScheduleBuilder OnThird(DayOfWeek day)
        {
            _DateTemplate.Occurance = Occurance.Third;
            _DateTemplate.Day = day;

            return new DayScheduleBuilder(_Template);
        }

        public DayScheduleBuilder OnFourth(DayOfWeek day)
        {
            _DateTemplate.Occurance = Occurance.Fourth;
            _DateTemplate.Day = day;

            return new DayScheduleBuilder(_Template);
        }

        public DayScheduleBuilder OnLast(DayOfWeek day)
        {
            _DateTemplate.Occurance = Occurance.Last;
            _DateTemplate.Day = day;

            return new DayScheduleBuilder(_Template);
        }

    } 

}
