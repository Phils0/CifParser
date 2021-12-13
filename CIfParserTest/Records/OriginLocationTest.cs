using System;
using CifParser.Records;
using Xunit;

namespace CifParserTest.Records
{
    public class OriginLocationTest
    {
        private const string _origin = @"LONRCH    1554 15542A C      TB                                                 ";
        
        [Fact]
        public void ReadLocationRecord()
        {
            var record = ParseRecord();
            Assert.NotNull(record);
        }

        private OriginLocation ParseRecord(string? record = null)
        {
            return (OriginLocation) ParserTest.ParseRecords(record ?? _origin)[0];
        }

        [Fact]
        public void TypePropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("LO", record.Type);
        }

        [Fact]
        public void LocationPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("NRCH", record.Location);
        }

        [Fact]
        public void SequencePropertySetToOneWhenNone()
        {
            var record = ParseRecord();
            Assert.Equal(1, record.Sequence);
        }

        [Fact]
        public void WorkingDeparturePropertySet()
        {
            var record = ParseRecord();
            Assert.Equal(new TimeSpan(15, 54, 0), record.WorkingDeparture);
        }

        [Fact]
        public void PublicDeparturePropertySet()
        {
            var record = ParseRecord();
            Assert.Equal(new TimeSpan(15, 54, 0), record.PublicDeparture);
        }

        [Fact]
        public void PlatformPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("2A", record.Platform);
        }

        [Fact]
        public void ActivitiesPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("TB", record.Activities);
        }
        
        [Theory]
        [InlineData(@"LONRCH    1554 15542A C      TB                                                 ", "NRCH 15:54:00 Stop")]
        [InlineData(@"LONRCH   21554 15542A C      TB                                                 ", "NRCH-2 15:54:00 Stop")]
        [InlineData(@"LONRCH    1554H00002A C      TB                                                 ", "NRCH 15:54:30 ")]
        public void CustomToString(string record, string expected)
        {
            var location = ParseRecord(record);
            Assert.Equal(expected, location.ToString());
        }
    }
}
