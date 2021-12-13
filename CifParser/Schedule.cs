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
        /// <summary>
        /// Schedule properties
        /// </summary>
        public ScheduleDetails ScheduleDetails { get; private set; } = null!;
        /// <summary>
        /// Additional schedule properties
        /// </summary>
        public ScheduleExtraData? ScheduleExtraDetails {get; private set; }
        /// <summary>
        /// Stops + Change Records
        /// </summary>
        public IList<IRecord> Records { get; set; } = new List<IRecord>();

        /// <summary>
        /// All Records
        /// </summary>
        public IEnumerable<IRecord> All
        {
            get
            {
                yield return ScheduleDetails;
                if(ScheduleExtraDetails != null)
                {
                    yield return ScheduleExtraDetails;
                }
                foreach (var stop in Records)
                {
                    yield return stop;
                }
            }
        }
        
        public ScheduleId GetId()
        {
            var details = ScheduleDetails;
            return new ScheduleId(
                details.TimetableUid,
                details.RunsFrom,
                details.StpIndicator,
                details.Action);
        }
                
        public void Add(IRecord record)
        {
            switch (record)
            {
                case ScheduleDetails schedule:
                    ScheduleDetails = schedule;
                    // Delete and Cancel have no child records so immediately terminate
                    IsTerminated = schedule.Action.Equals(RecordAction.Delete) || schedule.StpIndicator.Equals(StpIndicator.C);
                    break;
                case ScheduleExtraData extra:
                    ScheduleExtraDetails = extra;
                    break; 
                case TerminalLocation terminalLocation:
                    Records.Add(record);
                    IsTerminated = true;
                    break;
                default:
                    Records.Add(record);
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