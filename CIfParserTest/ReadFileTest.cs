using CifParser;
using CifParser.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var parser = new Parser();

            var records = parser.Read(DataFile).ToArray();

            Assert.IsType<Header>(records[0]);
            Assert.IsType<Trailer>(records[records.Length - 1]);
        }

        [Fact]
        public void ParseAndConsolidateFile()
        {
            var parser = new ScheduleConsolidator(new Parser(), Log.Logger);

            var records = parser.Read(DataFile).ToArray();

            Assert.IsType<Header>(records[0]);
            Assert.IsType<Trailer>(records[records.Length - 1]);
        }


        [Fact]
        public void ConsolidateSchedule()
        {
            var parser = new ScheduleConsolidator(new Parser(), Log.Logger);

            var schedule = parser.Read(DataFile).OfType<Schedule>().First();

            Assert.NotEmpty(schedule.Records);
        }
    }
}
