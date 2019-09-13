using System.Collections.Generic;
using System.IO;

namespace CifParser.Archives
{
    public interface ICifExtractor
    {
        /// <summary>
        /// Archive
        /// </summary>
        IArchive Archive { get; }
        
        /// <summary>
        /// Extracts the CIF file
        /// </summary>
        /// <returns></returns>
        TextReader Extract();
    }
}