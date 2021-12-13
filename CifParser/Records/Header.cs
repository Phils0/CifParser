using FileHelpers;
using System;

namespace CifParser.Records
{
    public enum ExtractType
    {
        NotSet = -1,
        F, // Full
        U, // Update (delta)
    }

    /// <summary>
    /// Header Record: HD
    /// </summary>
    [FixedLengthRecord(FixedMode.AllowLessChars)]
    public class Header : IRecord
    {
        /// <summary>
        ///Record type 
        /// </summary>
        /// <remarks>Length 2, Position 1-2</remarks>
        [FieldFixedLength(2)]
        public string Type { get; set; } = null!;
        /// <summary>
        /// Mainframe Identifier
        /// </summary>
        /// <remarks>Length 20, Position 3-22
        /// Format: TPS.Uxxxxxx.PDyymmdd where 'xxxxxx' is the user identity</remarks> 
        [FieldFixedLength(20)]
        public string MainframeId { get; set; } = null!;
        /// <summary>
        /// When extract was run
        /// </summary>
        /// <remarks>Length 10, Position 23-32
        /// Format: ddMMyyHHmm </remarks> 
        [FieldFixedLength(10)]
        [FieldConverter(ConverterKind.Date, "ddMMyyHHmm")]
        public DateTime ExtractedAt { get; set; } = DateTime.MinValue;
        /// <summary>
        /// Current File Reference
        /// </summary>
        /// <remarks>Length 7, Position 33-40
        /// Format: xxxxxxa where 'xxxxxx' is the user identity</remarks> 
        [FieldFixedLength(7)]
        public string CurrentFileReference { get; set; } = null!;
        /// <summary>
        /// Last File Reference
        /// </summary>
        /// <remarks>Length 7, Position 41-47
        /// Format: xxxxxxa where 'xxxxxx' is the user identity</remarks> 
        [FieldFixedLength(7)]
        public string LastFileReference { get; set; } = null!;

        /// <summary>
        /// Extract type: F (Full) or U (Update delta)
        /// </summary>
        /// <remarks>Length 1, Position 48-48</remarks> 
        [FieldFixedLength(1)]
        public ExtractType ExtractType { get; set; } = ExtractType.NotSet;
        /// <summary>
        /// CIF version
        /// </summary>
        /// <remarks>Length 1, Position 49-49</remarks> 
        [FieldFixedLength(1)]
        public string Version { get; set; } = null!;
        /// <summary>
        /// Start date
        /// </summary>
        /// <remarks>Length 6, Position 50-55
        /// Format: ddMMyy </remarks> 
        [FieldFixedLength(6)]
        [FieldConverter(ConverterKind.Date, "ddMMyy")]
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        /// <summary>
        /// End date
        /// </summary>
        /// <remarks>Length 6, Position 56-61
        /// Format: ddMMyy </remarks> 
        [FieldFixedLength(6)]
        [FieldConverter(ConverterKind.Date, "ddMMyy")]
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        /// <summary>
        /// Spare - NOT USED
        /// </summary>
        /// <remarks>Length 20, Position 62-81</remarks> 
        [FieldFixedLength(20)]
        [FieldTrim(TrimMode.Right)]
        [FieldOptional]
        public string Spare { get; set; } = string.Empty;
        
        public override string ToString()
        {
            return $"{MainframeId}";
        }
    }
}
