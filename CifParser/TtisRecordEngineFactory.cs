using CifParser.Records;
using FileHelpers;
using Serilog;
using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using CifParser.RdgRecords;
using FileHelpers.Options;

namespace CifParser
{
    /// <summary>
    /// Creates the underlying file reading record engine from FileHelpers
    /// </summary>
    internal class TtisRecordEngineFactory
    {
        private ILogger Logger;

        internal TtisRecordEngineFactory(ILogger logger)
        {
            Logger = logger;
        }

        internal StationParser CreateStationParser(int ignoreLines = 1)
        {
            var engine = new MultiRecordEngine(StationTypes);
            engine.Options.IgnoreFirstLines = ignoreLines;
            engine.RecordSelector = new RecordTypeSelector(SelectStationRecord);
            return new StationParser(engine);
        }

        /// <summary>
        /// List of Master Station file record types
        /// </summary>
        /// <remarks>Needs coresponding case statement in SelectStationRecord method</remarks>
        private readonly Type[] StationTypes = new[]
        {
            typeof(Station),
            typeof(StationAlias)
        };

        /// <summary>
        /// Record Selector function for Master Station File
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="recordLine"></param>
        /// <returns></returns>
        private Type SelectStationRecord(MultiRecordEngine engine, string recordLine)
        {
            if (recordLine.Length == 0)
            {
                Logger.Warning("Empty line");
                return null;
            }

            var recordType = recordLine.Substring(0, 1);

            switch (recordType)
            {
                case "A":
                    return typeof(Station);
                case "L":
                    // return typeof(StationAlias);
                    Logger.Debug("Ignored alias: {recordLine}", recordLine);
                    return null;                    
                case "Z":
                case "0":
                case "M":
                case "-":
                case " ":
                case "E":                    
                    Logger.Debug("Ignored record type {recordType}: {recordLine}", recordType, recordLine);
                    return null;                   
                default:
                    Logger.Warning("Unknown record type {recordType}: {recordLine}", recordType, recordLine);
                    return null;
            }
        }
    }
}
