# CifParser
A .Net Standard 2.0 Library to parse a CIF formatted UK rail timetable.  Can read both the Network Rail Open Data format and the RDG Format.

## How do I read a CIF file?

Instantiate a Parser and Read to be returned an enumeration of CIF records.  Records are returned in the order they appear in the file.

```
var factory = new CifParserFactory(logger);
var parser = factory.CreateParser();
var records = parser.Read(file);
```

## Grouping Schedule records.

You can group a set of schedule records together into a `Schedule`.
A schedule comprises a list of records in order:
* A `ScheduleDetails` record (BS)
* A `ScheduleDetailsExtraData` record (BX)
* An `OriginLocation` record (LO)
* Zero or more `IntermediateLocation` records (LI) in journey sequence
* Possibly `ScheduleChange` record(s) (CR).  These proceed the location record where the change occurs
* A `TerminalLocation` record (LT)

```
var factory = new ConsolidatorFactory(logger);
var parser = factory.CreateParser();
var records = parser.Read(file);

// To read an indivdual schedule
var schedule = records.OfType<Schedule>().First();
foreach(var scheduleRecord in schedule.Records)
{
	...
}
```

where a record is not part of a schdule e.g. `Association` it is immediately returned.

## RDG (TTIS) Zip Archives.

RDG (TTIS) zip archives contain a cif file (.mca) plus additional data files.  The CIF parser will happily process the CIF file.  It will set the RetailServiceId in the BX record that is additionally provided by the RDG format.  

The additional files are a work in progress, currently only the master station file (.msn) is handled.

```
var factory = new TtisParserFactory(logger);
var parser = factory.CreateStationParser();
var records = parser.Read(file);
```

## Implementation Details, why return `IEnumerable<ICifRecord>`?

It reads the CIF file record by record, yielding to the client once it has constructed a record.  This means it does not need to hold the whole set of records in memory at any time.

The implementation uses the [FileHelpers](https://www.filehelpers.net/) package to do most of the heavy lifting to acheive this.

# CifExtractor
A .Net Standard 2.0 Library to extract a CIF file from an archive.  
* `NrodZipExtractor` extracts the cif file from the Network Rail Open Data gz archive.
* `RdgZipExtractor` extracts the cif file from the RDG zip archive.  It can additionally extract the other files in the archive.

The Cif file is extracted directly into a `TextReader` that can be used when calling the CifParser
