using CifParser;
using CifParser.Records;
using System;
using Xunit;

namespace CifParserTest
{
    public class ScheduleTest
    {
        private static readonly ScheduleDetails ScheduleRecord = new ScheduleDetails()
        {
            TimetableUid = "A12345",
            RunsFrom = new DateTime(2019, 1, 2),
            StpIndicator = StpIndicator.P,
            Action = RecordAction.Create
        };

        private Schedule TestSchedule
        {
            get
            {
                var schedule = new Schedule();
                schedule.Add(ScheduleRecord);
                return schedule;
            }
        }
        
        [Fact]
        public void IdIsBaseScheduleValues()
        {
            var id = TestSchedule.GetId();
            Assert.Equal("A12345", id.TimetableUid);
            Assert.Equal(StpIndicator.P, id.StpIndicator);
            Assert.Equal(RecordAction.Create, id.Action);
        }

        [Fact]
        public void ToStringOutputsId()
        {
            Assert.Equal("A12345-20190102:P-Create", TestSchedule.ToString());
        }
        
        [Fact]
        public void InitiallyIsTerminatedIsFalse()
        {
            Assert.False(TestSchedule.IsTerminated);
        }

        [Fact]
        public void IsTerminatedIsFalseWhenHasNoTerminalRecord()
        {
            var schedule = TestSchedule;
            schedule.Add(new ScheduleExtraData());
            schedule.Add(new OriginLocation());
            schedule.Add(new IntermediateLocation());
            Assert.False(schedule.IsTerminated);
        }

        [Fact]
        public void IsTerminatedIsTrueWhenHasTerminalRecord()
        {
            var schedule = TestSchedule;
            schedule.Add(new TerminalLocation());

            Assert.True(schedule.IsTerminated);
        }
        
        
        [Fact]
        public void IsTerminatedIsTrueWhenIsADeleteRecord()
        {
            var delete = new ScheduleDetails()
            {
                TimetableUid = "A12345",
                RunsFrom = new DateTime(2019, 1, 2),
                StpIndicator = StpIndicator.P,
                Action = RecordAction.Delete
            };
            var schedule = new Schedule();
            schedule.Add(delete);

            Assert.True(schedule.IsTerminated);
        }
        
        [Fact]
        public void IsTerminatedIsTrueWhenIsACancelRecord()
        {
            var cancel = new ScheduleDetails()
            {
                TimetableUid = "A12345",
                RunsFrom = new DateTime(2019, 1, 2),
                StpIndicator = StpIndicator.C,
                Action = RecordAction.Create
            };
            var schedule = new Schedule();
            schedule.Add(cancel);

            Assert.True(schedule.IsTerminated);
        }
        
        [Fact]
        public void HasExtraData()
        {
            var schedule = TestSchedule;
            schedule.Add(new ScheduleDetails());
            schedule.Add(new ScheduleExtraData());
            Assert.True(schedule.HasExtraDetails);
        }
        
        [Fact]
        public void DoesNotHaveExtraData()
        {
            var schedule = TestSchedule;
            schedule.Add(new ScheduleDetails());
            Assert.False(schedule.HasExtraDetails);
        }
    }
}
