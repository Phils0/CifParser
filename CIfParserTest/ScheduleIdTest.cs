using System;
using System.Collections.Generic;
using CifParser;
using CifParser.Records;
using Xunit;

namespace CifParserTest
{
    public class ScheduleIdTest
    {
        private readonly ScheduleId _test = new ScheduleId("A12345", new DateTime(2019, 1, 2), StpIndicator.P, RecordAction.Create);

        [Fact]
        public void ToStringOutputsProperties()
        {
            Assert.Equal("A12345-20190102:P-Create", _test.ToString());
        }
        
        public static IEnumerable<object[]> NotEqualSchedules
        {
            get
            {
                yield return new object[] { new ScheduleId("A12345", new DateTime(2019, 1, 2), StpIndicator.P, RecordAction.Delete) };
                yield return new object[] { new ScheduleId("A12345", new DateTime(2019, 1, 2), StpIndicator.O, RecordAction.Create) };
                yield return new object[] { new ScheduleId("Z98765", new DateTime(2019, 1, 2), StpIndicator.P, RecordAction.Create) };
                yield return new object[] { new ScheduleId("A12345", new DateTime(2019, 1, 3), StpIndicator.P, RecordAction.Create) };
                yield return new object[] { new ScheduleId("", new DateTime(2019, 1, 2), StpIndicator.P, RecordAction.Delete) };
            }
        }
        
        [Fact]
        public void IsEqual()
        {     
            var id = new ScheduleId("A12345", new DateTime(2019, 1, 2), StpIndicator.P, RecordAction.Create);
            Assert.True(_test.Equals(id));
        }
        
        [Fact]
        public void IsEqualToItself()
        {            
            Assert.True(_test.Equals(_test));
        }
        
        [Theory]
        [MemberData(nameof(NotEqualSchedules))]
        public void IsNotEqual(ScheduleId id)
        {            
            Assert.False(_test.Equals(id));
        }
        
        [Fact]
        public void IsNotEqualToNull()
        {            
            Assert.False(_test.Equals(null));
        }
        
        [Fact]
        public void HasSameHashcode()
        {     
            var id = new ScheduleId("A12345", new DateTime(2019, 1, 2), StpIndicator.P, RecordAction.Create);
            Assert.Equal(_test.GetHashCode(), id.GetHashCode());
        }
                
        [Theory]
        [MemberData(nameof(NotEqualSchedules))]
        public void HasDifferentHashcode(ScheduleId id)
        {            
            Assert.NotEqual(_test.GetHashCode(), id.GetHashCode());
        }
    }
}