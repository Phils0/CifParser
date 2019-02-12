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

        /// <summary>
        /// List of CIF record types
        /// </summary>
        /// <remarks>Needs coresponding case statement in Select method</remarks>
        private readonly static Type[] Types = new[]
        {
            typeof(IntermediateLocation),
            typeof(OriginLocation),
            typeof(TerminalLocation),
            typeof(ScheduleDetails),
            typeof(ScheduleExtraData),
            typeof(ScheduleChange),
            typeof(TiplocInsert),
            typeof(TiplocAmend),
            typeof(TiplocDelete),
            typeof(Association),
            typeof(Header),
            typeof(Trailer)
        };

        /// <summary>
        /// Record Selector function
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="recordLine"></param>
        /// <returns></returns>
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
                case "LI":
                    return typeof(IntermediateLocation);
                case "LO":
                    return typeof(OriginLocation);
                case "LT":
                    return typeof(TerminalLocation);
                case "BS":
                    return typeof(ScheduleDetails);
                case "BX":
                    return typeof(ScheduleExtraData);
                case "CR":
                    return typeof(ScheduleChange);
                case "TI":
                    return typeof(TiplocInsert);
                case "TA":
                    return typeof(TiplocAmend);
                case "TD":
                    return typeof(TiplocDelete);
                case "AA":
                    return typeof(Association);
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
