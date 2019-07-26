using CifParser;
using CifParser.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CifParser.RdgRecords;
using NSubstitute;
using Serilog;
using Xunit;

namespace CifParserTest
{
    public class ReadStationFileTest
    {
        private const string TtisStationFile = @".\Data\ttisf193.msn";
        private const string DtdStationFile = @".\Data\RJTTF293.msn";

        [Fact]
        public void ParseTtisFile()
        {
            var factory = new StationParserFactory(Substitute.For<ILogger>());
            var parser = factory.CreateParser(StationParserFactory.TtisIgnoreLines);

            var records = parser.Read(TtisStationFile).Cast<Station>().ToArray();

            Assert.NotEmpty(records);

            var waterloo = records.First(s => s.Tiploc == "WATRLMN");
            Assert.Equal(InterchangeStatus.Main, waterloo.InterchangeStatus);
        }
        
        [Fact]
        public void ParseDtdFile()
        {
            var factory = new StationParserFactory(Substitute.For<ILogger>());
            var parser = factory.CreateParser(StationParserFactory.DtdIgnoreLines);

            var records = parser.Read(DtdStationFile).Cast<Station>().ToArray();

            Assert.NotEmpty(records);

            var waterloo = records.First(s => s.Tiploc == "WATRLMN");
            Assert.Equal(InterchangeStatus.Main, waterloo.InterchangeStatus);
        }
    }
}
