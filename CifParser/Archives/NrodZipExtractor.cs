using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Serilog;

namespace CifParser.Archives
{
    internal class NrodZipExtractor : ICifExtractor, ICifParser
    {
        public IArchive Archive { get; }

        private readonly ILogger _logger;
        private readonly IParserFactory _cifParserFactory;

        internal NrodZipExtractor(IArchive archive, ILogger logger, IParserFactory cifParserFactory = null)
        {
            Archive = archive;
            _logger = logger;
            _cifParserFactory = cifParserFactory ?? new ConsolidatorFactory(_logger);
        }
        
        public TextReader Extract()
        {           
            var fileStream = File.OpenRead(Archive.FullName);
            var decompressionStream = new GZipStream(fileStream, CompressionMode.Decompress);
            return new StreamReader(decompressionStream);
        }

        /// <summary>
        /// Standard implementation 
        /// </summary>
        /// <returns>Consolidated schedules</returns>
        public IEnumerable<IRecord> Read()
        {
            var parser = _cifParserFactory.CreateParser();
            return parser.Read(Extract());
        }
    }
}