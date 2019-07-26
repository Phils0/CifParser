using System;
using CifParser.Records;
using Xunit;

namespace CifParserTest.Records
{
    public class IntermediateLocationTest
    {
        private const string _stop = @"LITOTNES  1548H1550      154915502        T                                     ";
        private const string _pass = @"LIUPHILLJ           1708 00000000                        H                      ";


        [Fact]
        public void ReadLocationRecord()
        {
            var record = ParseRecord();
            Assert.NotNull(record);
        }

        private IntermediateLocation ParseRecord(string record = null)
        {
            return ParserTest.ParseRecords(record ?? _stop)[0] as IntermediateLocation;
        }

        [Fact]
        public void TypePropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("LI", record.Type);
        }

        [Fact]
        public void LocationPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("TOTNES", record.Location);
        }

        [Fact]
        public void SequencePropertySetToOneWhenNone()
        {
            var record = ParseRecord();
            Assert.Equal(1, record.Sequence);
        }

        [Fact]
        public void SequencePropertySet()
        {
            var record = ParseRecord("LIDORESNJ2          1945H00000000                                               ");
            Assert.Equal(2, record.Sequence);
        }

        [Fact]
        public void WorkingArrivalPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal(new TimeSpan(15, 48, 30), record.WorkingArrival);
        }

        [Fact]
        public void WorkingDeparturePropertySet()
        {
            var record = ParseRecord();
            Assert.Equal(new TimeSpan(15, 50, 0), record.WorkingDeparture);
        }

        [Fact]
        public void WorkingPassPropertyNotSet()
        {
            var record = ParseRecord();
            Assert.Null(record.WorkingPass);
        }

        [Fact]
        public void WorkingArrivalNotSetForPass()
        {
            var record = ParseRecord(_pass);
            Assert.Null(record.WorkingArrival);
        }

        [Fact]
        public void WorkingDepartureNotSetForPass()
        {
            var record = ParseRecord(_pass);
            Assert.Null(record.WorkingDeparture);
        }

        [Fact]
        public void WorkingPassPropertySet()
        {
            var record = ParseRecord(_pass);
            Assert.Equal(new TimeSpan(17, 8, 0), record.WorkingPass);
        }

        [Fact]
        public void PublicArrivalPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal(new TimeSpan(15, 49, 0), record.PublicArrival);
        }

        [Fact]
        public void PublicDeparturePropertySet()
        {
            var record = ParseRecord();
            Assert.Equal(new TimeSpan(15, 50, 0), record.PublicDeparture);
        }

        [Fact]
        public void PlatformPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("2", record.Platform);
        }

        [Fact]
        public void ActivitiesPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("T", record.Activities);
        }
        
        [Theory]
        [InlineData(@"LITOTNES  1548H1550      154915502        T                                     ", "TOTNES 15:49:00 Stop")]
        [InlineData(@"LITOTNES 21548H1550      154915502        T                                     ", "TOTNES-2 15:49:00 Stop")]
        [InlineData(@"LITOTNES  1548H1550      00000000        T                                     ", "TOTNES 15:48:30 ")]
        [InlineData(@"LIUPHILLJ           1708 00000000                        H                      ", "UPHILLJ 17:08:00 ")]
        public void CustomToString(string record, string expected)
        {
            var location = ParseRecord(record);
            Assert.Equal(expected, location.ToString());
        }
    }
}
