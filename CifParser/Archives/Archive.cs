using System;
using System.IO;
using Serilog;

namespace CifParser.Archives
{
    public interface IArchive
    {
        /// <summary>
        /// Archive File
        /// </summary>
        FileInfo File { get; }
        /// <summary>
        /// File path of Archive 
        /// </summary>
        string FullName { get; }
        /// <summary>
        /// Is RDG Archive
        /// </summary>
        bool IsRdgZip { get; }
        /// <summary>
        /// Is the TTIS version of the RDG archive 
        /// </summary>
        bool IsTtisZip { get; }
        /// <summary>
        /// Is the DTD version of the RDG archive 
        /// </summary>
        bool IsDtdZip { get; }

        /// <summary>
        /// Creates a extractor to get the CIF file from the archive
        /// </summary>
        /// <returns></returns>
        IExtractor CreateExtractor();
        
        /// <summary>
        /// Creates a extractor to get the CIF file from the archive
        /// </summary>
        /// <returns></returns>
        IArchiveFileExtractor CreateFileExtractor();
    }
    
    public class Archive : IArchive
    {
        private readonly ILogger _logger;

        public Archive(string archiveFile, ILogger logger) :
            this(new FileInfo(archiveFile), logger)
        {
        }
        
        public Archive(FileInfo archiveFile, ILogger logger)
        {
            File = archiveFile;
            _logger = logger;
        }
        
        public FileInfo File { get; }

        public bool IsRdgZip => IsDtdZip || IsTtisZip;

        public bool IsTtisZip => FullName.Contains("ttis");    // Initially just simple file name check
        
        public bool IsDtdZip => FullName.Contains("RJTTF");    // Initially just simple file name check
        public IExtractor CreateExtractor()
        {
            return IsRdgZip ? (IExtractor) new RdgZipExtractor(this,  _logger) : new NrodZipExtractor(this);
        }

        public IArchiveFileExtractor CreateFileExtractor()
        {
            if(!IsRdgZip)
                throw new InvalidOperationException($"{File.Name} is a Network Rail archive. It does not support IArchiveFileExtractor");

            return new RdgZipExtractor(this, _logger);
        }

        public string FullName => File.FullName;
        
        public override string ToString()
        {
            return $"{FullName}, IsRdgZip: {IsRdgZip}";
        }
    }
}