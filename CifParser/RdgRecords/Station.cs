using FileHelpers;

namespace CifParser.RdgRecords
{
    public enum InterchangeStatus
    {
        NotAnInterchange = 0,
        Minor = 1,
        Normal = 2,
        Main = 3,
        SubsidiaryLocation = 9
    }
    
    /// <summary>
    /// Master Station Name file: MSN
    /// A record
    /// </summary>
    [FixedLengthRecord]
    [IgnoreFirst(6)]
    public class Station : IRecord
    {
        /// <summary>
        /// Record type 
        /// </summary>
        /// <remarks>Length 1, Position 1-1
        /// Only interested in A records</remarks>
        [FieldFixedLength(1)]
        public string RecordType { get; set; }
        
        /// <summary>
        /// Name
        /// </summary>
        /// <remarks>Length 34, Position 2-35
        /// Includes the 2 reserved sets of empty spaces around the name
        /// that are trimmed away</remarks> 
        [FieldFixedLength(34)]
        [FieldTrim(TrimMode.Both)]
        public string Name { get; set; }
        
        /// <summary>
        /// Interchange Status
        /// </summary>
        /// <remarks>Length 1, Position 36-36
        /// 0 - not an interchange
        /// 1, 2, 3 Higher number more important interchange
        /// 9 a subsidiary TIPLOC at a station which has more than one TIPLOC.
        /// Stations which have more than one TIPLOC always have the same principal 3-Letter Code.</remarks>
        [FieldFixedLength(1)]
        public InterchangeStatus InterchangeStatus { get; set; }
        
        /// <summary>
        /// TIPLOC code
        /// </summary>
        /// <remarks>Length 7, Position 37-43</remarks> 
        [FieldFixedLength(7)]
        [FieldTrim(TrimMode.Right)]
        public string Tiploc { get; set; }
        

        /// <summary>
        /// Three Letter Code - CRS
        /// </summary>
        /// <remarks>Length 3, Position 44-49
        /// Includes the reserved set of empty spaces to the right
        /// that are trimmed away
        /// Normally the MinorThreeLetterCode and the ThreeLetterCode
        /// are the same.  Occasionally they vary
        /// Should use the ThreeLetterCode as the one in public and then map to all TIPLOCs</remarks> 
        [FieldFixedLength(6)]
        [FieldTrim(TrimMode.Right)]
        public string SubsidiaryThreeLetterCode { get; set; }

        /// <summary>
        /// Three Letter Code - CRS
        /// </summary>
        /// <remarks>Length 3, Position 50-52</remarks> 
        [FieldFixedLength(3)]
        public string ThreeLetterCode { get; set; }
        
        /// <summary>
        /// Ordnance Survey East
        /// </summary>
        /// <remarks>Length 3, Position 53-57
        /// Format is ‘1nnnn’ where nnnn is the distance in 0.1 km units.</remarks> 
        [FieldFixedLength(5)]
        public int East { get; set; }
        
        /// <summary>
        /// Ordnance Survey East
        /// </summary>
        /// <remarks>Length 1, Position 58-58
        /// E - estimate</remarks> 
        [FieldFixedLength(1)]
        [FieldConverter(ConverterKind.Boolean, "E", " ")]
        [FieldNullValue(false)]
        public bool PositionIsEstimated { get; set; }
        
        /// <summary>
        /// Ordnance Survey Northings
        /// </summary>
        /// <remarks>Length 5, Position 59-63
        /// Format is ‘6nnnn’ where nnnn is the distance in 0.1 km units.</remarks> 
        [FieldFixedLength(5)]
        public int North { get; set; }

        /// <summary>
        /// Minimum Change time
        /// </summary>
        /// <remarks>Length 1, Position 64-65
        /// in minutes </remarks>
        [FieldFixedLength(2)]
        public byte MinimumChangeTime { get; set; }      
        
        /// <summary>
        /// Spare - NOT USED
        /// </summary>
        /// <remarks>Length 17, Position 66-83</remarks> 
        [FieldFixedLength(17)]
        [FieldTrim(TrimMode.Right)]
        [FieldOptional]
        public string Spare { get; set; }
        
        public override string ToString()
        {
            return ThreeLetterCode == SubsidiaryThreeLetterCode ?
                $"{ThreeLetterCode}-{Tiploc} {Name}" :
                $"{ThreeLetterCode}({SubsidiaryThreeLetterCode})-{Tiploc} {Name}";
        }
    }
}
