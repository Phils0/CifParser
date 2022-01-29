using System;
using FileHelpers;

namespace CifParser.Records
{
    public class ServiceClassConverter : ConverterBase
    {
        public override object StringToField(string @from)
        {
            return Enum.Parse(typeof(ServiceClass), @from);
        }

        public override string FieldToString(object @from)
        {
            if (@from is ServiceClass sc)
            {
                switch (sc)
                {
                    case ServiceClass.None:
                        return " ";
                    case ServiceClass.B:
                        return "B";
                    case ServiceClass.S:
                        return "S";
                    case ServiceClass.F:
                        return "F";
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            throw new InvalidOperationException();
        }
    }
}