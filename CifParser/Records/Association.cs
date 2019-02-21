using FileHelpers;
using System;

namespace CifParser.Records
{

    /// <summary>
    /// Association Record: AA
    /// </summary>
    [FixedLengthRecord(FixedMode.AllowLessChars)]
    public class Association : ICifRecord
    {
        /// <summary>
        ///Record type 
        /// </summary>
        /// <remarks>Length 2, Position 1-2</remarks>
        [FieldFixedLength(2)]
        public string Type { get; set; }
        /// <summary>
        /// CRUD record type 
        /// </summary>
        /// <remarks>Length 1, Position 3-3</remarks>
        [FieldFixedLength(1)]
        [FieldConverter(typeof(ScheduleActionConverter))]
        public RecordAction Action { get; set; }
        /// <summary>
        /// Base Train UID - the through train
        /// </summary>
        /// <remarks>Length 6, Position 4-9</remarks>
        [FieldFixedLength(6)]
        public string MainUid { get; set; }
        /// <summary>
        /// Assocociated Train UID - the split\join
        /// </summary>
        /// <remarks>Length 6, Position 10-15</remarks>
        [FieldFixedLength(6)]
        public string AssociatedUid { get; set; }
        /// <summary>
        /// Runs from
        /// </summary>
        /// <remarks>Length 6, Position 16-21
        /// Format: yyMMdd </remarks> 
        [FieldFixedLength(6)]
        [FieldConverter(ConverterKind.Date, "yyMMdd")]
        public DateTime RunsFrom { get; set; }
        /// <summary>
        /// Runs To
        /// </summary>
        /// <remarks>Length 6, Position 22-27
        /// Format: yyMMdd </remarks> 
        [FieldFixedLength(6)]
        [FieldTrim(TrimMode.Right)]
        [FieldConverter(ConverterKind.Date, "yyMMdd")]
        public DateTime? RunsTo { get; set; }
        /// <summary>
        /// Days association occurs5
        /// </summary>
        /// <remarks>Length 7, Position 28-34
        /// Monday to Sunday</remarks>
        [FieldFixedLength(7)]
        [FieldTrim(TrimMode.Right)]
        public string DayMask { get; set; }
        /// <summary>
        /// Association Category
        /// </summary>
        /// <remarks>Length 2, Position 35-36
        /// Blank - used to override the permanent value in overlays and cancellations
        /// JJ - Joining
        /// VV - Splitting
        /// NP - Next/Previous.</remarks>
        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Right)]
        public string Category { get; set; }
        /// <summary>
        /// Association Date Indicator
        /// </summary>
        /// <remarks>Length 1, Position 37-37
        /// Blank - used to override the permanent value in overlays and cancellations
        /// S - Standard
        /// N - Over next midnight
        /// P - Over previous midnight</remarks>
        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Right)]
        public string DateIndicator { get; set; }
        /// <summary>
        /// Location - TIPLOC where the association occurs
        /// </summary>
        /// <remarks>Length 7, Position 38-44/remarks>
        [FieldFixedLength(7)]
        [FieldTrim(TrimMode.Right)]
        public string Location { get; set; }
        /// <summary>
        /// Base Location Sequence 
        /// </summary>
        /// <remarks>Length 1, Position 45-45
        /// This is to handle where a location appears multiple times in a schedule</remarks>
        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Right)]
        [FieldNullValue(1)]
        public int MainSequence { get; set; }
        /// <summary>
        /// Association Location Sequence 
        /// </summary>
        /// <remarks>Length 1, Position 46-46
        /// This is to handle where a location appears multiple times in a schedule</remarks>
        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Right)]
        [FieldNullValue(1)]
        public int AssociationSequence { get; set; }
        /// <summary>
        /// Diagram Type - always T
        /// </summary>
        /// <remarks>Length 1, Position 47-47</remarks> 
        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Right)]
        public string DiagramType { get; set; }
        /// <summary>
        /// Association Type
        /// </summary>
        /// <remarks>Length 1, Position 48-48
        /// P - Passenger use
        /// O - Operating use </remarks> 
        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Right)]
        public string AssociationType { get; set; }
        /// <summary>
        /// Spare - NOT USED
        /// </summary>
        /// <remarks>Length 31, Position 49-79</remarks> 
        [FieldFixedLength(31)]
        [FieldTrim(TrimMode.Right)]
        public string Spare { get; set; }
        /// <summary>
        /// STP (Short Term Plan) Indicator
        /// </summary>
        /// <remarks>Length 1, Position 80-80</remarks> 
        [FieldFixedLength(1)]
        public StpIndicator StpIndicator { get; set; }

        public override string ToString()
        {
            return $"{MainUid}-{AssociatedUid} @ {Location} STP: {StpIndicator} {Action}";
        }
    }
}
