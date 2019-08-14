using CifParser;
using CifParser.Records;
using System;
using System.Collections.Generic;
using System.IO;
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
        private static readonly string TtisStationFile = Path.Combine(".", "Data", "ttisf193.msn");
        private static readonly string DtdStationFile = Path.Combine(".", "Data", "RJTTF293.MSN");

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
