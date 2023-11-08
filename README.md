# CifParser
A .Net Standard 2.0 Library to parse a CIF formatted UK rail timetable.  Can read both the Network Rail Open Data format and the RDG Format, both DTD and TTIS archives.

![Build](https://github.com/phils0/CifParser/actions/workflows/build.yml/badge.svg)
![Package](https://github.com/phils0/CifParser/actions/workflows/package.yml/badge.svg)

## How do I read a CIF archive?

Instantiate an Archive, create a Parser and Read to be returned an enumeration of CIF records.  Records are returned in the order they appear in the file.

```
var archive = new Archive(file, logger);
var parser = archive.CreateCifParser();
var records = parser.Read();
```

## Grouping Schedule records.

By default a set of schedule records are grouped together into a `Schedule`.
A schedule comprises a list of records in order:
* A `ScheduleDetails` record (BS)
* A `ScheduleDetailsExtraData` record (BX)
* An `OriginLocation` record (LO)
* Zero or more `IntermediateLocation` records (LI) in journey sequence
* Possibly `ScheduleChange` record(s) (CR).  These proceed the location record where the change occurs
* A `TerminalLocation` record (LT)

```
// To read an indivdual schedule
var schedule = records.OfType<Schedule>().First();
foreach(var scheduleRecord in schedule.Records)
{
	...
}
```

where a record is not part of a schdule e.g. `Association` it is immediately returned.

## RDG (DTD or TTIS) Zip Archives.

RDG zip archives contain a cif file (.mca) plus additional data files.  The CIF parser will process the CIF file.  It will set the RetailServiceId in the BX record that is additionally provided by the RDG format.  

The additional files are a work in progress, currently only the master station file (.msn) is handled.

```
var archive = new Archive(file, logger);
var parser = archive.CreateParser();
var records = parser.ReadFile(RdgZipExtractor.StationExtension);
```

## Implementation Details, why return `IEnumerable<ICifRecord>`?

It reads the CIF file record by record, yielding to the client once it has constructed a record.  This means it does not need to hold the whole set of records in memory at any time.

The implementation uses the [FileHelpers](https://www.filehelpers.net/) package to do most of the heavy lifting to acheive this.
