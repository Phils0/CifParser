using CifParser.Records;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CifParserTest
{
    public class AssociationTest
    {
        private const string _permanent =
@"AANP19165P183661812151905180000010NPSCRDFCEN2 TO                               P
";

        [Fact]
        public void ReadScheduleRecord()
        {
            var record = ParseRecord();
            Assert.NotNull(record);
        }

        private Association ParseRecord(string records = null)
        {
            records = records ?? _permanent;
            return ParserTest.ParseRecords(records)[0] as Association;
        }

        [Fact]
        public void TypePropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("AA", record.Type);
        }

        [Fact]
        public void InsertActionPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal(RecordAction.Create, record.Action);
        }

        [Fact]
        public void DeleteActionPropertySet()
        {
            var delete = @"AADP19165P18366181215                CRDFCEN  T                                P
";
            var record = ParseRecord(delete);
            Assert.Equal(RecordAction.Delete, record.Action);
        }

        [Fact]
        public void ReviseActionPropertySet()
        {
            var delete = @"AARC59262C589531901071903221111100NPSBNBR     TO                               P
";
            var record = ParseRecord(delete);
            Assert.Equal(RecordAction.Update, record.Action);
        }


        [Fact]
        public void BaseUidPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("P19165", record.MainUid);
        }

        [Fact]
        public void AssociationUidPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("P18366", record.AssociatedUid);
        }

        [Fact]
        public void RunsFromPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal(new DateTime(2018, 12, 15, 0, 0, 0), record.RunsFrom);
        }

        [Fact]
        public void RunsToPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal(new DateTime(2019, 5, 18, 0, 0, 0), record.RunsTo);
        }

        [Fact]
        public void DayMaskPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("0000010", record.DayMask);
        }

        [Fact]
        public void RunsOnBankHoliday()
        {
            var record = ParseRecord();
            Assert.Equal("NP", record.Category);
        }

        [Fact]
        public void TrainIdPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("S", record.DateIndicator);
        }

        [Fact]
        public void LocationPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("CRDFCEN", record.Location);
        }

        [Fact]
        public void BaseSequencePropertySet()
        {
            var record = ParseRecord();
            Assert.Equal(2, record.MainSequence);
        }

        [Fact]
        public void AssociationSequencePropertySet()
        {
            var record = ParseRecord();
            Assert.Equal(1, record.AssociationSequence);
        }

        [Theory]
        [InlineData("P", StpIndicator.P)]
        [InlineData("N", StpIndicator.N)]
        [InlineData("O", StpIndicator.O)]
        public void StpIndicatorropertySet(string value, StpIndicator expectedValue)
        {
            var stpRecord = $"AANG89899G898621902111903291111100JJSCRNLRCH  TP                               { value}";
            var record = ParseRecord(stpRecord);
            Assert.Equal(expectedValue, record.StpIndicator);
        }

        [Fact]
        public void CancelledStpIndicatorropertySet()
        {
            var cancelled = "AANL29223L290941902111902141111000   LIVST    T                                C";
            var record = ParseRecord(cancelled);
            Assert.Equal(StpIndicator.C, record.StpIndicator);
        }
    }
}
