using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using Booth.Scheduler.Fluent;

namespace Booth.Scheduler.Test
{
    class SchedulerTests
    {

        [TestCase]
        public void AddJobsTest()
        {
            var scheduler = new Scheduler();
            scheduler.Start();

            scheduler.AddJob("Test", () => { }, Schedule.EveryDay().At(9, 00), DateTime.Today);
            scheduler.AddJob("Test2", () => { }, Schedule.EveryMonth().OnFirstDay().At(9, 00), DateTime.Today);

            var jobs = scheduler.Jobs.OrderBy(x => x.Name).ToList();

            DateTime next1;
            if (DateTime.Now.Hour >= 9)
                next1 = DateTime.Today.AddDays(1).AddHours(9);
            else
                next1 = DateTime.Today.AddHours(9);

            DateTime next2;
            if (DateTime.Today.Day == 1)
                next2 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1, 9, 0, 0);
            else
                next2 = new DateTime(DateTime.Today.Year, DateTime.Today.Month + 1, 1, 9, 0, 0);

            Assert.Multiple(() =>
            {
                Assert.That(jobs, Has.Count.EqualTo(2));

                Assert.That(jobs[0].Name, Is.EqualTo("Test"));
                Assert.That(jobs[0].Description, Is.EqualTo("Run every day, at 9:00"));
                Assert.That(jobs[0].Status, Is.EqualTo(JobStatus.Active));
                Assert.That(jobs[0].NextRunTime, Is.EqualTo(next1));

                Assert.That(jobs[1].Name, Is.EqualTo("Test2"));
                Assert.That(jobs[1].Description, Is.EqualTo("Run on the first day of every month, at 9:00"));
                Assert.That(jobs[1].Status, Is.EqualTo(JobStatus.Active));
                Assert.That(jobs[1].NextRunTime, Is.EqualTo(next2));
            });
        }

        [TestCase]
        public void AddJobWithTheSameNameTest()
        {
            var scheduler = new Scheduler();

            scheduler.AddJob("Test", () => { }, Schedule.EveryDay().At(9, 00), DateTime.Today);

            Assert.That(() => scheduler.AddJob("Test", () => { }, Schedule.EveryDay().At(9, 00), DateTime.Today), Throws.ArgumentException);     
        }

        [TestCase]
        public void StartTest()
        {
            var scheduler = new Scheduler();

            scheduler.Start();

            Assert.That(scheduler.Running, Is.True);
        }

        [TestCase]
        public void StopTest()
        {
            var scheduler = new Scheduler();

            scheduler.Start();
            scheduler.Stop();

            Assert.That(scheduler.Running, Is.False);
        }


    }
}
