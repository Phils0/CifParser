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
    public class Schedule : ICifRecord
    {
        public IList<ICifRecord> Records { get; set; } = new List<ICifRecord>();

        public ScheduleDetails GetScheduleDetails() => Records.OfType<ScheduleDetails>().First();
        public ScheduleExtraData GetScheduleExtraDetails() => Records.OfType<ScheduleExtraData>().FirstOrDefault();


        public ScheduleId GetId()
        {
            var details = GetScheduleDetails();
            return new ScheduleId(
                details.TimetableUid,
                details.StpIndicator,
                details.Action);
        }
                
        public void Add(ICifRecord record)
        {
            Records.Add(record);
            switch (record)
            {
                case TerminalLocation terminalLocation:
                    IsTerminated = true;
                    break;
                case ScheduleDetails schedule:
                    IsTerminated = schedule.Action.Equals(RecordAction.Delete);    // Delete has no child records so immediately terminate
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