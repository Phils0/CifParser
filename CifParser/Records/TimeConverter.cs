using FileHelpers;
using System;
using System.Globalization;

namespace CifParser.Records
{
    internal abstract class TimeConverter : ConverterBase
    {
        private static IFormatProvider Culture = CultureInfo.CurrentCulture;

        private string NullValue;

        protected TimeConverter(string nullValue)
        {
            NullValue = nullValue;
        }

        public override object? StringToField(string source)
        {
            if (string.Equals(NullValue, source))
                return null;

            return ParseTime(source);
        }

        public override string FieldToString(object @from)
        {
            
            if (@from is TimeSpan timeSpan)
            {
                
                var time = timeSpan.ToString("hhmm", Culture);
                if (timeSpan.Seconds == 30)
                    time += "H";
                return time;
            }

            if (@from == null)
            {
                return NullValue;
            }

            throw new InvalidOperationException($"Invalid record type {@from.GetType()}");
        }

        protected virtual TimeSpan ParseTime(string source)
        {
            return TimeSpan.ParseExact(source, "hhmm", Culture);
        }
    }

    internal class PublicTimeConverter : TimeConverter
    {
        public PublicTimeConverter() : base("0000")
        {
        }
    }

    internal class WorkingTimeConverter : TimeConverter
    {
        private readonly static TimeSpan ThirtySeconds = new TimeSpan(0, 0, 30);

        public WorkingTimeConverter() : base("     ")  // 5 spaces
        {
        }

        protected override TimeSpan ParseTime(string source)
        {
            var hrsMins = base.ParseTime(source.Substring(0, 4));
            return IsHalfMinute(source) ?
                hrsMins.Add(ThirtySeconds) :
                hrsMins;
        }

        private bool IsHalfMinute(string source)
        {
            return source.EndsWith("H");
        }
    }
}
