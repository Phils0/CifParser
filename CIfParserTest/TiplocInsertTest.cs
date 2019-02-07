using CifParser;
using CifParser.Records;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace CifParserTest
{
    public class TiplocInsertTest
    {

        private TiplocInsert ParseInsertRecord()
        {
            var newRecord = @"TISURBITN00557100NSURBITON                  87171   0SURSURBITON
";

            return ParserTest.ParseRecords(newRecord)[0] as TiplocInsert;
        }

        [Fact]
        public void ActionPropertySet()
        {
            var record = ParseInsertRecord();
            Assert.Equal(RecordAction.Insert, record.Action);
        }

        [Fact]
        public void TiplocCodePropertySet()
        {
            var record = ParseInsertRecord();
            Assert.Equal("SURBITN", record.Code);
        }

        [Fact]
        public void NlcPropertySet()
        {
            var record = ParseInsertRecord();
            Assert.Equal("557100", record.Nalco);
        }

        [Fact]
        public void NlcDescriptionPropertySet()
        {
            var record = ParseInsertRecord();
            Assert.Equal("SURBITON", record.NlcDescription);
        }

        [Fact]
        public void DescriptionPropertySet()
        {
            var record = ParseInsertRecord();
            Assert.Equal("SURBITON", record.Description);
        }

        [Fact]
        public void StanoxPropertySet()
        {
            var record = ParseInsertRecord();
            Assert.Equal("87171", record.Stanox);
        }

        [Fact]
        public void CrsPropertySet()
        {
            var record = ParseInsertRecord();
            Assert.Equal("SUR", record.ThreeLetterCode);
        }
    }
}
