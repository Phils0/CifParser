using CifParser;
using CifParser.Records;
using System;
using System.IO;
using System.Linq;
using Xunit;

namespace CifParserTest
{
    public class TiplocAmendTest
    {

        private TiplocInsertAmend ParseAmendRecord(string record = @"TASURBITN00557100NSURBITON                  87171   0SURSURBITON")
        {
            return ParserTest.ParseRecords(record)[0] as TiplocInsertAmend;
        }

        [Theory]
        [InlineData(@"TASURBITN00557100NSURBITON                  87171   0SURSURBITON", "557100 SUR SURBITN Update")]
        [InlineData(@"TASURBITN00557100NSURBITON                  87171   0   SURBITON", "557100  SURBITN Update")]
        public void ToStringReturnsNlcCrsTiploc(string record, string expected)
        {
            var location = ParseAmendRecord(record);
            Assert.Equal(expected, location.ToString());
        }
                
        [Fact]
        public void ActionPropertySet()
        {
            var record = ParseAmendRecord();
            Assert.Equal(RecordAction.Update, record.Action);
        }

        [Fact]
        public void TiplocCodePropertySet()
        {
            var record = ParseAmendRecord();
            Assert.Equal("SURBITN", record.Code);
        }

        [Fact]
        public void NlcPropertySet()
        {
            var record = ParseAmendRecord();
            Assert.Equal("557100", record.Nalco);
        }

        [Fact]
        public void NlcDescriptionPropertySet()
        {
            var record = ParseAmendRecord();
            Assert.Equal("SURBITON", record.NlcDescription);
        }

        [Fact]
        public void DescriptionPropertySet()
        {
            var record = ParseAmendRecord();
            Assert.Equal("SURBITON", record.Description);
        }

        [Fact]
        public void StanoxPropertySet()
        {
            var record = ParseAmendRecord();
            Assert.Equal("87171", record.Stanox);
        }

        [Fact]
        public void CrsPropertySet()
        {
            var record = ParseAmendRecord();
            Assert.Equal("SUR", record.ThreeLetterCode);
        }
    }
}
