using CifParser.Records;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CifParserTest
{
    public class ScheduleTest
    {
        private const string _permanent =
@"BSNC108651812191905171110100 PXX1S531220122180012 DMUV   125      B S T        P
";

        [Fact]
        public void ReadScheduleRecord()
        {
            var record = ParseRecord();
            Assert.NotNull(record);
        }

        private Schedule ParseRecord(string records = null)
        {
            records = records ?? _permanent;
            return ParserTest.ParseRecords(records)[0] as Schedule;
        }

        [Fact]
        public void TypePropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("BS", record.Type);
        }

        [Fact]
        public void InsertActionPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal(RecordAction.Insert, record.Action);
        }

        [Fact]
        public void DeleteActionPropertySet()
        {
            var delete = @"BSDN41191190112                                                                N
";
            var record = ParseRecord(delete);
            Assert.Equal(RecordAction.Delete, record.Action);
        }

        [Fact]
        public void ReviseActionPropertySet()
        {
            var delete = @"BSRN412321901121901260000010 1OO2D87    121800000 DMUA   075      S            N
";
            var record = ParseRecord(delete);
            Assert.Equal(RecordAction.Amend, record.Action);
        }


        [Fact]
        public void TimetableUidPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("C10865", record.TimetableUid);
        }

        [Fact]
        public void RunsFromPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal(new DateTime(2018, 12, 19, 0, 0, 0), record.RunsFrom);
        }

        [Fact]
        public void RunsToPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal(new DateTime(2019, 5, 17, 0, 0, 0), record.RunsTo);
        }

        [Fact]
        public void DayMaskPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("1110100", record.DayMask);
        }

        [Fact]
        public void RunsOnBankHoliday()
        {
            var record = ParseRecord();
            Assert.Equal("", record.BankHolidayRunning);
        }

        [Fact]
        public void DoesNotRunOnBankHoliday()
        {
            var noBankHoliday =
@"BSNC108651812191905171110100XPXX1S531220122180012 DMUV   125      B S T        P
";
            var record = ParseRecord(noBankHoliday);
            Assert.Equal("X", record.BankHolidayRunning);
        }

        [Fact]
        public void DoesNotRunOnScottishBankHoliday()
        {
            var noBankHoliday =
@"BSNC108651812191905171110100GPXX1S531220122180012 DMUV   125      B S T        P
";
            var record = ParseRecord(noBankHoliday);
            Assert.Equal("G", record.BankHolidayRunning);
        }

        [Fact]
        public void TrainIdPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("1S53", record.TrainIdentity);
        }

        [Theory]
        [InlineData(" ", ServiceClass.B)]
        [InlineData("B", ServiceClass.B)]
        [InlineData("S", ServiceClass.S)]
        public void SeatClassPropertySet(string value, ServiceClass expectedValue)
        {
            var classRecord = $"BSNC108651812191905171110100GPXX1S531220122180012 DMUV   125      {value} S T        P";
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
            var classRecord = $"BSNC108651812191905171110100GPXX1S531220122180012 DMUV   125      B{value}S T        P";
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
            var reservationRecord = $"BSNC108651812191905171110100GPXX1S531220122180012 DMUV   125      B {value} T        P";
            var record = ParseRecord(reservationRecord);
            Assert.Equal(expectedValue, record.ReservationIndicator);
        }

        [Fact]
        public void CateringPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("T", record.Catering);
        }

        [Theory]
        [InlineData("P", StpIndicator.P)]
        [InlineData("N", StpIndicator.N)]
        [InlineData("O", StpIndicator.O)]
        public void StpIndicatorropertySet(string value, StpIndicator expectedValue)
        {
            var classRecord = $"BSNC108651812191905171110100GPXX1S531220122180012 DMUV   125      B R T        {value}";
            var record = ParseRecord(classRecord);
            Assert.Equal(expectedValue, record.StpIndicator);
        }

        [Fact]
        public void CancelledStpIndicatorropertySet()
        {
            var cancelled = "BSNH317491901311902010001100            1                                      C";
            var record = ParseRecord(cancelled);
            Assert.Equal(StpIndicator.C, record.StpIndicator);
        }
    }
}
