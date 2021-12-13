using CifParser.Records;
using System;
using System.Collections.Generic;
using Xunit;

namespace CifParserTest
{
    public class WorkingTimeConverterTest
    {

        public static IEnumerable<object[]> Times
        {
            get
            {
                yield return new object[] { "0800 ", new TimeSpan(8, 0, 0) };
                yield return new object[] { "2359 ", new TimeSpan(23, 59, 0) };
                yield return new object[] { "0001 ", new TimeSpan(0, 1, 0) };
                yield return new object[] { "0901H", new TimeSpan(9, 1, 30) };
            }
        }

        [Theory]
        [MemberData(nameof(Times))]
        public void ParseScheduleTime(string input, TimeSpan expected)
        {
            var converter = new WorkingTimeConverter();

            var time = converter.StringToField(input) as TimeSpan?;
            Assert.Equal(expected, time);
        }

        [Fact]
        public void ParseNoScheduledTime()
        {
            var converter = new WorkingTimeConverter();

            var time = converter.StringToField("     ") as TimeSpan?;
            Assert.Null(time);
        }
    }
}
