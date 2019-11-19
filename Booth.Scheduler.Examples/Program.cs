using System;

using Booth.Scheduler;
using Booth.Scheduler.Fluent;

namespace Booth.Scheduler.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            var schedule = Schedule.EveryDay().EveryMinute().From(0, 00).Until(23, 59);

            var scheduler = new Scheduler();
            scheduler.AddJob("Test", ScheduledJob, schedule, DateTime.Today);

            scheduler.Start();

            Console.ReadKey();

            scheduler.Stop();
        }

        static void ScheduledJob()
        {
            Console.WriteLine("Hello World!");
        }
    }

}

