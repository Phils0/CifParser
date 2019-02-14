using FileHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CifParser.Records
{
    /// <summary>
    /// Origin Location Record: LO
    /// </summary>
    [FixedLengthRecord(FixedMode.AllowLessChars)]
    public class OriginLocation : ICifRecord
    {
        /// <summary>
        ///Record type 
        /// </summary>
        /// <remarks>Length 2, Position 1-2</remarks>
        [FieldFixedLength(2)]
        public string Type { get; set; }
        /// <summary>
        ///Location - TIPLOC 
        /// </summary>
        /// <remarks>Length 7, Position 3-9</remarks>
        [FieldFixedLength(7)]
        [FieldTrim(TrimMode.Right)]
        public string Location { get; set; }
        /// <summary>
        /// Location Sequence 
        /// </summary>
        /// <remarks>Length 1, Position 10-10
        /// This is to handle where a location appears multiple times in a schedule</remarks>
        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Right)]
        [FieldNullValue(1)]
        public int Sequence { get; set; }
        /// <summary>
        /// Working Arrival Time
        /// </summary>
        /// <remarks>Length 5, Position 11-15/remarks>
        [FieldFixedLength(5)]
        [FieldConverter(typeof(WorkingTimeConverter))]

        public TimeSpan? WorkingDeparture { get; set; }
        /// <summary>
        /// Public Departure Time
        /// </summary>
        /// <remarks>Length 4, Position 16-19/remarks>
        [FieldFixedLength(4)]
        [FieldConverter(typeof(PublicTimeConverter))]
        public TimeSpan? PublicDeparture { get; set; }
        /// <summary>
        /// Platform
        /// </summary>
        /// <remarks>Length 3, Position 20-22/remarks>
        [FieldFixedLength(3)]
        [FieldTrim(TrimMode.Right)]
        public string Platform { get; set; }
        /// <summary>
        /// Line
        /// </summary>
        /// <remarks>Length 3, Position 23-25/remarks>
        [FieldFixedLength(3)]
        [FieldTrim(TrimMode.Right)]
        public string Line { get; set; }
        /// <summary>
        /// Engineering Allowance
        /// </summary>
        /// <remarks>Length 2, Position 26-27/remarks>
        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Right)]
        public string EngineeringAllowance { get; set; }
        /// <summary>
        /// Pathing Allowance
        /// </summary>
        /// <remarks>Length 2, Position 28-29/remarks>
        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Right)]
        public string PathingAllowance { get; set; }
        /// <summary>
        /// Activity
        /// </summary>
        /// <remarks>Length 12, Position 30-41/remarks>
        [FieldFixedLength(12)]
        [FieldTrim(TrimMode.Right)]
        public string Activities { get; set; }
        /// <summary>
        /// Performance Allowance
        /// </summary>
        /// <remarks>Length 2, Position 42-43/remarks>
        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Right)]
        public string PerformanceAllowance { get; set; }
        /// <summary>
        /// Spare - NOT USED
        /// </summary>
        /// <remarks>Length 37, Position 44-80</remarks> 
        [FieldFixedLength(37)]
        [FieldTrim(TrimMode.Right)]
        public string Spare { get; set; }
        
        public override string ToString()
        {
            var stop = PublicDeparture.HasValue ? "Stop" : "";
            var time = (PublicDeparture ?? WorkingDeparture)?.ToString(@"hh\:mm\:ss");
            return Sequence > 1 ? 
                $"{Location}-{Sequence} {time} {stop}" :
                $"{Location} {time} {stop}";
        }
    }
}
