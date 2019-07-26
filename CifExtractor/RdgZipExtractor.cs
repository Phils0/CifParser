using System;
using System.IO;
using System.IO.Compression;
using Serilog;

namespace CifExtractor
{
    public class RdgZipExtractor : IExtractor, IArchiveFileExtractor
    {
        public const string CifExtension = ".MCA";
        public const string StationExtension = ".MSN";
        
        private readonly ILogger _logger;

        public IArchive Archive { get; }
        
        public RdgZipExtractor(IArchive archive, ILogger logger)
        {
            Archive = archive;
            _logger = logger;
        }
        
        public TextReader ExtractCif()
        {           
            return ExtractFile(CifExtension);
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

            return null;
        }
    }
}