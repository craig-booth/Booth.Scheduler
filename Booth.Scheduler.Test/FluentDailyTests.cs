using System;
using System.Linq;

using Xunit;
using FluentAssertions;
using FluentAssertions.Execution;

using Booth.Scheduler.Fluent;

namespace Booth.Scheduler.Test
{
    public class FluentDailyTests
    {
        [Fact]
        public void DailyAt14_30()
        {
            var schedule = Schedule.EveryDay().At(14, 30);

            using (new AssertionScope())
            {
                schedule.DateTemplate.Should().BeOfType<DailyScheduleTemplate>().And.BeEquivalentTo(new { Every = 1 });
                schedule.TimeTemplate.Should().BeOfType<ExactTimeScheduleTemplate>().And.BeEquivalentTo(new { Time = new TimeSpan(14, 30, 00) });
                schedule.ToString().Should().Be("Run every day, at 14:30");
            }
        }

        [Fact]
        public void EveryFifthDayAt09_00()
        {
            var schedule = Schedule.EveryDays(5).At(09, 00);

            using (new AssertionScope())
            {
                schedule.DateTemplate.Should().BeOfType<DailyScheduleTemplate>().And.BeEquivalentTo(new { Every = 5 });
                schedule.TimeTemplate.Should().BeOfType<ExactTimeScheduleTemplate>().And.BeEquivalentTo(new { Time = new TimeSpan(09, 00, 00) });
                schedule.ToString().Should().Be("Run every 5 days, at 9:00");
            }
        }


        [Fact]
        public void EveryDayEvery2HoursFrom9To5()
        {
            var schedule = Schedule.EveryDay().EveryHours(2).From(9, 00).Until(17, 00);

            using (new AssertionScope())
            {
                schedule.DateTemplate.Should().BeOfType<DailyScheduleTemplate>().And.BeEquivalentTo(new { Every = 1 });
                schedule.TimeTemplate.Should().BeOfType<HourlyScheduleTemplate>().And.BeEquivalentTo(new { Every = 2, FromTime = new TimeSpan(09, 00, 00), ToTime = new TimeSpan(17, 00, 00) });
                schedule.ToString().Should().Be("Run every day, every 2 hours between 9:00 and 17:00");
            }
        }

        [Fact]
        public void Every2DaysEveryMinuteFrom9To5()
        {
            var schedule = Schedule.EveryDays(2).EveryMinute().From(9, 00).Until(17, 00);

            using (new AssertionScope())
            {
                schedule.DateTemplate.Should().BeOfType<DailyScheduleTemplate>().And.BeEquivalentTo(new { Every = 2 });
                schedule.TimeTemplate.Should().BeOfType<MinuteScheduleTemplate>().And.BeEquivalentTo(new { Every = 1, FromTime = new TimeSpan(09, 00, 00), ToTime = new TimeSpan(17, 00, 00) });
                schedule.ToString().Should().Be("Run every 2 days, every minute between 9:00 and 17:00");
            }
        }

        [Fact]
        public void EveryDayEvery30MinutesFrom9To5()
        {
            var schedule = Schedule.EveryDay().EveryMinutes(30).From(9, 00).Until(17, 00);

            using (new AssertionScope())
            {
                schedule.DateTemplate.Should().BeOfType<DailyScheduleTemplate>().And.BeEquivalentTo(new { Every = 1 });
                schedule.TimeTemplate.Should().BeOfType<MinuteScheduleTemplate>().And.BeEquivalentTo(new { Every = 30, FromTime = new TimeSpan(09, 00, 00), ToTime = new TimeSpan(17, 00, 00) });
                schedule.ToString().Should().Be("Run every day, every 30 minutes between 9:00 and 17:00");
            }
        }
    }
}
