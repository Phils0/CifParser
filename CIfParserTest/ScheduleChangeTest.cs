using CifParser.Records;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CifParserTest
{
    public class ScheduleChangeTest
    {
        private const string _change =
@"CROXFD    OO2V11    125506005 DMUT   090D     S                                 
";

        [Fact]
        public void ReadScheduleChangeRecord()
        {
            var record = ParseRecord();
            Assert.NotNull(record);
        }

        private ScheduleChange ParseRecord(string records = null)
        {
            records = records ?? _change;
            return ParserTest.ParseRecords(records)[0] as ScheduleChange;
        }

        [Fact]
        public void TypePropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("CR", record.Type);
        }

        [Fact]
        public void LocationPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("OXFD", record.Location);
        }

        [Fact]
        public void SequencePropertySetToOneWhenNone()
        {
            var record = ParseRecord();
            Assert.Equal(1, record.Sequence);
        }

        [Fact]
        public void TrainIdPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("2V11", record.TrainIdentity);
        }

        [Theory]
        [InlineData(" ", ServiceClass.B)]
        [InlineData("B", ServiceClass.B)]
        [InlineData("S", ServiceClass.S)]
        public void SeatClassPropertySet(string value, ServiceClass expectedValue)
        {
            var classRecord = $"CROXFD    OO2V11    125506005 DMUT   090D     {value}                                 ";
            var record = ParseRecord(classRecord);
            Assert.Equal(expectedValue, record.SeatClass);
        }

        [Theory]
        [InlineData(" ", ServiceClass.None)]
        [InlineData("B", ServiceClass.B)]
        [InlineData("S", ServiceClass.S)]
        [InlineData("F", ServiceClass.F)]
        public void SleeperClassPropertySet(string value, ServiceClass expectedValue)
        {
            var classRecord = $"CROXFD    OO2V11    125506005 DMUT   090D     B{value}                                ";
            var record = ParseRecord(classRecord);
            Assert.Equal(expectedValue, record.SleeperClass);
        }

        [Theory]
        [InlineData(" ", ReservationIndicator.None)]
        [InlineData("A", ReservationIndicator.A)]
        [InlineData("R", ReservationIndicator.R)]
        [InlineData("S", ReservationIndicator.S)]
        [InlineData("E", ReservationIndicator.E)]
        public void ReservationIndicatorropertySet(string value, ReservationIndicator expectedValue)
        {
            var reservationRecord = $"CROXFD    OO2V11    125506005 DMUT   090D     B {value}                               ";
            var record = ParseRecord(reservationRecord);
            Assert.Equal(expectedValue, record.ReservationIndicator);
        }

        [Fact]
        public void CateringPropertySet()
        {
            var cateringRecord = $"CRCHST    XX1D314004122340000 DMUE   100      S S T                             ";
            var record = ParseRecord(cateringRecord);
            Assert.Equal("T", record.Catering);
        }
    }
}
