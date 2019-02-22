# CifParser
A .Net Standard 2.0 Library to parse a CIF formatted UK rail timetable

## How do I read a CIF file?

Instantiate a Parser and Read to be returned an enumeration of CIF records.  Records are returned in the order they appear in the file.

```
var parser = new Parser();
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
var parser = new ScheduleConsolidator(new Parser());;
var records = parser.Read(file);

var schedule = records.OfType<Schedule>().First();
foreach(var scheduleRecord in schedule.Records)
{
	...
}
```

where a record is not part of a schdule e.g. `Association` it is immediately returned.

## Implementation Details, why return `IEnumerable<ICifRecord>`?

It reads the CIF file record by record, yielding to the client once it has constructed a record.  This means it does not need to hold the whole set of records in memory at any time.

The implementation uses the [FileHelpers](https://www.filehelpers.net/) package to do most of the heavy lifting to acheive this.
