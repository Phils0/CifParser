﻿using CifParser;
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
            StpIndicator = StpIndicator.P,
            Action = RecordAction.Create
        };


        [Fact]
        public void TimetableUIdIsBaseScheduleValue()
        {
            var service = new Schedule(ScheduleRecord);
            Assert.Equal("A12345", service.GetTimetableUid());
        }

        [Fact]
        public void StpIndicatorIsBaseScheduleValue()
        {
            var service = new Schedule(ScheduleRecord);
            Assert.Equal(StpIndicator.P, service.GetStpIndicator());
        }

        [Fact]
        public void ActionIsBaseScheduleValue()
        {
            var service = new Schedule(ScheduleRecord);
            Assert.Equal(RecordAction.Create, service.GetAction());
        }

        [Fact]
        public void InitiallyIsTerminatedIsFalse()
        {
            var service = new Schedule(ScheduleRecord);
            Assert.False(service.IsTerminated);
        }

        [Fact]
        public void IsTerminatedIsFalsehenHasNoTerminalRecord()
        {
            var service = new Schedule(ScheduleRecord);
            service.Add(new ScheduleExtraData());
            service.Add(new OriginLocation());
            service.Add(new IntermediateLocation());
            Assert.False(service.IsTerminated);
        }

        [Fact]
        public void IsTerminatedIsTrueeWhenHasTerminalRecord()
        {
            var service = new Schedule(ScheduleRecord);
            service.Add(new TerminalLocation());

            Assert.True(service.IsTerminated);
        }
    }
}
