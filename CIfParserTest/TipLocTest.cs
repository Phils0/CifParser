using CifParser;
using CifParser.Records;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace CifParserTest
{
    public class TiplocTest
    {

        private const string _records = 
@"TIPRNC84800932885DEDINBURGH SIGNAL 848      04305   0                           
TIPRNC88400932884CEDINBURGH SIGNAL 844      04310   0                           
TAWROX14 00732701NWROXHOVETON & WROXHAM SIG 48032   0                           
TDLNDRBES                                                                       
";

        [Fact]
        public void ReadInsertRecord()
        {
            ICifRecord[] records = ParseRecords();

            Assert.IsType<TiplocInsert>(records[0]);
        }

        public ICifRecord[] ParseRecords()
        {
            return ParserTest.ParseRecords(_records);
        }

        [Fact]
        public void ReadAmendRecord()
        {
            ICifRecord[] records = ParseRecords();

            Assert.IsType<TiplocAmend>(records[2]);
        }

        [Fact]
        public void ReadDeleteRecord()
        {
            ICifRecord[] records = ParseRecords();

            Assert.IsType<TiplocDelete>(records[3]);
        }
    }
}
