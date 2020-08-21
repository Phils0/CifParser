using CifParser;
using System;
using System.IO;
using System.Linq;
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
            var parser = factory.CreateParser(StationParserFactory.Ttis);

            var records = parser.Read(File.OpenText(TtisStationFile)).Cast<Station>().ToArray();

            Assert.NotEmpty(records);

            var waterloo = records.First(s => s.Tiploc == "WATRLMN");
            Assert.Equal(InterchangeStatus.Main, waterloo.InterchangeStatus);
        }
        
        [Fact]
        public void ParseDtdFile()
        {
            var factory = new StationParserFactory(Substitute.For<ILogger>());
            var parser = factory.CreateParser(StationParserFactory.Dtd);

            var records = parser.Read(File.OpenText(DtdStationFile)).Cast<Station>().ToArray();

            Assert.NotEmpty(records);

            var waterloo = records.First(s => s.Tiploc == "WATRLMN");
            Assert.Equal(InterchangeStatus.Main, waterloo.InterchangeStatus);
        }
    }
}
