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

        public override string FieldToString(object @from)
        {
            if (@from is RecordAction recordAction)
            {
                switch (recordAction)
                {
                    case RecordAction.NotSet:
                        throw new InvalidOperationException($"Invalid record type {recordAction}");
                    case RecordAction.Create:
                        return "N";
                    case RecordAction.Update:
                        return "R";
                    case RecordAction.Delete:
                        return "D";
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            throw new InvalidOperationException($"Invalid record type {@from.GetType()}");

        }
    }
}
