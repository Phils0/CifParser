using CifParser;
using CifParser.Records;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Serilog;
using Xunit;

namespace CifParserTest
{
    // These tests assume a correctly formatted CIF file
    // Not checking for errors in format i.e. wrongly ordered records
    public class ScheduleConsolidatorTest
    {
        private readonly TextReader _dummy = null;

        public static IEnumerable<object[]> NonScheduleRecords
        {
            get
            {
                yield return new object[] { new Header() };
                yield return new object[] { new Trailer() };
                yield return new object[] { new TiplocInsertAmend() };
                yield return new object[] { new TiplocDelete() };
                yield return new object[] { new Association() };
            }
        }

        [Theory]
        [MemberData(nameof(NonScheduleRecords))]
        public void ImmediatelyReturnNonServiceRecords(IRecord record)
        {
            var parser = Substitute.For<IParser>();
            parser.Read(Arg.Any<TextReader>()).Returns(new[] { record });

            var consolidator = new ScheduleConsolidator(parser, Substitute.For<ILogger>());

            var returned = consolidator.Read(_dummy).Single();

            Assert.Equal(record, returned);
        }

        [Fact]
        public void ReturnServiceWhenTerminated()
        {
            var parser = Substitute.For<IParser>();
            parser.Read(Arg.Any<TextReader>()).Returns(new IRecord[] { new ScheduleDetails(), new TerminalLocation() });

            var consolidator = new ScheduleConsolidator(parser, Substitute.For<ILogger>());

            var returned = consolidator.Read(_dummy).Single(); ;

            Assert.IsType<Schedule>(returned);
        }

        [Fact]
        public void DoesNotReturnServiceUntilTerminated()
        {
            var parser = Substitute.For<IParser>();
            parser.Read(Arg.Any<TextReader>()).Returns(new[] { new ScheduleDetails() });

            var consolidator = new ScheduleConsolidator(parser, Substitute.For<ILogger>());

            var returned = consolidator.Read(_dummy);

            Assert.Empty(returned);
        }

        [Fact]
        public void ReturnAllRecords()
        {
            var records = new IRecord[] { new ScheduleDetails(), new IntermediateLocation(), new TerminalLocation() };
            var parser = Substitute.For<IParser>();
            parser.Read(Arg.Any<TextReader>()).Returns(records);

            var consolidator = new ScheduleConsolidator(parser, Substitute.For<ILogger>());

            var returned = consolidator.Read(_dummy).Single() as Schedule;

            Assert.Equal(records.Length, returned.Records.Count);
            Assert.All(returned.Records, r => Assert.Contains(r, records));
        }

        public static IEnumerable<object[]> ScheduleRecords
        {
            get
            {
                yield return new object[] { new ScheduleExtraData() };
                yield return new object[] { new OriginLocation() };
                yield return new object[] { new IntermediateLocation() };
                yield return new object[] { new ScheduleChange() };
            }
        }

        [Theory]
        [MemberData(nameof(ScheduleRecords))]
        public void ReturnScheduleRecordsInService(IRecord record)
        {
            var records = new IRecord[] { new ScheduleDetails(), record, new TerminalLocation() };
            var parser = Substitute.For<IParser>();
            parser.Read(Arg.Any<TextReader>()).Returns(records);

            var consolidator = new ScheduleConsolidator(parser, Substitute.For<ILogger>());

            var returned = consolidator.Read(_dummy).Single() as Schedule;

            Assert.Contains(record, returned.Records);
        }

        [Fact]
        public void MultipleServicesReturned()
        {
            var records = new IRecord[] { new ScheduleDetails(), new TerminalLocation(), new ScheduleDetails(), new TerminalLocation() };
            var parser = Substitute.For<IParser>();
            parser.Read(Arg.Any<TextReader>()).Returns(records);

            var consolidator = new ScheduleConsolidator(parser, Substitute.For<ILogger>());

            var returned = consolidator.Read(_dummy).ToArray();

            Assert.Equal(2, returned.Length);
        }

        [Fact]
        public void HandleRecordsInOrderInCif()
        {
            var records = new IRecord[] 
                {
                    new Header(),
                    new TiplocInsertAmend(),
                    new Association(),
                    new ScheduleDetails() { TimetableUid = "Service1"}, 
                    new ScheduleExtraData(),
                    new OriginLocation(),
                    new IntermediateLocation(),
                    new TerminalLocation(),
                    new ScheduleDetails(){ TimetableUid = "Service2"},
                    new ScheduleExtraData(),
                    new OriginLocation(),
                    new IntermediateLocation(),
                    new ScheduleChange(),
                    new IntermediateLocation(),
                    new TerminalLocation(),
                    new Trailer()
                };

            var parser = Substitute.For<IParser>();
            parser.Read(Arg.Any<TextReader>()).Returns(records);

            var consolidator = new ScheduleConsolidator(parser, Substitute.For<ILogger>());

            var returned = consolidator.Read(_dummy).ToArray();

            Assert.Equal(6, returned.Length);
            Assert.Equal("Service1", ((Schedule) returned[3]).GetId().TimetableUid);
            Assert.Equal("Service2", ((Schedule)returned[4]).GetId().TimetableUid);
        }
        
        [Fact]
        public void ReturnAllRecordsWHenHaveDelete()
        {
            var records = new IRecord[] { new ScheduleDetails() {Action = RecordAction.Delete}, new Trailer(),  };
            var parser = Substitute.For<IParser>();
            parser.Read(Arg.Any<TextReader>()).Returns(records);

            var consolidator = new ScheduleConsolidator(parser, Substitute.For<ILogger>());

            var returned = consolidator.Read(_dummy);

            Assert.Equal(records.Length, returned.Count());
        }
        
        [Fact]
        public void LogErrorIfNotTerminatedASchedule()
        {
            var records = new IRecord[] 
            {
                new ScheduleDetails() { TimetableUid = "Service1"}, 
                new ScheduleExtraData(),
                new OriginLocation(),
                new IntermediateLocation(),
                new ScheduleDetails(){ TimetableUid = "Service2", Action = RecordAction.Delete},
            };
            
            var parser = Substitute.For<IParser>();
            parser.Read(Arg.Any<TextReader>()).Returns(records);
            var logger = Substitute.For<ILogger>();

            var consolidator = new ScheduleConsolidator(parser, logger);

            var returned = consolidator.Read(_dummy).ToArray();

            logger.ReceivedWithAnyArgs().Error<IRecord>(Arg.Any<string>(), Arg.Any<IRecord>());
        }
    }
}
