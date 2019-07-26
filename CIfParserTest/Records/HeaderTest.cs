using System;
using CifParser.Records;
using Xunit;

namespace CifParserTest.Records
{
    public class HeaderTest
    {
        private const string _records =
@"HDTPS.UDFROC1.PD1901292901191927DFROC1MDFROC1LUA290119290120                    
";

        [Fact]
        public void ReadHeaderRecord()
        {
            var record = ParseRecord();
            Assert.NotNull(record);
        }

        private Header ParseRecord()
        {
            return ParserTest.ParseRecords(_records)[0] as Header;
        }

        [Fact]
        public void TypePropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("HD", record.Type);
        }

        [Fact]
        public void MainframePropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("TPS.UDFROC1.PD190129", record.MainframeId);
        }

        [Fact]
        public void ExtractedAtPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal(new DateTime(2019, 1, 29, 19, 27, 0), record.ExtractedAt);
        }

        [Fact]
        public void CurrentFileReferencePropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("DFROC1M", record.CurrentFileReference);
        }

        [Fact]
        public void LastFileReferencePropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("DFROC1L", record.LastFileReference);
        }

        [Fact]
        public void ExtractTypePropertySet()
        {
            var record = ParseRecord();
            Assert.Equal(ExtractType.U, record.ExtractType);
        }

        [Fact]
        public void StartDatePropertySet()
        {
            var record = ParseRecord();
            Assert.Equal(new DateTime(2019, 1, 29, 0, 0, 0), record.StartDate);
        }

        [Fact]
        public void EndDatePropertySet()
        {
            var record = ParseRecord();
            Assert.Equal(new DateTime(2020, 1, 29, 0, 0, 0), record.EndDate);
        }
    }
}
