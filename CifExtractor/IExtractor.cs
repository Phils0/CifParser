using System.IO;

namespace CifExtractor
{
    public interface IExtractor
    {
        TextReader ExtractCif(string file);
    }
}