using CleanArchitecture.Domain.Rents;

namespace CleanArchitecture.Domain.Reviews;

public static class ReviewErrors
{
    public static readonly Error NotEligible = new(
        "Review.NotEligigle",
        "The comment and rating are not available until the rental is completed."
    );
}
