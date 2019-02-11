using FileHelpers;
using System;

namespace CifParser.Records
{
    /// <summary>
    /// Schedule Change Enroute Record: CR
    /// </summary>
    /// <remarks>Something changes with the servbice enroute</remarks>
    [FixedLengthRecord(FixedMode.AllowLessChars)]
    public class ScheduleChange : ICifRecord
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
        /// Train Category
        /// </summary>
        /// <remarks>Length 2, Position 11-12
        /// For values: https://wiki.openraildata.com/index.php?title=CIF_Codes#Train_Category </remarks>
        [FieldFixedLength(2)]
        [FieldTrim(TrimMode.Right)]
        public string Category { get; set; }
        /// <summary>
        /// Train Identity
        /// </summary>
        /// <remarks>Length 4, Position 13-16
        /// Sometimes called the headcode (but not to be confused with headcode in this record)</remarks>
        [FieldFixedLength(4)]
        [FieldTrim(TrimMode.Right)]
        public string TrainIdentity { get; set; }
        /// <summary>
        /// NRS HeadCode
        /// </summary>
        /// <remarks>Length 4, Position 17-20
        /// Not to be confused with the TrainIdentity in this record </remarks>
        [FieldFixedLength(4)]
        [FieldTrim(TrimMode.Right)]
        public string HeadCode { get; set; }
        /// <summary>
        /// Course Indicator - NO LONGER USED
        /// </summary>
        /// <remarks>Length 1, Position 21-21 
        /// Value is always 1</remarks>
        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Right)]
        public string CourseIndicator { get; set; }
        /// <summary>
        /// Train service code, used for revenue attribution
        /// </summary>
        /// <remarks>Length 8, Position 22-29</remarks>
        [FieldFixedLength(8)]
        [FieldTrim(TrimMode.Right)]
        public string ServiceCode { get; set; }
        /// <summary>
        /// Portion Id, used for joining/splitting services
        /// </summary>
        /// <remarks>Length 1, Position 30-30
        /// Contain the portion suffix for RSID </remarks>
        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Right)]
        public string PortionId { get; set; }
        /// <summary>
        /// How the train is powered
        /// </summary>
        /// <remarks>Length 3, Position 31-33
        /// For values: https://wiki.openraildata.com/index.php?title=CIF_Codes#Power_Type </remarks>
        [FieldFixedLength(3)]
        [FieldTrim(TrimMode.Right)]
        public string PowerType { get; set; }
        /// <summary>
        /// How the train is powered detail
        /// </summary>
        /// <remarks>Length 3, Position 34-37
        /// For values: https://wiki.openraildata.com/index.php?title=CIF_Codes#Timing_Load </remarks>
        [FieldFixedLength(4)]
        [FieldTrim(TrimMode.Right)]
        public string TimingLoadType { get; set; }
        /// <summary>
        /// Planned speed of service
        /// </summary>
        /// <remarks>Length 3, Position 38-40</remarks>
        [FieldFixedLength(3)]
        [FieldTrim(TrimMode.Right)]
        public string Speed { get; set; }
        /// <summary>
        /// How the train is powered detail
        /// </summary>
        /// <remarks>Length 3, Position 41-46
        /// For values: https://wiki.openraildata.com/index.php?title=CIF_Codes#Operating_Characteristics </remarks>
        [FieldFixedLength(6)]
        [FieldTrim(TrimMode.Right)]
        public string OperatingCharacteristics { get; set; }
        /// <summary>
        /// Seating classes available on service
        /// </summary>
        /// <remarks>Length 1, Position 47-47
        /// Blank or B - First and standard
        /// S - Standard class only.</remarks>
        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Right)]
        [FieldNullValue(ServiceClass.B)]
        public ServiceClass SeatClass { get; set; }
        /// <summary>
        /// Sleeper classes available on service
        /// </summary>
        /// <remarks>Length 1, Position 48-48
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
        /// <remarks>Length 1, Position 49-49
        /// Values: ARSE </remarks>
        [FieldFixedLength(1)]
        [FieldNullValue(ReservationIndicator.None)]
        public ReservationIndicator ReservationIndicator { get; set; }
        /// <summary>
        /// Connect Indicator - NOT USED
        /// </summary>
        /// <remarks>Length 1, Position 50-50</remarks>
        [FieldFixedLength(1)]
        [FieldTrim(TrimMode.Right)]
        public string ConnectIndicator { get; set; }
        /// <summary>
        /// Available catering
        /// </summary>
        /// <remarks>Length 4, Position 51-54
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
        /// <remarks>Length 4, Position 55-58
        /// E - Eurostar</remarks>
        [FieldFixedLength(4)]
        [FieldTrim(TrimMode.Right)]
        public string Branding { get; set; }
        /// <summary>
        /// Traction Class
        /// </summary>
        /// <remarks>Length 4, Position 59-62</remarks>
        [FieldFixedLength(4)]
        [FieldTrim(TrimMode.Right)]
        public string TractionClass { get; set; }
        /// <summary>
        /// UIC code
        /// </summary>
        /// <remarks>Length 5, Position 63-67
        /// Only populated for trains travelling to/from Europe via the Channel Tunnel, otherwise blank.</remarks>
        [FieldFixedLength(5)]
        [FieldTrim(TrimMode.Right)]
        public string Uic { get; set; }
        /// <summary>
        /// Retail Service ID
        /// </summary>
        /// <remarks>Length 8, Position 68-75.  Id used in NRS</remarks>
        [FieldFixedLength(8)]
        [FieldTrim(TrimMode.Right)]
        public string RetailServiceId { get; set; }
        /// <summary>
        /// Spare - NOT USED
        /// </summary>
        /// <remarks>Length 5, Position 76-80</remarks> 
        [FieldFixedLength(5)]
        [FieldTrim(TrimMode.Right)]
        public string Spare { get; set; }
    }
}
