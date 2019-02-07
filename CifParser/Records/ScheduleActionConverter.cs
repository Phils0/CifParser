using FileHelpers;
using System;

namespace CifParser.Records
{
    internal class ScheduleActionConverter : ConverterBase
    {
        public override object StringToField(string source)
        {
            switch(source)
            {
                case "N":
                    return RecordAction.Insert;
                case "D":
                    return RecordAction.Delete;
                case "R":
                    return RecordAction.Amend;
                default:
                    throw new InvalidOperationException($"Invalid record type {source}");
            }
        }
    }
}
