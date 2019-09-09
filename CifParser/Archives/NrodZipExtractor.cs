using System.IO;
using System.IO.Compression;

namespace CifParser.Archives
{
    internal class NrodZipExtractor : IExtractor
    {
        public IArchive Archive { get; }
        
        internal NrodZipExtractor(IArchive archive)
        {
            Archive = archive;
        }
        
        public TextReader ExtractCif()
        {           
            var fileStream = File.OpenRead(Archive.FullName);
            var decompressionStream = new GZipStream(fileStream, CompressionMode.Decompress);
            return new StreamReader(decompressionStream);
        }
    }
}