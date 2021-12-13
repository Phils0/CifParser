using CifParser.Records;
using Xunit;

namespace CifParserTest.Records
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
            return (Trailer) ParserTest.ParseRecords(_records)[0];
        }

        [Fact]
        public void TypePropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("ZZ", record.Type);
        }
    }
}
