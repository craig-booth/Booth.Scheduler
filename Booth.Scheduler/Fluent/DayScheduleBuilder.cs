using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler.Fluent
{
    public class DayScheduleBuilder
    {
        private readonly ScheduleTemplate _Template;
        public DayScheduleBuilder(ScheduleTemplate template)
        {
            _Template = template;
        }

        public ScheduleTemplate At(int hour, int minute)
        {
            _Template.TimeTemplate = new ExactTimeScheduleTemplate(hour, minute);

            return _Template;
        }

        public IntermediateDayScheduleBuilder EveryHour()
        {
            _Template.TimeTemplate = new HourlyScheduleTemplate();

            return new IntermediateDayScheduleBuilder(_Template);
        }
   
        public IntermediateDayScheduleBuilder EveryHours(int every)
        {
            _Template.TimeTemplate = new HourlyScheduleTemplate(every);

            return new IntermediateDayScheduleBuilder(_Template);
        }

        public IntermediateDayScheduleBuilder EveryMinute()
        {
            _Template.TimeTemplate = new MinuteScheduleTemplate();

            return new IntermediateDayScheduleBuilder(_Template);
        }

        public IntermediateDayScheduleBuilder EveryMinutes(int every)
        {
            _Template.TimeTemplate = new MinuteScheduleTemplate(every);

            return new IntermediateDayScheduleBuilder(_Template);
        }
    }

    public class IntermediateDayScheduleBuilder
    {
        private readonly ScheduleTemplate _Template;
        private readonly RecurringTimeScheduleTemplate _TimeTemplate;
        public IntermediateDayScheduleBuilder(ScheduleTemplate template)
        {
            _Template = template;
            _TimeTemplate = (RecurringTimeScheduleTemplate)template.TimeTemplate;
        }

        public IntermediateDayScheduleBuilder2 From(int hour, int minute)
        {
            _TimeTemplate.From(hour, minute);
            return new IntermediateDayScheduleBuilder2(_Template);
        }

    }

    public class IntermediateDayScheduleBuilder2
    {
        private readonly ScheduleTemplate _Template;
        private readonly RecurringTimeScheduleTemplate _TimeTemplate;
        public IntermediateDayScheduleBuilder2(ScheduleTemplate template)
        {
            _Template = template;
            _TimeTemplate = (RecurringTimeScheduleTemplate)template.TimeTemplate;
        }

        public ScheduleTemplate Until(int hour, int minute)
        {
            _TimeTemplate.To(hour, minute);
            return _Template;
        }

    }
}
