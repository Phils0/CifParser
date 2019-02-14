using CifParser.Records;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CifParser
{
    /// <summary>
    /// Structure to hold all of a schedule records
    /// </summary>
    /// <remarks>TimetableUid, STPIndicator and Action form a unique identifier </remarks>
    public class Schedule : ICifRecord
    {
        public IList<ICifRecord> Records { get; }  = new List<ICifRecord>();

        public ScheduleDetails ScheduleDetails { get; }
        public ScheduleExtraData ScheduleExtraDetails { get; }

        public string TimetableUid => ScheduleDetails.TimetableUid;
        public StpIndicator StpIndicator => ScheduleDetails.StpIndicator;
        public RecordAction Action => ScheduleDetails.Action;

        public Schedule(ScheduleDetails schedule)
        {
            ScheduleDetails = schedule;
            Records.Add(schedule);            
        }

        public void Add(ICifRecord record)
        {
            Records.Add(record);
        }

        public bool IsTerminated => Records.Last() is TerminalLocation;

        public override string ToString()
        {
            return $"{TimetableUid} STP: {StpIndicator} {Action}";
        }
    }
}
