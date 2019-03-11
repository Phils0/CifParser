using CifParser;
using CifParser.Records;
using System;
using System.IO;
using System.Linq;
using NSubstitute;
using Serilog;
using Xunit;

namespace CifParserTest
{
    public class ParserTest
    {
        public static IRecord[] ParseRecords(string data)
        {
            var input = new StringReader(data);

            var factory = new CifParserFactory(Substitute.For<ILogger>());
            var parser = factory.CreateParser();

            var records = parser.Read(input).ToArray();
            return records;
        }

        [Fact]
        public void HandleInvalidRecord()
        {
            var invalidRecord =
@"HDTPS.UDFROC1.PD1901292901191927DFROC1MDFROC1LUA290119290120                    
XXINVALID                     
ZZ                                                                              
";
        IRecord[] records = ParseRecords(invalidRecord);

         Assert.Equal(2, records.Length);
        }

        [Fact]
        public void HandleEmptyLine()
        {
            var emptyLine =
@"HDTPS.UDFROC1.PD1901292901191927DFROC1MDFROC1LUA290119290120                    
                     
ZZ                                                                              
";
            IRecord[] records = ParseRecords(emptyLine);

            Assert.Equal(2, records.Length);
        }

        [Fact]
        public void HandleEmptyLineAtEnd()
        {
            var emptyLine =
@"HDTPS.UDFROC1.PD1901292901191927DFROC1MDFROC1LUA290119290120                    
                     
ZZ                                                                              

";
            IRecord[] records = ParseRecords(emptyLine);

            Assert.Equal(2, records.Length);
        }



        [Fact]
        public void HandleWhitespaceLine()
        {
            var whiteSpaceLine =
@"HDTPS.UDFROC1.PD1901292901191927DFROC1MDFROC1LUA290119290120                    
                                  
ZZ                                                                              
";
            IRecord[] records = ParseRecords(whiteSpaceLine);

            Assert.Equal(2, records.Length);
        }

        [Fact]
        public void CannotEnuerateTwice()
        {
            var records =
@"HDTPS.UDFROC1.PD1901292901191927DFROC1MDFROC1LUA290119290120                    
ZZ                                                                              
";
            var input = new StringReader(records);
            var factory = new CifParserFactory(Substitute.For<ILogger>());
            var parser = factory.CreateParser();

            var enumerable = parser.Read(input);

            var first = enumerable.ToArray();
            Assert.Throws<InvalidOperationException>(() =>  enumerable.ToArray());
        }
    }
}
