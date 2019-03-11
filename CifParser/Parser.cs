using CifParser.Records;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileHelpers;

namespace CifParser
{
    /// <summary>
    /// Parses the file, creating one record for each line of the file
    /// </summary>
    public class Parser : IParser
    {
        private readonly MultiRecordEngine _engine;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="engine">File reader engine</param>
        internal Parser(MultiRecordEngine engine)
        {
            _engine = engine;
        }
                
        public IEnumerable<ICifRecord> Read(TextReader reader)
        {
            _engine.BeginReadStream(reader);
            var objects = new SingleCallEnumerator(_engine);
            return objects.Cast<ICifRecord>();
        }

        public IEnumerable<ICifRecord> Read(string file)
        {
            if (!File.Exists(file))
                throw new ArgumentException($"File does not exist: {file}");

            var reader = File.OpenText(file);
            return Read(reader);
            ;
        }
    }
}
