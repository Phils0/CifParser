using System;
using FileHelpers;

namespace CifParser.RdgRecords
{
    [FixedLengthRecord]
    public class StationAlias  : IRecord
    {
        /// <summary>
        /// Record type 
        /// </summary>
        /// <remarks>Length 1, Position 1-1
        /// L records</remarks>
        [FieldFixedLength(1)]
        public string RecordType { get; set; } = null!;
        
        /// <summary>
        /// Name
        /// </summary>
        /// <remarks>Length 34, Position 2-35
        /// Includes the reserved set of empty spaces prior to the name
        /// that are trimmed away</remarks> 
        [FieldFixedLength(34)]
        [FieldTrim(TrimMode.Both)]
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Alias
        /// </summary>
        /// <remarks>Length 47, Position 36-83
        /// Includes the 2 reserved sets of empty spaces around the alias
        /// that are trimmed away</remarks> 
        [FieldFixedLength(47)]
        [FieldTrim(TrimMode.Both)]
        public string Alias { get; set; } = string.Empty;
    }
}