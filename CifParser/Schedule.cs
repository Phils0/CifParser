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

        private ScheduleDetails _schedule;

        public string TimetableUid => _schedule.TimetableUid;
        public StpIndicator StpIndicator => _schedule.StpIndicator;
        public RecordAction Action => _schedule.Action;

        public Schedule(ScheduleDetails schedule)
        {
            _schedule = schedule;
            Records.Add(schedule);            
        }

        public void Add(ICifRecord record)
        {
            Records.Add(record);
        }

        public bool IsTerminated => Records.Last() is TerminalLocation;
    }
}
