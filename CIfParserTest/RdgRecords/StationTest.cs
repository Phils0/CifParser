using System;
using System.IO;
using System.Linq;
using CifParser;
using CifParser.RdgRecords;
using CifParser.Records;
using NSubstitute;
using Serilog;
using Xunit;

namespace CifParserTest.RdgRecords
{
    public class StationTest
    {
        private const string _station =
            @"A    TAMWORTH HL                   9TMWTHHLTAH   TAM14213 63045 5                 ";

        [Fact]
        public void ReadStationRecord()
        {
            var record = ParseRecord();
            Assert.NotNull(record);
        }

        private Station ParseRecord(string record = null)
        {
            var input = new StringReader(record ?? _station);

            var factory = new StationParserFactory(Substitute.For<ILogger>());
            var parser = factory.CreateStationParser(0);

            var records = parser.Read(input).ToArray();
           
            return records[0] as Station;
        }

        [Fact]
        public void RecordTypeSet()
        {
            var record = ParseRecord();
            Assert.Equal("A", record.RecordType);
        }
        
        [Fact]
        public void NamePropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("TAMWORTH HL", record.Name);
        }
        
        [Fact]
        public void InterchangeStatusPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal(InterchangeStatus.SubsidiaryLocation, record.InterchangeStatus);
        }
        
        [Fact]
        public void TiplocPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("TMWTHHL", record.Tiploc);
        }

        [Fact]
        public void SubsidiaryThreeLetterCodePropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("TAH", record.SubsidiaryThreeLetterCode);
        }
        
        [Fact]
        public void ThreeLetterCodePropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("TAM", record.ThreeLetterCode);
        }
        
        [Fact]
        public void EastPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal(14213, record.East);
        }
        
        [Fact]
        public void NorthPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal(63045, record.North);
        }
        
        [Fact]
        public void PositionIsEstimatedPropertySet()
        {
            var record = ParseRecord();
            Assert.False(record.PositionIsEstimated);
        }

        [Fact]
        public void MinimumChangeTimePropertySet()
        {
            var record = ParseRecord();
            Assert.Equal(5, record.MinimumChangeTime);
        }
        
        [Theory]
        [InlineData(@"A    TAMWORTH                      2TMWTHLLTAM   TAM14213 63044 5                 ", "TAM-TMWTHLL TAMWORTH")]
        [InlineData(@"A    TAMWORTH HL                   9TMWTHHLTAH   TAM14213 63045 5                 ", "TAM(TAH)-TMWTHHL TAMWORTH HL")]
        public void CustomToString(string record, string expected)
        {
            var location = ParseRecord(record);
            Assert.Equal(expected, location.ToString());
        }
    }
}
