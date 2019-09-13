using System;
using System.IO;
using CifParser.Archives;
using NSubstitute;
using Serilog;
using Xunit;

namespace CifParserTest.Archives
{
    public class ArchiveParserTest
    {
        [Theory]
        [InlineData("ttis144.zip")]
        [InlineData("RJTTF293.zip")]
        [InlineData("toc-update-tue.CIF.gz")]
        public void ParseCifRecords(string file)
        {
            var parser = CreateParser(file);
            Assert.NotEmpty(parser.ParseCif());
        }
        
        private IArchiveParser CreateParser(string file)
        {
            var localFile = Path.Combine(".", "Data", file);
            var archive = new Archive(new FileInfo(localFile), Substitute.For<ILogger>());
            return archive.CreateParser();
        }
        
        [Theory]
        [InlineData("ttis144.zip")]
        [InlineData("RJTTF293.zip")]
        public void CreatesArchiveParser(string file)
        {
            var parser = CreateParser(file);
            Assert.NotEmpty(parser.ParseFile(RdgZipExtractor.StationExtension));
        }
        
        [Fact]
        public void CreatesFileExtractorThrowsExceptionWhenNotRdgArchive()
        {
            var parser = CreateParser("toc-update-tue.CIF.gz");
            Assert.Throws<InvalidOperationException>(() => parser.ParseFile(RdgZipExtractor.StationExtension));
        }
    }
}