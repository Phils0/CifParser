using CifParser;
using CifParser.Records;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace CifParserTest
{
    public class TrailerTest
    {
        private const string _records =
@"ZZ                                                                              
";

        [Fact]
        public void ReadTrailerRecord()
        {
            var record = ParseRecord();
            Assert.NotNull(record);
        }

        private Trailer ParseRecord()
        {
            return ParserTest.ParseRecords(_records)[0] as Trailer;
        }

        [Fact]
        public void TypePropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("ZZ", record.Type);
        }
    }
}
