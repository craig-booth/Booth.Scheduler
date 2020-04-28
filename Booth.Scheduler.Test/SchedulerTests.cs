using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;
using FluentAssertions;
using FluentAssertions.Execution;

using Booth.Scheduler.Fluent;

namespace Booth.Scheduler.Test
{
    public class SchedulerTests
    {

        [Fact]
        public void AddJobsTest()
        {
            var scheduler = new Scheduler();
            scheduler.Start();

            scheduler.AddJob("Test", () => { }, Schedule.EveryDay().At(9, 00), DateTime.Today);
            scheduler.AddJob("Test2", () => { }, Schedule.EveryMonth().OnFirstDay().At(9, 00), DateTime.Today);

            DateTime next1;
            if (DateTime.Now.Hour >= 9)
                next1 = DateTime.Today.AddDays(1).AddHours(9);
            else
                next1 = DateTime.Today.AddHours(9);

            DateTime next2;
            if (DateTime.Today.Day == 1)
                next2 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 9, 0, 0);
            else if (DateTime.Today.Month == 12)
                next2 = new DateTime(DateTime.Today.Year + 1, 1, 1, 9, 0, 0);
            else
                next2 = new DateTime(DateTime.Today.Year, DateTime.Today.Month + 1, 1, 9, 0, 0);

            scheduler.Jobs.Should().BeEquivalentTo(new object[]
            {
                new { Name = "Test", Description = "Run every day, at 9:00", Status = JobStatus.Active, NextRunTime = next1 },
                new { Name = "Test2", Description = "Run on the first day of every month, at 9:00", Status = JobStatus.Active, NextRunTime = next2 },
            });
        }

        [Fact]
        public void AddJobWithTheSameNameTest()
        {
            var scheduler = new Scheduler();

            scheduler.AddJob("Test", () => { }, Schedule.EveryDay().At(9, 00), DateTime.Today);

            Action a = () => scheduler.AddJob("Test", () => { }, Schedule.EveryDay().At(9, 00), DateTime.Today);

            a.Should().ThrowExactly<ArgumentException>();  
        }

        [Fact]
        public void StartTest()
        {
            var scheduler = new Scheduler();

            scheduler.Start();

            scheduler.Running.Should().BeTrue();
        }

        [Fact]
        public void StopTest()
        {
            var scheduler = new Scheduler();

            scheduler.Start();
            scheduler.Stop();

            scheduler.Running.Should().BeFalse();
        }


    }
}
