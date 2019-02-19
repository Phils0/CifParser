using CifParser;
using CifParser.Records;
using System;
using Castle.Core;
using Xunit;

namespace CifParserTest
{
    public class ScheduleTest
    {
        private static readonly ScheduleDetails ScheduleRecord = new ScheduleDetails()
        {
            TimetableUid = "A12345",
            StpIndicator = StpIndicator.P,
            Action = RecordAction.Create
        };

        private static readonly ScheduleDetails DeleteRecord = new ScheduleDetails()
        {
            TimetableUid = "A12345",
            StpIndicator = StpIndicator.P,
            Action = RecordAction.Delete
        };

        [Fact]
        public void IdIsBaseScheduleValues()
        {
            var schedule = new Schedule();
            schedule.Add(ScheduleRecord);

            var id = schedule.GetId();
            Assert.Equal("A12345", id.TimetableUid);
            Assert.Equal(StpIndicator.P, id.StpIndicator);
            Assert.Equal(RecordAction.Create, id.Action);
        }

        [Fact]
        public void InitiallyIsTerminatedIsFalse()
        {
            var service = new Schedule();
            service.Add(ScheduleRecord);
            Assert.False(service.IsTerminated);
        }

        [Fact]
        public void IsTerminatedIsFalseWhenHasNoTerminalRecord()
        {
            var service = new Schedule();
            service.Add(ScheduleRecord);
            service.Add(new ScheduleExtraData());
            service.Add(new OriginLocation());
            service.Add(new IntermediateLocation());
            Assert.False(service.IsTerminated);
        }

        [Fact]
        public void IsTerminatedIsTrueWhenHasTerminalRecord()
        {
            var service = new Schedule();
            service.Add(ScheduleRecord);
            service.Add(new TerminalLocation());

            Assert.True(service.IsTerminated);
        }
        
        
        [Fact]
        public void IsTerminatedIsTrueWhenIsADeleteRecord()
        {
            var service = new Schedule();
            service.Add(DeleteRecord);

            Assert.True(service.IsTerminated);
        }
    }
}
