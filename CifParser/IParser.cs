using System.Collections.Generic;
using System.IO;
using CifParser.Records;

namespace CifParser
{
    /// <summary>
    /// Interface to parse a CIF file
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Read the CIF file
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>An enuumeration of CIF records</returns>
        /// <remarks>Records are streamed as the file is read.  As one line is read the record is returned.</remarks>
        IEnumerable<ICifRecord> Read(TextReader reader);
        /// <summary>
        /// Read the CIF file
        /// </summary>
        /// <param name="file">CIF File path</param>
        /// <returns></returns>
        IEnumerable<ICifRecord> Read(string file);
    }
}