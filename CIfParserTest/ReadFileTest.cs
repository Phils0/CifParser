using CifParser;
using CifParser.Records;
using System;
using System.IO;
using System.Linq;
using NSubstitute;
using Serilog;
using Xunit;

namespace CifParserTest
{
    public class ReadFileTest
    {
        private static readonly string DataFile = Path.Combine(".", "Data", "toc-update-mon.CIF");

        private TextReader Reader => File.OpenText(DataFile);
        
        [Fact]
        public void ParseFile()
        {
            var factory = new CifParserFactory(Substitute.For<ILogger>());
            var parser = factory.CreateParser();

            var records = parser.Read(Reader).ToArray();

            Assert.IsType<Header>(records[0]);
            Assert.IsType<Trailer>(records[records.Length - 1]);
        }

        [Fact]
        public void ParseAndConsolidateFile()
        {
            var logger = Substitute.For<ILogger>();
            var factory = new ConsolidatorFactory(Substitute.For<ILogger>());
            var parser = factory.CreateParser();

            var records = parser.Read(Reader).ToArray();

            Assert.IsType<Header>(records[0]);
            Assert.IsType<Trailer>(records[records.Length - 1]);
        }

        [Fact]
        public void ConsolidateSchedule()
        {
            var logger = Substitute.For<ILogger>();
            var factory = new ConsolidatorFactory(Substitute.For<ILogger>());
            var parser = factory.CreateParser();

            var schedule = parser.Read(Reader).OfType<Schedule>().First();

            Assert.NotEmpty(schedule.All);
        }
    }
}
