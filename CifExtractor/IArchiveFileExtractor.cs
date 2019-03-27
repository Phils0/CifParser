using System.IO;

namespace CifExtractor
{
    public interface IArchiveFileExtractor
    {
        TextReader ExtractFile(string file, string extension);
    }
}