using System;
using System.IO;
using CifParser.Archives;
using NSubstitute;
using Serilog;
using Xunit;

namespace CifParserTest.Archives
{
    public class ArchiveTest
    {
        [Theory]
        [InlineData("ttis144.zip")]
        [InlineData("RJTTF293.zip")]
        [InlineData("toc-update-tue.CIF.gz")]
        public void CreatesExtractor(string file)
        {
            var archive = CreateArchive(file);
            Assert.NotNull(archive.CreateExtractor());
        }
        
        [Theory]
        [InlineData("ttis144.zip")]
        [InlineData("RJTTF293.zip")]
        public void CreatesFileExtractor(string file)
        {
            var archive = CreateArchive(file);
            Assert.NotNull(archive.CreateExtractor());
        }

        [Fact]
        public void CreatesFileExtractorThrowsExceptionWhenNotRdgArchive()
        {
            var archive = CreateArchive("toc-update-tue.CIF.gz");
            Assert.Throws<InvalidOperationException>(() => archive.CreateFileExtractor());
        }
        
        [Theory]
        [InlineData("ttis144.zip", true)]
        [InlineData("RJTTF293.zip", true)]
        [InlineData("toc-update-tue.CIF.gz", false)]
        public void IsRdgFile(string file, bool expected)
        {
            var archive = CreateArchive(file);
            Assert.Equal(expected, archive.IsRdgZip);
        }

        private Archive CreateArchive(string file)
        {
            return new Archive(new FileInfo(file), Substitute.For<ILogger>());
        }

        [Theory]
        [InlineData("ttis144.zip", true)]
        [InlineData("RJTTF293.zip", false)]
        [InlineData("toc-update-tue.CIF.gz", false)]
        public void IsTtisFile(string file, bool expected)
        {
            var archive = CreateArchive(file);
            Assert.Equal(expected, archive.IsTtisZip);
        }
        
        [Theory]
        [InlineData("ttis144.zip", false)]
        [InlineData("RJTTF293.zip", true)]
        [InlineData("toc-update-tue.CIF.gz", false)]
        public void IsDtdFile(string file, bool expected)
        {
            var archive = CreateArchive(file);
            Assert.Equal(expected, archive.IsDtdZip);
        }
    }
}