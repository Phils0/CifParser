﻿using System;
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
            Schedule current = null;

            foreach(var record in _parser.Read(reader))
            {
                var schedule = record as ScheduleDetails;
                if (schedule != null)
                {
                    current = new Schedule(schedule);
                    continue;
                }
                
                if(current == null)
                {
                    // Not consolidating a service so just return the record
                    yield return record;
                }
                else
                {
                    current.Add(record);
                    if(current.IsTerminated)
                    {
                        var temp = current;
                        current = null;
                        yield return temp;
                    }
                }
            }
        }
    }
}
