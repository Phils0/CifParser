using FileHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CifParser.Records
{
    internal static class Selector
    {
        internal static Type Select(MultiRecordEngine engine, string recordLine)
        {
            if (recordLine.Length == 0)
                return null;

            var recordType = recordLine.Substring(0, 2);

            switch(recordType)
            {
                case "TI":
                    return typeof(TiplocInsert);
                case "TA":
                    return typeof(TiplocAmend);
                case "TD":
                    return typeof(TiplocDelete);
                default:
                    return null;
            }
        }


    }
}
