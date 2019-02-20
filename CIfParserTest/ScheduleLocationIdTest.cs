using System;
using System.Collections.Generic;
using CifParser;
using CifParser.Records;
using Xunit;

namespace CifParserTest
{
    public class ScheduleLocationIdTest
    {
        private static readonly ScheduleId TestSchedule = new ScheduleId("A12345", new DateTime(2019, 1, 2), StpIndicator.P, RecordAction.Create);
        
        private readonly ScheduleLocationId _test= new ScheduleLocationId(TestSchedule, "SURBITN", 1);

        [Fact]
        public void ToStringOutputsProperties()
        {
            Assert.Equal("SURBITN A12345-20190102:P-Create", _test.ToString());
        }
        
        public static IEnumerable<object[]> NotEqualLocations
        {
            get
            {
                yield return new object[] { new ScheduleLocationId( TestSchedule, "SURBITN", 2) };
                yield return new object[] { new ScheduleLocationId( TestSchedule, "WATRLOO", 1) };
                yield return new object[] { new ScheduleLocationId( new ScheduleId("A12345", new DateTime(2019, 1, 2), StpIndicator.P, RecordAction.Update), "SURBITN", 1) };
            }
        }
        
        [Fact]
        public void IsEqual()
        {     
            var id = new ScheduleLocationId(TestSchedule, "SURBITN", 1);
            Assert.True(_test.Equals(id));
        }
        
        [Fact]
        public void IsEqualToItself()
        {            
            Assert.True(_test.Equals(_test));
        }
        
        [Theory]
        [MemberData(nameof(NotEqualLocations))]
        public void IsNotEqual(ScheduleLocationId id)
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
            var id = new ScheduleLocationId(TestSchedule, "SURBITN", 1);
            Assert.Equal(_test.GetHashCode(), id.GetHashCode());
        }
                
        [Theory]
        [MemberData(nameof(NotEqualLocations))]
        public void HasDifferentHashcode(ScheduleLocationId id)
        {            
            Assert.NotEqual(_test.GetHashCode(), id.GetHashCode());
        }
    }
}