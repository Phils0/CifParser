using CifParser.Records;
using FileHelpers;
using System.Collections.Generic;
using System.IO;

namespace CifParser
{
    public class Parser : IParser
    {
        public IEnumerable<ICifRecord> Read(TextReader reader)
        {
            var engine = RecordEngineFactory.Create();

            engine.BeginReadStream(reader);
            foreach (ICifRecord record in engine)
            {
                yield return record;
            }
        }
    }
}
