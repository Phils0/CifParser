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
        private const string DataFile = @".\Data\ttisf193.msn";

        [Fact]
        public void ParseFile()
        {
            var factory = new TtisRecordEngineFactory(Substitute.For<ILogger>());
            var parser = factory.CreateStationParser();

            var records = parser.Read(DataFile).ToArray();

            Assert.NotEmpty(records);

            var waterloo = records.First(s => s.Tiploc == "WATRLMN");
            Assert.Equal(InterchangeStatus.Main, waterloo.InterchangeStatus);
        }
    }
}
