using System;
using FileHelpers;

namespace CifParser.Records
{
    public class SequenceConverter : ConverterBase
    {
        public override object StringToField(string @from)
        {
            if (string.IsNullOrEmpty(@from))
                return 1;

            return int.Parse(@from);
        }

        public override string FieldToString(object @from)
        {
            if (@from is int sequence)
            {
                return sequence == 1 ? " " : sequence.ToString();
            }

            throw new InvalidOperationException($"Invalid record type {@from.GetType()}");
        }
        
    }
}