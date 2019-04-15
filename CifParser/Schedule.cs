using CifParser.Records;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CifParser
{
    /// <summary>
    /// Structure to hold all of a schedule records
    /// </summary>
    /// <remarks>TimetableUid, STPIndicator and Action form a unique identifier </remarks>
    public class Schedule : IRecord
    {
        public IList<IRecord> Records { get; set; } = new List<IRecord>();

        public ScheduleDetails GetScheduleDetails() => Records.OfType<ScheduleDetails>().First();
        public ScheduleExtraData GetScheduleExtraDetails() => Records.OfType<ScheduleExtraData>().FirstOrDefault();
        public ScheduleId GetId()
        {
            var details = GetScheduleDetails();
            return new ScheduleId(
                details.TimetableUid,
                details.RunsFrom,
                details.StpIndicator,
                details.Action);
        }
                
        public void Add(IRecord record)
        {
            Records.Add(record);
            switch (record)
            {
                case TerminalLocation terminalLocation:
                    IsTerminated = true;
                    break;
                case ScheduleDetails schedule:
                    // Delete and Cancel have no child records so immediately terminate
                    IsTerminated = schedule.Action.Equals(RecordAction.Delete) || schedule.StpIndicator.Equals(StpIndicator.C);
                    break;         
            }
        }

        public bool IsTerminated { get; set; }

        public override string ToString()
        {
            return GetId().ToString();
        }
    }
}