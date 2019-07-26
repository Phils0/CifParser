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
    public class CifParserFactory : IParserFactory
    {
        private ILogger _logger;

        public CifParserFactory(ILogger logger)
        {
            _logger = logger;
        }

        public IParser CreateParser()
        {
            var engine = new MultiRecordEngine(Types);
            engine.RecordSelector = new RecordTypeSelector(Select);
            return new Parser(engine);
        }

        public IParser CreateParser(int ignoreLines)
        {
            var engine = new MultiRecordEngine(Types);
            engine.Options.IgnoreFirstLines = ignoreLines;
            engine.RecordSelector = new RecordTypeSelector(Select);
            return new Parser(engine);
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
                _logger.Warning("Empty line");
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
                    _logger.Warning("Unknown record type {recordType}: {recordLine}", recordType, recordLine);
                    return null;
            }
        }
    }

    public class ConsolidatorFactory : IParserFactory
    {
        private IParserFactory _factory;
        private ILogger _logger;

        public ConsolidatorFactory(ILogger logger) : this(new CifParserFactory(logger), logger)
        {
        }
              
        public ConsolidatorFactory(IParserFactory factory, ILogger logger)
        {
            _factory = factory;
            _logger = logger;
        }
        
        public IParser CreateParser()
        {
            return new ScheduleConsolidator(_factory.CreateParser(), _logger);
        }
        
        public IParser CreateParser(int ignoreLines)
        {
            return new ScheduleConsolidator(_factory.CreateParser(ignoreLines), _logger);
        }
    }
}
