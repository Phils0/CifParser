using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Serilog;

namespace CifParser.Archives
{
    internal class NrodZipExtractor : IExtractor
    {
        public IArchive Archive { get; }

        private readonly ILogger _logger;
        
        internal NrodZipExtractor(IArchive archive, ILogger logger)
        {
            Archive = archive;
            _logger = logger;
        }
        
        public TextReader ExtractCif()
        {           
            var fileStream = File.OpenRead(Archive.FullName);
            var decompressionStream = new GZipStream(fileStream, CompressionMode.Decompress);
            return new StreamReader(decompressionStream);
        }

        /// <summary>
        /// Standard implementation 
        /// </summary>
        /// <returns>Consolidated schedules</returns>
        public IEnumerable<IRecord> ParseCif()
        {
            var factory = new ConsolidatorFactory(_logger);
            var parser = factory.CreateParser();
            return parser.Read(ExtractCif());
        }
    }
}