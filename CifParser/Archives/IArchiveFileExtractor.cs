using System.IO;

namespace CifParser.Archives
{
    /// <summary>
    /// Returns files contained in an archive
    /// </summary>
    public interface IArchiveFileExtractor
    {
        /// <summary>
        /// Archive
        /// </summary>
        IArchive Archive { get; }
        
        /// <summary>
        /// Extract a text file from the archive
        /// </summary>
        /// <param name="extension">file extension</param>
        /// <returns>A TextReader referencing the file</returns>
        TextReader ExtractFile(string extension);
    }
}