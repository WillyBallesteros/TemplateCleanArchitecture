namespace CleanArchitecture.Domain.Rents;

public sealed record DateRange
{
    private DateRange()
    { }

    public DateOnly Start {get; init;}
    public DateOnly End {get; init;}
    public int NumberOfDays => End.DayNumber - Start.DayNumber;
    
    public static DateRange Create(DateOnly start, DateOnly end) {
        if (start > end) {
            throw new ApplicationException("The end date is before the start date");
        }
        return new DateRange {
            Start = start,
            End = end 
        };
    }
}
