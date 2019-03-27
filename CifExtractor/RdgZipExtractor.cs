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

        public RdgZipExtractor(ILogger logger)
        {
            _logger = logger;
        }
        
        public TextReader ExtractCif(string file)
        {           
            return ExtractFile(file, CifExtension);
        }

        /// <summary>
        /// Extract from an RDG Timetable extract
        /// </summary>
        /// <param name="file">RDG timtable zip archive - ttisnnn.zip </param>
        /// <param name="extension">The file inside the archive to extract</param>
        /// <returns>A reader to read the file</returns>
        public TextReader ExtractFile(string file, string extension)
        {
            var archive = ZipFile.OpenRead(file);

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