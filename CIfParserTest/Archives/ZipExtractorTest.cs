using System.IO;
using CifParser.Archives;
using NSubstitute;
using Serilog;
using Xunit;

namespace CifParserTest.Archives
{
    public class NrodZipExtractorTest
    {
        private static readonly string cifGzipFile =  Path.Combine(".", "Data", "toc-update-tue.CIF.gz");
               
        [Fact]
        public void CanReadCifFile()
        {
            var archive = new Archive(cifGzipFile, Substitute.For<ILogger>());
            var extractor = new NrodZipCifExtractor(archive, Substitute.For<ILogger>());

            using (var reader = extractor.Extract())
            {
                var first = reader.ReadLine();
                Assert.NotEmpty(first);
            }
        }
        
        [Fact]
        public void CanParseCifFile()
        {
            var archive = new Archive(cifGzipFile, Substitute.For<ILogger>());
            var extractor = new NrodZipCifExtractor(archive, Substitute.For<ILogger>());

            var records = extractor.Read();
            Assert.NotEmpty(records);
        }
    }
    
    public class RdgZipExtractorTest
    {
        [Theory]
        [InlineData("ttis144.zip")]
        [InlineData("RJTTF293.ZIP")]
        public void CanReadCifFile(string file)
        {
            var extractor = CreateExtractor(file);

            using (var reader = extractor.Extract())
            {
                var first = reader.ReadLine();
                Assert.NotEmpty(first);
            }
        }
        
        private RdgZipCifExtractor CreateExtractor(string file)
        {
            var localFile = Path.Combine(".", "Data", file);
            var archive = new Archive(new FileInfo(localFile), Substitute.For<ILogger>());
            return new RdgZipCifExtractor(archive,  Substitute.For<ILogger>());
        }
        
        [Theory]
        [InlineData("ttis144.zip")]
        [InlineData("RJTTF293.ZIP")]
        public void CanParseCifFile(string file)
        {
            var extractor = CreateExtractor(file);

            var records = extractor.Read();
            Assert.NotEmpty(records);
        }
       
        [Theory]
        [InlineData("ttis144.zip")]
        [InlineData("RJTTF293.ZIP")]
        public void CanReadStationFile(string file)
        {
            var extractor = CreateExtractor(file);

            using (var reader = extractor.ExtractFile(RdgZipCifExtractor.StationExtension))
            {
                var first = reader.ReadLine();
                Assert.NotNull(first);
            }
        }
    }
}