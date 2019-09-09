using System.IO;

namespace CifParser.Archives
{
    public interface IExtractor
    {
        /// <summary>
        /// Archive
        /// </summary>
        IArchive Archive { get; }
        
        /// <summary>
        /// Extracts the CIF file
        /// </summary>
        /// <returns></returns>
        TextReader ExtractCif();
    }
}