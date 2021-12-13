using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Serilog;

namespace CifParser.Archives
{
    public class RdgZipExtractor : ICifExtractor, ICifParser, IArchiveFileExtractor
    {
        public const string CifExtension = ".MCA";
        public const string StationExtension = ".MSN";
        
        private readonly ILogger _logger;
        private readonly IParserFactory _cifParserFactory;

        public IArchive Archive { get; }
        
        public RdgZipExtractor(IArchive archive, ILogger logger, IParserFactory? cifParserFactory = null)
        {
            Archive = archive;
            _logger = logger;
            _cifParserFactory = cifParserFactory ?? new ConsolidatorFactory(_logger);
        }
        
        public TextReader Extract()
        {           
            return ExtractFile(CifExtension);
        }

        public IEnumerable<IRecord> Read()
        {
            var parser = _cifParserFactory.CreateParser();
            return parser.Read(Extract());
        }

        /// <summary>
        /// Extract from an RDG Timetable extract
        /// </summary>
        /// <param name="file">RDG timtable zip archive - ttisnnn.zip </param>
        /// <param name="extension">The file inside the archive to extract</param>
        /// <returns>A reader to read the file</returns>
        public TextReader ExtractFile(string extension)
        {
            var archive = ZipFile.OpenRead(Archive.FullName);

            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (entry.FullName.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
                {
                    _logger.Information("Loading {file}", entry.FullName);
                    var s = entry.Open();
                    return new StreamReader(s);
                }
            }

            throw new FileNotFoundException($"{extension} not found in {Archive.FullName}");
        }
    }
}