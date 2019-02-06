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
                    return RecordAction.Insert;
                case "TA":
                    return RecordAction.Amend;
                case "TD":
                    return RecordAction.Delete;
                default:
                    throw new InvalidOperationException($"Invalid record type {source}");
            }
        }
    }
}
