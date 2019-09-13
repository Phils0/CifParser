using System.Collections.Generic;

namespace CifParser.Archives
{
    /// <summary>
    /// Parses an archive
    /// </summary>
    public interface IArchiveParser
    {
        /// <summary>
        /// Archive
        /// </summary>
        IArchive Archive { get; }
        
        /// <summary>
        /// Parses a CIF file from an archive
        /// </summary>
        /// <returns>A set of schedules</returns>
        IEnumerable<IRecord> ParseCif();
        
        /// <summary>
        /// Parses a non-CIF file from an archive
        /// </summary>
        /// <param name="extension">file extension</param>
        /// <returns>A set of records</returns>
        IEnumerable<IRecord> ParseFile(string extension);
    }
}