using CifParser.Records;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CifParser
{
    /// <summary>
    /// Parses the file, creating one record for each line of the file
    /// </summary>
    public class Parser : IParser
    {
        private readonly RecordEngineFactory _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Serilog Logger</param>
        public Parser(ILogger logger)
        {
            _factory = new RecordEngineFactory(logger);
        }
        public IEnumerable<ICifRecord> Read(TextReader reader)
        {
            var enumerable = _factory.Create(reader);
            return enumerable.Cast<ICifRecord>();
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
