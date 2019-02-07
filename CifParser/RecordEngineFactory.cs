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

        private static Serilog.ILogger Logger => Serilog.Log.Logger;

        private readonly static Type[] Types = new[]
        {
            typeof(Schedule),
            typeof(ScheduleExtraData),
            typeof(TiplocInsert),
            typeof(TiplocAmend),
            typeof(TiplocDelete),
            typeof(Header),
            typeof(Trailer)
        };

        private static Type Select(MultiRecordEngine engine, string recordLine)
        {
            if (recordLine.Length == 0)
            {
                Logger.Warning("Empty line");
                return null;
            }

            var recordType = recordLine.Substring(0, 2);

            switch (recordType)
            {
                case "BS":
                    return typeof(Schedule);
                case "BX":
                    return typeof(ScheduleExtraData);
                case "TI":
                    return typeof(TiplocInsert);
                case "TA":
                    return typeof(TiplocAmend);
                case "TD":
                    return typeof(TiplocDelete);
                case "HD":
                    return typeof(Header);
                case "ZZ":
                    return typeof(Trailer);
                default:
                    Logger.Warning("Unknown record type {recordType}: {recordLine}", recordType, recordLine);
                    return null;
            }
        }


    }
}
