using FileHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CifParser.Records
{
    /// <summary>
    /// Terminating Location Record: LT
    [FixedLengthRecord(FixedMode.AllowLessChars)]
    public class TerminalLocation : IRecord
    {
        /// <summary>
        ///Record type 
        /// </summary>
        /// <remarks>Length 2, Position 1-2</remarks>
        [FieldFixedLength(2)]
        public string Type { get; set; } = null!;
        /// <summary>
        ///Location - TIPLOC 
        /// </summary>
        /// <remarks>Length 7, Position 3-9</remarks>
        [FieldFixedLength(7)]
        [FieldTrim(TrimMode.Right)]
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// Location Sequence 
        /// </summary>
        /// <remarks>Length 1, Position 10-10
        /// This is to handle where a location appears multiple times in a schedule</remarks>
        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Right)]
        [FieldNullValue(1)]
        public int Sequence { get; set; } = -1;
        /// <summary>
        /// Working Arrival Time
        /// </summary>
        /// <remarks>Length 5, Position 11-15/remarks>
        [FieldFixedLength(5)]
        [FieldConverter(typeof(WorkingTimeConverter))]
        public TimeSpan? WorkingArrival { get; set; }
        /// <summary>
        /// Public Arrival Time
        /// </summary>
        /// <remarks>Length 4, Position 16-19/remarks>
        [FieldFixedLength(4)]
        [FieldConverter(typeof(PublicTimeConverter))]
        public TimeSpan? PublicArrival { get; set; }
        /// <summary>
        /// Platform
        /// </summary>
        /// <remarks>Length 3, Position20-22/remarks>
        [FieldFixedLength(3)]
        [FieldTrim(TrimMode.Right)]
        public string Platform { get; set; } = string.Empty;
        /// <summary>
        /// Path
        /// </summary>
        /// <remarks>Length 3, Position 23-25/remarks>
        [FieldFixedLength(3)]
        [FieldTrim(TrimMode.Right)]
        public string Path { get; set; } = string.Empty;
        /// <summary>
        /// Activity
        /// </summary>
        /// <remarks>Length 12, Position 26-37/remarks>
        [FieldFixedLength(12)]
        [FieldTrim(TrimMode.Right)]
        public string Activities { get; set; } = string.Empty;
        /// <summary>
        /// Spare - NOT USED
        /// </summary>
        /// <remarks>Length 43, Position38-80</remarks> 
        [FieldFixedLength(43)]
        [FieldTrim(TrimMode.Right)]
        public string Spare { get; set; } = string.Empty;
        
        public override string ToString()
        {
            var stop = PublicArrival.HasValue ? "Stop" : "";
            var time = (PublicArrival ?? WorkingArrival)?.ToString(@"hh\:mm\:ss");
            return Sequence > 1 ? 
                $"{Location}-{Sequence} {time} {stop}" :
                $"{Location} {time} {stop}";
        }
    }
}
