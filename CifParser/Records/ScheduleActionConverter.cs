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
                    return RecordAction.Create;
                case "D":
                    return RecordAction.Delete;
                case "R":
                    return RecordAction.Update;
                default:
                    throw new InvalidOperationException($"Invalid record type {source}");
            }
        }
    }
}
