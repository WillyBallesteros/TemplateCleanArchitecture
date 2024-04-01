using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Rents;

namespace CleanArchitecture.Domain.Reviews;

public sealed record Rating
{
    public static readonly Error Invalid = new("Rating.Invalid","The rating is invalid");

    public int Value {get; init;}

    //New way to create a constructor
    private Rating(int value) => Value = value;

    public static Result<Rating> Create(int value) {
        if (value < 1 || value > 5) {
            return Result.Failure<Rating>(Invalid);
        }
        return new Rating(value);
    }
}

