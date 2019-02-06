using System.Collections.Generic;
using System.IO;
using CifParser.Records;

namespace CifParser
{
    public interface IParser
    {
        IEnumerable<ICifRecord> Read(TextReader reader);
    }
}