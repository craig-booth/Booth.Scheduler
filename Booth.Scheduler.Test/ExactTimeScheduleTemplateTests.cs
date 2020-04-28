using System;
using System.Linq;

using Xunit;
using FluentAssertions;

using Booth.Common;

namespace Booth.Scheduler.Test
{
    public class ExactTimeScheduleTemplateTests
    {

        [Fact]
        public void OnlyRunTime()
        {
            var template = new ExactTimeScheduleTemplate(13, 32);

            var actual = template.GetTimes();

            actual.Should().Equal(new Time[] { new Time(13, 32, 00) });
        }

    }
}
