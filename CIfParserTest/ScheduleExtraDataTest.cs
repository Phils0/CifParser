﻿using CifParser.Records;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CifParserTest
{
    public class ScheduleExtraDataTest
    {
        private const string _rdg =
@"BX         XCYXC122000
";

        private const string _networkRail =
@"BX         NTY                                                                  
";
        [Fact]
        public void ReadScheduleExtraDataRecord()
        {
            var record = ParseRecord();
            Assert.NotNull(record);
        }

        private ScheduleExtraData ParseRecord(string records = null)
        {
            records = records ?? _rdg;
            return ParserTest.ParseRecords(records)[0] as ScheduleExtraData;
        }

        [Fact]
        public void TypePropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("BX", record.Type);
        }


        [Fact]
        public void TocPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("XC", record.Toc);
        }

        [Fact]
        public void ApplicableTimetableCodePropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("Y", record.ApplicableTimetableCode);
        }

        [Fact]
        public void RetailServiceIdPropertySet()
        {
            var record = ParseRecord();
            Assert.Equal("XC122000", record.RetailServiceId);
        }

        [Fact]
        public void HandleNetworkRailFormat()
        {
            var record = ParseRecord(_networkRail);
            Assert.Equal("NT", record.Toc);
            Assert.Equal("", record.RetailServiceId);
        }
    }
}
