using FileHelpers;
using System;

namespace CifParser.Records
{
    /// <summary>
    /// Accomodation classes supported
    /// </summary>
    public enum ServiceClass
    {
        None,   // Not available (Sleepers only)
        B,      // Both First and Standard
        S,      // Standard only
        F       // First only (Sleepers only)
    }

    /// <summary>
    /// Possible reservation settings, making the ARSE mnemonic
    /// </summary>
    public enum ReservationIndicator
    {
        None,   // Not supported
        A,      // Always - Manadatory
        R,      // Recommended
        S,      // Supported
        E       // Essential for bicycles - never seen this value set
    }

    /// <summary>
    /// Short Term Plan (STP) 
    /// </summary>
    public enum StpIndicator
    {
        P,  // Permanent schedule
        C,  // STP Cancellation of Permanent schedule
        N,  // New STP schedule (not an overlay)
        O,  // STP overlay of Permanent schedule
    }

    /// <summary>
    /// Basic Schedule Record: BS
    /// </summary>
    [FixedLengthRecord(FixedMode.AllowLessChars)]
    public class ScheduleDetails : ICifRecord
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
        /// Train UID - unique timetable Id
        /// </summary>
        /// <remarks>Length 6, Position 4-9</remarks>
        [FieldFixedLength(6)]
        public string TimetableUid { get; set; }
        /// <summary>
        /// Runs from
        /// </summary>
        /// <remarks>Length 6, Position 10-15
        /// Format: yyMMdd </remarks> 
        [FieldFixedLength(6)]
        [FieldConverter(ConverterKind.Date, "yyMMdd")]
        public DateTime RunsFrom { get; set; }
        /// <summary>
        /// Runs To
        /// </summary>
        /// <remarks>Length 6, Position 16-21
        /// Format: yyMMdd </remarks> 
        [FieldFixedLength(6)]
        [FieldTrim(TrimMode.Right)]
        [FieldConverter(ConverterKind.Date, "yyMMdd")]
        public DateTime? RunsTo { get; set; }
        /// <summary>
        /// Days service runs
        /// </summary>
        /// <remarks>Length 7, Position 22-28
        /// Monday to Sunday</remarks>
        [FieldFixedLength(7)]
        [FieldTrim(TrimMode.Right)]
        public string DayMask { get; set; }
        /// <summary>
        /// WHether runs on a bank holiday
        /// </summary>
        /// <remarks>Length 1, Position 29-29
        /// empty - runs
        /// X - Does not run on specified Bank Holiday Mondays
        /// E - Does not run on Edinburgh Bank Holidays. NO LONGER USED
        /// G - Does not run on Glasgow Bank Holidays.</remarks>
        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Right)]
        public string BankHolidayRunning { get; set; }
        /// <summary>
        /// Status - values incorporates transport mode and whether its permanant or STP
        /// </summary>
        /// <remarks>Length 1, Position 30-30
        /// For values: https://wiki.openraildata.com/index.php?title=CIF_Codes#Train_Status </remarks>
        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Right)]
        public string Status { get; set; }
        /// <summary>
        /// Train Category
        /// </summary>
        /// <remarks>Length 2, Position 31-32
        /// For values: https://wiki.openraildata.com/index.php?title=CIF_Codes#Train_Category </remarks>
        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Right)]
        public string Category { get; set; }
        /// <summary>
        /// Train Identity
        /// </summary>
        /// <remarks>Length 4, Position 33-36
        /// Sometimes called the headcode (but not to be confused with headcode in this record)</remarks>
        [FieldFixedLength(4)]
        [FieldTrim(TrimMode.Right)]
        public string TrainIdentity { get; set; }
        /// <summary>
        /// NRS HeadCode
        /// </summary>
        /// <remarks>Length 4, Position 37-40
        /// Not to be confused with the TrainIdentity in this record </remarks>
        [FieldFixedLength(4)]
        [FieldTrim(TrimMode.Right)]
        public string HeadCode { get; set; }
        /// <summary>
        /// Course Indicator - NO LONGER USED
        /// </summary>
        /// <remarks>Length 1, Position 41-41 
        /// Value is always 1</remarks>
        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Right)]
        public string CourseIndicator { get; set; }
        /// <summary>
        /// Train service code, used for revenue attribution
        /// </summary>
        /// <remarks>Length 8, Position 42-49</remarks>
        [FieldFixedLength(8)]
        [FieldTrim(TrimMode.Right)]
        public string ServiceCode { get; set; }
        /// <summary>
        /// Portion Id, used for joining/splitting services
        /// </summary>
        /// <remarks>Length 1, Position 50-50
        /// Contain the portion suffix for RSID </remarks>
        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Right)]
        public string PortionId { get; set; }
        /// <summary>
        /// How the train is powered
        /// </summary>
        /// <remarks>Length 3, Position 51-53
        /// For values: https://wiki.openraildata.com/index.php?title=CIF_Codes#Power_Type </remarks>
        [FieldFixedLength(3)]
        [FieldTrim(TrimMode.Right)]
        public string PowerType { get; set; }
        /// <summary>
        /// How the train is powered detail
        /// </summary>
        /// <remarks>Length 4, Position 54-57
        /// For values: https://wiki.openraildata.com/index.php?title=CIF_Codes#Timing_Load </remarks>
        [FieldFixedLength(4)]
        [FieldTrim(TrimMode.Right)]
        public string TimingLoadType { get; set; }
        /// <summary>
        /// Planned speed of service
        /// </summary>
        /// <remarks>Length 3, Position 58-60</remarks>
        [FieldFixedLength(3)]
        [FieldTrim(TrimMode.Right)]
        public string Speed { get; set; }
        /// <summary>
        /// How the train is powered detail
        /// </summary>
        /// <remarks>Length 3, Position 61-66
        /// For values: https://wiki.openraildata.com/index.php?title=CIF_Codes#Operating_Characteristics </remarks>
        [FieldFixedLength(6)]
        [FieldTrim(TrimMode.Right)]
        public string OperatingCharacteristics { get; set; }
        /// <summary>
        /// Seating classes available on service
        /// </summary>
        /// <remarks>Length 1, Position 67-67
        /// Blank or B - First and standard
        /// S - Standard class only.</remarks>
        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Right)]
        [FieldNullValue(ServiceClass.B)]
        public ServiceClass SeatClass { get; set; }
        /// <summary>
        /// Sleeper classes available on service
        /// </summary>
        /// <remarks>Length 1, Position 68-68
        /// Blank - Not supported
        /// B - First and standard
        /// S - Standard class only.
        /// F - First class only</remarks>
        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Right)]
        [FieldNullValue(ServiceClass.None)]
        public ServiceClass SleeperClass { get; set; }
        /// <summary>
        /// Reservation indicator
        /// </summary>
        /// <remarks>Length 1, Position 69-69
        /// Values: ARSE </remarks>
        [FieldFixedLength(1)]
        [FieldNullValue(ReservationIndicator.None)]
        public ReservationIndicator ReservationIndicator { get; set; }
        /// <summary>
        /// Connect Indicator - NOT USED
        /// </summary>
        /// <remarks>Length 1, Position 70-70</remarks>
        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Right)]
        public string ConnectIndicator { get; set; }
        /// <summary>
        /// Available catering
        /// </summary>
        /// <remarks>Length 4, Position 71-74
        /// C - Buffet Service
        /// F - Restaurant Car available for First Class passengers
        /// H - Hot food available
        /// M - Meal included for First Class passengers
        /// R - Restaurant
        /// T - Trolley service. </remarks>
        [FieldFixedLength(4)]
        [FieldTrim(TrimMode.Right)]
        public string Catering { get; set; }
        /// <summary>
        /// Service branding
        /// </summary>
        /// <remarks>Length 4, Position 75-78
        /// E - Eurostar</remarks>
        [FieldFixedLength(4)]
        [FieldTrim(TrimMode.Right)]
        public string Branding { get; set; }
        /// <summary>
        /// Spare - NOT USED
        /// </summary>
        /// <remarks>Length 1, Position 79-79</remarks> 
        [FieldFixedLength(1)]
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
            return $"{TimetableUid} STP: {StpIndicator} {Action}";
        }
    }
}
