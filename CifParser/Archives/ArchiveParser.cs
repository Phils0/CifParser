using System;
using System.Collections.Generic;
using Serilog;

namespace CifParser.Archives
{
    public class ArchiveParser : IArchiveParser
    {
        private readonly ILogger _logger;
        public IArchive Archive { get; }

        public ArchiveParser(IArchive archive, ILogger logger)
        {
            _logger = logger;
            Archive = archive;
        }

        public IEnumerable<IRecord> ParseCif()
        {
            var extractor = Archive.CreateExtractor();
            return extractor.ParseCif();
        }

        public IEnumerable<IRecord> ParseFile(string extension)
        {
            var extractor = Archive.CreateFileExtractor();
            var parser = CreateParser();
            return parser.Read(extractor.ExtractFile(extension));

            IParser CreateParser()
            {
                if (!RdgZipExtractor.StationExtension.Equals(extension, StringComparison.InvariantCultureIgnoreCase))
                    throw new ArgumentException($"File type {extension} not supported");

                var stationParserFactory = new StationParserFactory(_logger);
                var ignoreLines = Archive.IsDtdZip
                    ? StationParserFactory.Dtd
                    : StationParserFactory.Ttis;
                return stationParserFactory.CreateParser(ignoreLines);
            }
        }
    }
}