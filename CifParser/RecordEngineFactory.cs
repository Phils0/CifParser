using CifParser.Records;
using FileHelpers;
using Serilog;
using System;
using System.Collections;
using System.IO;

namespace CifParser
{
    /// <summary>
    /// Creates the underlying file reading record engine from FileHelpers
    /// </summary>
    internal class RecordEngineFactory
    {
        private ILogger Logger;

        internal RecordEngineFactory(ILogger logger)
        {
            Logger = logger;
        }

        internal IEnumerable Create(TextReader reader)
        {
            var engine = new MultiRecordEngine(Types);
            engine.RecordSelector = new RecordTypeSelector(Select);
            engine.BeginReadStream(reader);
            return new SingleCallEnumerator(engine);
        }

        /// <summary>
        /// List of CIF record types
        /// </summary>
        /// <remarks>Needs coresponding case statement in Select method</remarks>
        private readonly Type[] Types = new[]
        {
            typeof(IntermediateLocation),
            typeof(OriginLocation),
            typeof(TerminalLocation),
            typeof(ScheduleDetails),
            typeof(ScheduleExtraData),
            typeof(ScheduleChange),
            typeof(TiplocInsertAmend),
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
        private Type Select(MultiRecordEngine engine, string recordLine)
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
                case "TA":
                    return typeof(TiplocInsertAmend);
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

        private class SingleCallEnumerator : IEnumerable
        {
            private bool _runOnce;

            private IEnumerable _enumerable;

            internal SingleCallEnumerator(IEnumerable enumerable)
            {
                _enumerable = enumerable;
            }

            public IEnumerator GetEnumerator()
            {
                if (_runOnce)
                    throw new InvalidOperationException("Can only iterate once.");

                _runOnce = true;
                return _enumerable.GetEnumerator();
            }
        }
    }
}
