using System;
using System.Collections.Generic;
using System.IO;
using CifParser.Records;
using Serilog;

namespace CifParser
{
    /// <summary>
    /// Groups a set of schedule records into a Schedule
    /// </summary>
    /// <remarks>Handles happy path only, assumes records are going to turn up in the right order</remarks>
    internal class ScheduleConsolidator : IParser
    {
        private IParser _parser;
        private ILogger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parser">Internal parser that really parses the text stream</param>
        public ScheduleConsolidator(IParser parser, ILogger logger)
        {
            _logger = logger;
            _parser = parser;
        }

        public IEnumerable<IRecord> Read(TextReader reader)
        {
            return Consolidate(_parser.Read(reader));
        }

        private IEnumerable<IRecord> Consolidate(IEnumerable<IRecord> records)
        {
            Schedule current = null;

            foreach (var record in records)
            {
                if (record is ScheduleDetails)
                {
                    if(current != null)
                        _logger.Error("Schedule not terminated {record}", record);
                    current = new Schedule();
                }

                if (current == null)
                {
                    // Not consolidating a service so just return the record
                    yield return record;
                }
                else
                {
                    current.Add(record);
                    if (current.IsTerminated)
                    {
                        var temp = current;
                        current = null;
                        yield return temp;
                    }
                }
            }
        }

        public IEnumerable<IRecord> Read(string file)
        {
            return Consolidate(_parser.Read(file));
        }
    }
}
