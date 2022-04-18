namespace Privasight.Model.Shared.Display.DataList;

public record DataListDisplayDetails(string Value, IEnumerable<DateTimeOffset> GenerationDates);