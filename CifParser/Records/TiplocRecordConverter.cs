using FileHelpers;
using System;

namespace CifParser.Records
{
    internal class TiplocRecordConverter : ConverterBase
    {
        public override object StringToField(string source)
        {
            switch(source)
            {
                case "TI":
                    return RecordAction.Create;
                case "TA":
                    return RecordAction.Update;
                case "TD":
                    return RecordAction.Delete;
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
                        return "TI";
                    case RecordAction.Update:
                        return "TA";
                    case RecordAction.Delete:
                        return "TD";
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            throw new InvalidOperationException($"{@from.GetType()} is unsupported");
        }
    }
}
