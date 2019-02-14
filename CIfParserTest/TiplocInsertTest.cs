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
        private TiplocInsertAmend ParseInsertRecord(string record = @"TISURBITN00557100NSURBITON                  87171   0SURSURBITON")
        {
            return ParserTest.ParseRecords(record)[0] as TiplocInsertAmend;
        }

        [Theory]
        [InlineData(@"TISURBITN00557100NSURBITON                  87171   0SURSURBITON", "557100 SUR SURBITN Create")]
        [InlineData(@"TISURBITN00557100NSURBITON                  87171   0   SURBITON", "557100  SURBITN Create")]
        public void ToStringReturnsNlcCrsTiploc(string record, string expected)
        {
            var location = ParseInsertRecord(record);
            Assert.Equal(expected, location.ToString());
        }

        [Fact]
        public void ActionPropertySet()
        {
            var record = ParseInsertRecord();
            Assert.Equal(RecordAction.Create, record.Action);
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
