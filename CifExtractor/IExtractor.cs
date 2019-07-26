using System.IO;

namespace CifExtractor
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