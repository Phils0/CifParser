using CifParser;
using CifParser.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSubstitute;
using Serilog;
using Xunit;

namespace CifParserTest
{
    public class ReadFileTest
    {
        private const string DataFile = @".\Data\toc-update-mon.CIF";

        [Fact]
        public void ParseFile()
        {
            var factory = new CifParserFactory(Substitute.For<ILogger>());
            var parser = factory.CreateParser();

            var records = parser.Read(DataFile).ToArray();

            Assert.IsType<Header>(records[0]);
            Assert.IsType<Trailer>(records[records.Length - 1]);
        }

        [Fact]
        public void ParseAndConsolidateFile()
        {
            var logger = Substitute.For<ILogger>();
            var factory = new ConsolidatorFactory(Substitute.For<ILogger>());
            var parser = factory.CreateParser();

            var records = parser.Read(DataFile).ToArray();

            Assert.IsType<Header>(records[0]);
            Assert.IsType<Trailer>(records[records.Length - 1]);
        }

        [Fact]
        public void ConsolidateSchedule()
        {
            var logger = Substitute.For<ILogger>();
            var factory = new ConsolidatorFactory(Substitute.For<ILogger>());
            var parser = factory.CreateParser();

            var schedule = parser.Read(DataFile).OfType<Schedule>().First();

            Assert.NotEmpty(schedule.Records);
        }
    }
}
