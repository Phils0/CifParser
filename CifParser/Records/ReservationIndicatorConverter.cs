using System;
using FileHelpers;

namespace CifParser.Records
{
    public class ReservationIndicatorConverter : ConverterBase
    {
        public override object StringToField(string @from)
        {
            return Enum.Parse(typeof(ReservationIndicator), @from);
        }

        public override string FieldToString(object @from)
        {
            if (@from is ReservationIndicator reservationIndicator)
            {
                switch (reservationIndicator)
                {
                    case ReservationIndicator.None:
                        return " ";
                    case ReservationIndicator.A:
                        return "A";
                    case ReservationIndicator.R:
                        return "R";
                    case ReservationIndicator.S:
                        return "S";
                    case ReservationIndicator.E:
                        return "E";
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            throw new InvalidOperationException();
        }
    }
}