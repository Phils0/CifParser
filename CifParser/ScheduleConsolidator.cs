using System;
using System.Collections.Generic;
using System.IO;
using CifParser.Records;

namespace CifParser
{
    /// <summary>
    /// Groups a set of schedule records into a Schedule
    /// </summary>
    /// <remarks>Handles happy path only, assumes records are going to turn up in the right order</remarks>
    public class ScheduleConsolidator : IParser
    {
        private IParser _parser;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parser">Internal parser that really parses the text stream</param>
        public ScheduleConsolidator(IParser parser)
        {
            _parser = parser;
        }

        public IEnumerable<ICifRecord> Read(TextReader reader)
        {
            return Consolidate(_parser.Read(reader));
        }

        private IEnumerable<ICifRecord> Consolidate(IEnumerable<ICifRecord> records)
        {
            Schedule current = null;

            foreach (var record in records)
            {
                if (record is ScheduleDetails)
                {
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

        public IEnumerable<ICifRecord> Read(string file)
        {
            return Consolidate(_parser.Read(file));
        }
    }
}
