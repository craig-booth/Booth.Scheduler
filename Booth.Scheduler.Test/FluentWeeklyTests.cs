using System;
using System.Linq;

using Xunit;
using FluentAssertions;
using FluentAssertions.Execution;

using Booth.Common;
using Booth.Scheduler.Fluent;

namespace Booth.Scheduler.Test
{
    public class FluentWeeklyTests
    {

        [Fact]
        public void EveryWeekOnMondayAt14_00()
        {
            var schedule = Schedule.EveryWeek().OnMonday().At(14, 00);

            using (new AssertionScope())
            {
                schedule.DateTemplate.Should().BeOfType<WeeklyScheduleTemplate>().And.BeEquivalentTo(new { Every = 1 });

                var weeklySchedule = schedule.DateTemplate as WeeklyScheduleTemplate;
                weeklySchedule[DayOfWeek.Sunday].Should().BeFalse();
                weeklySchedule[DayOfWeek.Monday].Should().BeTrue();
                weeklySchedule[DayOfWeek.Tuesday].Should().BeFalse();
                weeklySchedule[DayOfWeek.Wednesday].Should().BeFalse();
                weeklySchedule[DayOfWeek.Thursday].Should().BeFalse();
                weeklySchedule[DayOfWeek.Friday].Should().BeFalse();
                weeklySchedule[DayOfWeek.Saturday].Should().BeFalse();

                schedule.TimeTemplate.Should().BeOfType<ExactTimeScheduleTemplate>().And.BeEquivalentTo(new { Time = new Time(14, 00, 00) });
                schedule.ToString().Should().Be("Run every week on Monday, at 14:00");
            }
        }

        [Fact]
        public void EveryWeekOnWeekdaysAt14_00()
        {
            var schedule = Schedule.EveryWeek().OnWeekdays().At(14, 00);

            using (new AssertionScope())
            {
                schedule.DateTemplate.Should().BeOfType<WeeklyScheduleTemplate>().And.BeEquivalentTo(new { Every = 1 });

                var weeklySchedule = schedule.DateTemplate as WeeklyScheduleTemplate;
                weeklySchedule[DayOfWeek.Sunday].Should().BeFalse();
                weeklySchedule[DayOfWeek.Monday].Should().BeTrue();
                weeklySchedule[DayOfWeek.Tuesday].Should().BeTrue();
                weeklySchedule[DayOfWeek.Wednesday].Should().BeTrue();
                weeklySchedule[DayOfWeek.Thursday].Should().BeTrue();
                weeklySchedule[DayOfWeek.Friday].Should().BeTrue();
                weeklySchedule[DayOfWeek.Saturday].Should().BeFalse();

                schedule.TimeTemplate.Should().BeOfType<ExactTimeScheduleTemplate>().And.BeEquivalentTo(new { Time = new Time(14, 00, 00) });
                schedule.ToString().Should().Be("Run every week on Monday, Tuesday, Wednesday, Thursday and Friday, at 14:00");
            }
        }

        [Fact]
        public void EveryWeekOnWeekdayEvery2HoursFrom9to5()
        {
            var schedule = Schedule.EveryWeek().OnWeekdays().EveryHours(2).From(9, 00).Until(17, 00);

            using (new AssertionScope())
            {
                schedule.DateTemplate.Should().BeOfType<WeeklyScheduleTemplate>().And.BeEquivalentTo(new { Every = 1 });

                var weeklySchedule = schedule.DateTemplate as WeeklyScheduleTemplate;
                weeklySchedule[DayOfWeek.Sunday].Should().BeFalse();
                weeklySchedule[DayOfWeek.Monday].Should().BeTrue();
                weeklySchedule[DayOfWeek.Tuesday].Should().BeTrue();
                weeklySchedule[DayOfWeek.Wednesday].Should().BeTrue();
                weeklySchedule[DayOfWeek.Thursday].Should().BeTrue();
                weeklySchedule[DayOfWeek.Friday].Should().BeTrue();
                weeklySchedule[DayOfWeek.Saturday].Should().BeFalse();

                schedule.TimeTemplate.Should().BeOfType<HourlyScheduleTemplate>().And.BeEquivalentTo(new { Every = 2, FromTime = new Time(09, 00, 00), ToTime = new Time(17, 00, 00) });
                schedule.ToString().Should().Be("Run every week on Monday, Tuesday, Wednesday, Thursday and Friday, every 2 hours between 9:00 and 17:00");
            }
        }


        [Fact]
        public void Every2WeeksOnTuesdayWednesdayAndThursdayAt7()
        {
            var schedule = Schedule.EveryWeeks(2).OnTuesday().AndOnWednesday().AndOnThursday().At(7, 00);

            using (new AssertionScope())
            {
                schedule.DateTemplate.Should().BeOfType<WeeklyScheduleTemplate>().And.BeEquivalentTo(new { Every = 2 });

                var weeklySchedule = schedule.DateTemplate as WeeklyScheduleTemplate;
                weeklySchedule[DayOfWeek.Sunday].Should().BeFalse();
                weeklySchedule[DayOfWeek.Monday].Should().BeFalse();
                weeklySchedule[DayOfWeek.Tuesday].Should().BeTrue();
                weeklySchedule[DayOfWeek.Wednesday].Should().BeTrue();
                weeklySchedule[DayOfWeek.Thursday].Should().BeTrue();
                weeklySchedule[DayOfWeek.Friday].Should().BeFalse();
                weeklySchedule[DayOfWeek.Saturday].Should().BeFalse();

                schedule.TimeTemplate.Should().BeOfType<ExactTimeScheduleTemplate>().And.BeEquivalentTo(new { Time = new Time(07, 00, 00) });
                schedule.ToString().Should().Be("Run every 2 weeks on Tuesday, Wednesday and Thursday, at 7:00");
            }
        }

    }
}
