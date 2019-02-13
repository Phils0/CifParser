using FileHelpers;

namespace CifParser.Records
{
    /// <summary>
    /// TIPLOC Record: T[IAD]
    /// I: Insert, A: Amend, D: Delete 
    /// </summary>
    public class Tiploc : ICifRecord
    {
        /// <summary>
        /// CRUD record type 
        /// </summary>
        /// <remarks>Length 2, Position 1-2</remarks>
        [FieldFixedLength(2)]
        [FieldConverter(typeof(TiplocRecordConverter))]
        public RecordAction Action { get; set; }
        /// <summary>
        /// TIPLOC code
        /// </summary>
        /// <remarks>Length 7, Position 3-9</remarks> 
        [FieldFixedLength(7)]
        [FieldTrim(TrimMode.Right)]
        public string Code { get; set; }
    }

    /// <summary>
    /// TIPLOC Insert Record: TI
    /// </summary>
    [FixedLengthRecord(FixedMode.AllowLessChars)]
    public class TiplocInsertAmend : Tiploc
    {
        /// <summary>
        /// Capitals - NO LONGER USED
        /// </summary>
        /// <remarks>Length 2, Position 10-11</remarks> 
        [FieldFixedLength(2)]
        public string Capitals { get; set; }
        /// <summary>
        /// National Location code - NLC
        /// </summary>
        /// <remarks>Length 6, Position 12-17</remarks> 
        [FieldFixedLength(6)]
        public string Nalco { get; set; }
        /// <summary>
        /// National Location code check character
        /// </summary>
        /// <remarks>Length 1, Position 18-18</remarks> 
        [FieldFixedLength(1)]
        public string NalcoCheckCharacter { get; set; }
        /// <summary>
        /// TIPLOC description
        /// </summary>
        /// <remarks>Length 26, Position 19-44</remarks> 
        [FieldFixedLength(26)]
        [FieldTrim(TrimMode.Right)]
        public string Description { get; set; }
        /// <summary>
        /// Station numbers - STANOX Location code
        /// </summary>
        /// <remarks>Length 5, Position 45-49</remarks> 
        [FieldFixedLength(5)]
        public string Stanox { get; set; }
        /// <summary>
        /// Post Office code - NO LONGER USED
        /// </summary>
        /// <remarks>Length 4, Position 50-53</remarks> 
        [FieldFixedLength(4)]
        [FieldTrim(TrimMode.Left)]
        public string PostOfficeCode { get; set; }
        /// <summary>
        /// Three Letter Code - CRS
        /// </summary>
        /// <remarks>Length 3, Position 54-56</remarks> 
        [FieldFixedLength(3)]
        [FieldTrim(TrimMode.Right)]
        public string ThreeLetterCode { get; set; }
        /// <summary>
        /// NLC Description
        /// </summary>
        /// <remarks>Length 16, Position 57-72</remarks> 
        [FieldFixedLength(16)]
        [FieldTrim(TrimMode.Right)]
        public string NlcDescription { get; set; }
        /// <summary>
        /// Spare - NOT USED
        /// </summary>
        /// <remarks>Length 8, Position 73-80</remarks> 
        [FieldFixedLength(8)]
        [FieldTrim(TrimMode.Right)]
        [FieldOptional]
        public string Spare { get; set; }
    }

    /// <summary>
    /// TIPLOC Delete Record: TD
    /// </summary>
    [FixedLengthRecord(FixedMode.AllowLessChars)]
    public class TiplocDelete : Tiploc
    {
        /// <summary>
        /// Spare - NOT USED
        /// </summary>
        /// <remarks>Length 71, Position 9-80</remarks> 
        [FieldFixedLength(71)]
        [FieldTrim(TrimMode.Right)]
        [FieldOptional]
        public string Spare { get; set; }
    }
}
