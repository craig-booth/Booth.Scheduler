using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler.Fluent
{
    public class WeekScheduleBuilder 
    {
        private readonly ScheduleTemplate _Template;
        private readonly WeeklyScheduleTemplate _DateTemplate;
        public WeekScheduleBuilder(ScheduleTemplate template)
        {
            _Template = template;
            _DateTemplate = (WeeklyScheduleTemplate)template.DateTemplate;
        }

        public IntermediateWeeklyScheduleBuilder OnMonday()
        {
            _DateTemplate[DayOfWeek.Monday] = true;

            return new IntermediateWeeklyScheduleBuilder(_Template);
        }

        public IntermediateWeeklyScheduleBuilder OnTuesday()
        {
            _DateTemplate[DayOfWeek.Tuesday] = true;

            return new IntermediateWeeklyScheduleBuilder(_Template);
        }

        public IntermediateWeeklyScheduleBuilder OnWednesday()
        {
            _DateTemplate[DayOfWeek.Wednesday] = true;

            return new IntermediateWeeklyScheduleBuilder(_Template);
        }

        public IntermediateWeeklyScheduleBuilder OnThursday()
        {
            _DateTemplate[DayOfWeek.Thursday] = true;

            return new IntermediateWeeklyScheduleBuilder(_Template);
        }

        public IntermediateWeeklyScheduleBuilder OnFriday()
        {
            _DateTemplate[DayOfWeek.Friday] = true;

            return new IntermediateWeeklyScheduleBuilder(_Template);
        }

        public IntermediateWeeklyScheduleBuilder OnSaturday()
        {
            _DateTemplate[DayOfWeek.Saturday] = true;

            return new IntermediateWeeklyScheduleBuilder(_Template);
        }

        public IntermediateWeeklyScheduleBuilder OnSunday()
        {
            _DateTemplate[DayOfWeek.Monday] = true;

            return new IntermediateWeeklyScheduleBuilder(_Template);
        }

        public DayScheduleBuilder OnWeekdays()
        {
            _DateTemplate[DayOfWeek.Monday] = true;
            _DateTemplate[DayOfWeek.Tuesday] = true;
            _DateTemplate[DayOfWeek.Wednesday] = true;
            _DateTemplate[DayOfWeek.Thursday] = true;
            _DateTemplate[DayOfWeek.Friday] = true;
            _DateTemplate[DayOfWeek.Saturday] = false;
            _DateTemplate[DayOfWeek.Sunday] = false;

            return new DayScheduleBuilder(_Template);
        }
    }

    public class IntermediateWeeklyScheduleBuilder : DayScheduleBuilder
    {
        private readonly WeeklyScheduleTemplate _DateTemplate;
        public IntermediateWeeklyScheduleBuilder(ScheduleTemplate template)
            : base(template)
        {
            _DateTemplate = (WeeklyScheduleTemplate)template.DateTemplate;
        }

        public IntermediateWeeklyScheduleBuilder AndOnMonday()
        {
            _DateTemplate[DayOfWeek.Monday] = true;

            return this;
        }

        public IntermediateWeeklyScheduleBuilder AndOnTuesday()
        {
            _DateTemplate[DayOfWeek.Tuesday] = true;

            return this;
        }

        public IntermediateWeeklyScheduleBuilder AndOnWednesday()
        {
            _DateTemplate[DayOfWeek.Wednesday] = true;

            return this;
        }

        public IntermediateWeeklyScheduleBuilder AndOnThursday()
        {
            _DateTemplate[DayOfWeek.Thursday] = true;

            return this;
        }

        public IntermediateWeeklyScheduleBuilder AndOnFriday()
        {
            _DateTemplate[DayOfWeek.Friday] = true;

            return this;
        }

        public IntermediateWeeklyScheduleBuilder AndOnSaturday()
        {
            _DateTemplate[DayOfWeek.Saturday] = true;

            return this;
        }

        public IntermediateWeeklyScheduleBuilder AndOnSunday()
        {
            _DateTemplate[DayOfWeek.Monday] = true;

            return this;
        }
    }
}
