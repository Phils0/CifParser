using CifParser;
using CifParser.Records;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace CifParserTest
{
    // These tests assume a correctly formatted CIF file
    // Not checking for errors in format i.e. wrongly ordered records
    public class ScheduleConsolidatorTest
    {
        private TextReader _dummy = null;

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
        public void ImmediatelyReturnNonServiceRecords(ICifRecord record)
        {
            var parser = Substitute.For<IParser>();
            parser.Read(Arg.Any<TextReader>()).Returns(new[] { record });

            var consolidator = new ScheduleConsolidator(parser);

            var returned = consolidator.Read(_dummy).Single();

            Assert.Equal(record, returned);
        }

        [Fact]
        public void ReturnServiceWhenTerminated()
        {
            var parser = Substitute.For<IParser>();
            parser.Read(Arg.Any<TextReader>()).Returns(new ICifRecord[] { new ScheduleDetails(), new TerminalLocation() });

            var consolidator = new ScheduleConsolidator(parser);

            var returned = consolidator.Read(_dummy).Single(); ;

            Assert.IsType<Schedule>(returned);
        }

        [Fact]
        public void DoesNotReturnServiceUntilTerminated()
        {
            var parser = Substitute.For<IParser>();
            parser.Read(Arg.Any<TextReader>()).Returns(new[] { new ScheduleDetails() });

            var consolidator = new ScheduleConsolidator(parser);

            var returned = consolidator.Read(_dummy);

            Assert.Empty(returned);
        }

        [Fact]
        public void ReturnAllRecords()
        {
            var records = new ICifRecord[] { new ScheduleDetails(), new IntermediateLocation(), new TerminalLocation() };
            var parser = Substitute.For<IParser>();
            parser.Read(Arg.Any<TextReader>()).Returns(records);

            var consolidator = new ScheduleConsolidator(parser);

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
        public void ReturnScheduleRecordsInService(ICifRecord record)
        {
            var records = new ICifRecord[] { new ScheduleDetails(), record, new TerminalLocation() };
            var parser = Substitute.For<IParser>();
            parser.Read(Arg.Any<TextReader>()).Returns(records);

            var consolidator = new ScheduleConsolidator(parser);

            var returned = consolidator.Read(_dummy).Single() as Schedule;

            Assert.Contains(record, returned.Records);
        }

        [Fact]
        public void MultipleServicesReturned()
        {
            var records = new ICifRecord[] { new ScheduleDetails(), new TerminalLocation(), new ScheduleDetails(), new TerminalLocation() };
            var parser = Substitute.For<IParser>();
            parser.Read(Arg.Any<TextReader>()).Returns(records);

            var consolidator = new ScheduleConsolidator(parser);

            var returned = consolidator.Read(_dummy).ToArray();

            Assert.Equal(2, returned.Length);
        }

        [Fact]
        public void HandleRecordsInOrderInCif()
        {
            var records = new ICifRecord[] 
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

            var consolidator = new ScheduleConsolidator(parser);

            var returned = consolidator.Read(_dummy).ToArray();

            Assert.Equal(6, returned.Length);
            Assert.Equal("Service1", ((Schedule) returned[3]).GetTimetableUid());
            Assert.Equal("Service2", ((Schedule)returned[4]).GetTimetableUid());
        }
        
        [Fact]
        public void ReturnAllRecordsWHenHaveDelete()
        {
            var records = new ICifRecord[] { new ScheduleDetails() {Action = RecordAction.Delete}, new Trailer(),  };
            var parser = Substitute.For<IParser>();
            parser.Read(Arg.Any<TextReader>()).Returns(records);

            var consolidator = new ScheduleConsolidator(parser);

            var returned = consolidator.Read(_dummy);

            Assert.Equal(records.Length, returned.Count());
        }
    }
}
