using System.Collections.Generic;

namespace CifParser
{
    /// <summary>
    /// Interface to parse a CIF file
    /// </summary>
    public interface ICifParser
    {
        /// <summary>
        /// Read a CIF file
        /// </summary>
        /// <returns>An enuumeration of CIF records</returns>
        /// <remarks>Records are streamed as the file is read.  As one line is read the record is returned.</remarks>
        IEnumerable<IRecord> Read();
    }
}