using FileHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CifParser.Records
{
    /// <summary>
    /// Trailer Record: ZZ
    /// </summary>
    [FixedLengthRecord(FixedMode.AllowLessChars)]
    public class Trailer : ICifRecord
    {
        /// <summary>
        ///Record type 
        /// </summary>
        /// <remarks>Length 2, Position 1-2</remarks>
        [FieldFixedLength(2)]
        public string Type { get; set; }
        /// <summary>
        /// Spare
        /// </summary>
        /// <remarks>NOT USED Length 78, Position 3-80</remarks> 
        [FieldFixedLength(78)]
        [FieldTrim(TrimMode.Right)]
        [FieldOptional]
        public string Spare { get; set; }
    }
}
