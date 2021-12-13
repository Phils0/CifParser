using System;
using CifParser.Records;

namespace CifParser
{
    public class ScheduleId : IEquatable<ScheduleId>
    {
        public string TimetableUid { get; }
        public StpIndicator StpIndicator { get; }
        public RecordAction Action { get; }

        public DateTime RunsFrom { get; }
        
        public ScheduleId(string id, DateTime runsFrom, StpIndicator stp, RecordAction action)
        {
            TimetableUid = id;
            StpIndicator = stp;
            Action = action;
            RunsFrom = runsFrom;
        }

        public bool Equals(ScheduleId? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(TimetableUid, other.TimetableUid) && StpIndicator == other.StpIndicator && Action == other.Action && RunsFrom.Equals(other.RunsFrom);
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ScheduleId) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = TimetableUid.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) StpIndicator;
                hashCode = (hashCode * 397) ^ (int) Action;
                hashCode = (hashCode * 397) ^ RunsFrom.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{TimetableUid}-{RunsFrom:yyyyMMdd}:{StpIndicator}-{Action}";
        }
    }
}