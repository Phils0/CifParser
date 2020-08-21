using CifParser.Records;
using System;
using System.Collections.Generic;
using Xunit;

namespace CifParserTest
{
    public class PublicTimeConverterTest
    {

        public static IEnumerable<object[]> Times
        {
            get
            {
                yield return new object[] { "0800", new TimeSpan(8, 0, 0) };
                yield return new object[] { "2359", new TimeSpan(23, 59, 0) };
                yield return new object[] { "0001", new TimeSpan(0, 1, 0) };
            }
        }

        [Theory]
        [MemberData(nameof(Times))]
        public void ParseScheduleTime(string input, TimeSpan expected)
        {
            var converter = new PublicTimeConverter();

            var time = converter.StringToField(input) as TimeSpan?;
            Assert.Equal(expected, time.Value);
        }

        [Fact]
        public void ParseNoScheduledTime()
        {
            var converter = new PublicTimeConverter();

            var time = converter.StringToField("0000") as TimeSpan?;
            Assert.Null(time);
        }
    }
}
