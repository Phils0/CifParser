using System;
using CifParser.Records;

namespace CifParser
{
    public class ScheduleLocationId : IEquatable<ScheduleLocationId>
    {
        public ScheduleId Schedule { get; }
        public string Location { get; }
        public int Sequence { get; }
        
        public ScheduleLocationId(ScheduleId schedule, string location, int locationSequence)
        {
            Schedule = schedule;
            Location = location;
            Sequence = locationSequence;
        }

        public bool Equals(ScheduleLocationId? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Schedule.Equals(other.Schedule) && string.Equals(Location, other.Location) && Sequence == other.Sequence;
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ScheduleLocationId) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Schedule.GetHashCode();
                hashCode = (hashCode * 397) ^ Location.GetHashCode();
                hashCode = (hashCode * 397) ^ Sequence;
                return hashCode;
            }
        }

        public override string ToString()
        {
            return Sequence > 1 ?
                $"{Location}:{Sequence} {Schedule}" :
                $"{Location} {Schedule}";
        }
    }
}