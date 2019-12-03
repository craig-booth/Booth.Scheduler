﻿using System;
using System.Linq;

using Booth.Common;

using NUnit.Framework;


namespace Booth.Scheduler.Test
{
    class RecurringTimeScheduleTemplateTests
    {
        [TestCase]
        public void HourlyWithNoStartEndTime()
        {
            var template = new HourlyScheduleTemplate(1);

            var actual = template.GetTimes();

            var expected = new Time[]
            {
                new Time(00, 00, 00),
                new Time(01, 00, 00),
                new Time(02, 00, 00),
                new Time(03, 00, 00),
                new Time(04, 00, 00),
                new Time(05, 00, 00),
                new Time(06, 00, 00),
                new Time(07, 00, 00),
                new Time(08, 00, 00),
                new Time(09, 00, 00),
                new Time(10, 00, 00),
                new Time(11, 00, 00),
                new Time(12, 00, 00),
                new Time(13, 00, 00),
                new Time(14, 00, 00),
                new Time(15, 00, 00),
                new Time(16, 00, 00),
                new Time(17, 00, 00),
                new Time(18, 00, 00),
                new Time(19, 00, 00),
                new Time(20, 00, 00),
                new Time(21, 00, 00),
                new Time(22, 00, 00),
                new Time(23, 00, 00)
            };
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void HourlyWithExactStartAndEndTimes()
        {
            var template = new HourlyScheduleTemplate();
            template.From(9, 00);
            template.To(17, 00);

            var actual = template.GetTimes();

            var expected = new Time[]
            {
                new Time(09, 00, 00),
                new Time(10, 00, 00),
                new Time(11, 00, 00),
                new Time(12, 00, 00),
                new Time(13, 00, 00),
                new Time(14, 00, 00),
                new Time(15, 00, 00),
                new Time(16, 00, 00),
                new Time(17, 00, 00)
            };

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void TwiceHourlyWithDifferentStartAndEndTimes()
        {
            var template = new HourlyScheduleTemplate(2);
            template.From(9, 05);
            template.To(17, 30);

            var actual = template.GetTimes();

            var expected = new Time[]
            {
                new Time(09, 05, 00),
                new Time(11, 05, 00),
                new Time(13, 05, 00),
                new Time(15, 05, 00),
                new Time(17, 05, 00)
            };
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void Every15Hours()
        {
            var template = new HourlyScheduleTemplate(15);

            var actual = template.GetTimes();

            var expected = new Time[]
            {
                new Time(00, 00, 00),
                new Time(15, 00, 00)
            };
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void RecurranceGreaterThanOneDay()
        {
            var template = new HourlyScheduleTemplate(25);
            template.From(11, 00);
            template.To(14, 00);

            var actual = template.GetTimes();

            var expected = new Time[]
            {
                new Time(11, 00, 00)
            };
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void Every150MinutesWithNoStartAndEndTime()
        {
            var template = new MinuteScheduleTemplate(150);

            var actual = template.GetTimes();

            var expected = new Time[]
            {
                new Time(00, 00, 00),
                new Time(02, 30, 00),
                new Time(05, 00, 00),
                new Time(07, 30, 00),
                new Time(10, 00, 00),
                new Time(12, 30, 00),
                new Time(15, 00, 00),
                new Time(17, 30, 00),
                new Time(20, 00, 00),
                new Time(22, 30, 00),
            };
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void Every15MinutesBetween10And12()
        {
            var template = new MinuteScheduleTemplate(15);
            template.From(10, 00);
            template.To(12, 00);

            var actual = template.GetTimes();

            var expected = new Time[]
            {
                new Time(10, 00, 00),
                new Time(10, 15, 00),
                new Time(10, 30, 00),
                new Time(10, 45, 00),
                new Time(11, 00, 00),
                new Time(11, 15, 00),
                new Time(11, 30, 00),
                new Time(11, 45, 00),
                new Time(12, 00, 00)
            };
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void ValidateEveryMinuteNotZero()
        {
            var template = new MinuteScheduleTemplate(0);

            var errors = template.Validate().ToList();
            Assert.That(errors, Has.Count.EqualTo(1));
            Assert.That(errors[0], Is.EqualTo("Time must be less than 1 day"));
        }

        [TestCase]
        public void ValidateEveryMinuteNotLessThanZero()
        {
            var template = new MinuteScheduleTemplate(-1);

            var errors = template.Validate().ToList();
            Assert.That(errors, Has.Count.EqualTo(1));
            Assert.That(errors[0], Is.EqualTo("Time must be less than 1 day"));
        }

        [TestCase]
        public void ValidateEveryHourNotZero()
        {
            var template = new HourlyScheduleTemplate(0);

            var errors = template.Validate().ToList();
            Assert.That(errors, Has.Count.EqualTo(1));
            Assert.That(errors[0], Is.EqualTo("Time must be less than 1 day"));
        }

        [TestCase]
        public void ValidateEveryHourNotLessThanZero()
        {
            var template = new HourlyScheduleTemplate(-1);

            var errors = template.Validate().ToList();
            Assert.That(errors, Has.Count.EqualTo(1));
            Assert.That(errors[0], Is.EqualTo("Time must be less than 1 day"));
        }

        [TestCase]
        public void ValidateFromTimeNotEqualToTime()
        {
            var template = new HourlyScheduleTemplate(1);
            template.From(9, 00);
            template.To(9, 00);

            var errors = template.Validate().ToList();
            Assert.That(errors, Has.Count.EqualTo(1));
            Assert.That(errors[0], Is.EqualTo("From time must be earlier than To Time"));
        }

        [TestCase]
        public void ValidateFromTimeLessThanToTime()
        {
            var template = new HourlyScheduleTemplate(1);
            template.From(17, 00);
            template.To(9, 00);

            var errors = template.Validate().ToList();
            Assert.That(errors, Has.Count.EqualTo(1));
            Assert.That(errors[0], Is.EqualTo("From time must be earlier than To Time"));
        }
    }
}
