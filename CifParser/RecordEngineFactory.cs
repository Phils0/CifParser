using CifParser.Records;
using FileHelpers;
using System;

namespace CifParser
{
    internal static class RecordEngineFactory
    {
        internal static MultiRecordEngine Create()
        {
            var engine = new MultiRecordEngine(Types);
            engine.RecordSelector = new RecordTypeSelector(Select);
            return engine;
        }

        private readonly static Type[] Types = new []
        {
            typeof(TiplocInsert),
            typeof(TiplocAmend),
            typeof(TiplocDelete),
            typeof(Header) 
        };

        private static Type Select(MultiRecordEngine engine, string recordLine)
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
                case "HD":
                    return typeof(Header);
                default:
                    return null;
            }
        }


    }
}
