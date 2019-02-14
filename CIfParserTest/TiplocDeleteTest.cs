using CifParser;
using CifParser.Records;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace CifParserTest
{
    public class TiplocDeleteTest
    {

        private TiplocDelete ParseDeleteRecord()
        {
            var newRecord = @"TDLNDRBES                                                                       
";

            return ParserTest.ParseRecords(newRecord)[0] as TiplocDelete;
        }

        [Fact]
        public void ActionPropertySet()
        {
            var record = ParseDeleteRecord();
            Assert.Equal(RecordAction.Delete, record.Action);
        }

        [Fact]
        public void TiplocCodePropertySet()
        {
            var record = ParseDeleteRecord();
            Assert.Equal("LNDRBES", record.Code);
        }
        
        [Fact]
        public void ToStringReturnsTiploc()
        {
            var location = ParseDeleteRecord();
            Assert.Equal("LNDRBES Delete", location.ToString());
        }

    }
}
