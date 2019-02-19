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
            var parser = new Parser(Substitute.For<ILogger>());

            var records = parser.Read(DataFile).ToArray();

            Assert.IsType<Header>(records[0]);
            Assert.IsType<Trailer>(records[records.Length - 1]);
        }

        [Fact]
        public void ParseAndConsolidateFile()
        {
            var logger = Substitute.For<ILogger>();
            var parser = new ScheduleConsolidator(new Parser(logger), logger);

            var records = parser.Read(DataFile).ToArray();

            Assert.IsType<Header>(records[0]);
            Assert.IsType<Trailer>(records[records.Length - 1]);
        }


        [Fact]
        public void ConsolidateSchedule()
        {
            var logger = Substitute.For<ILogger>();
            var parser = new ScheduleConsolidator(new Parser(logger), logger);

            var schedule = parser.Read(DataFile).OfType<Schedule>().First();

            Assert.NotEmpty(schedule.Records);
        }
    }
}
