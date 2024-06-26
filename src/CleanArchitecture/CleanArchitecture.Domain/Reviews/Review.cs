using System.Security.Cryptography.X509Certificates;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Rents;
using CleanArchitecture.Domain.Reviews.Events;

namespace CleanArchitecture.Domain.Reviews;

public sealed class Review : Entity
{
    private Review(){}
    private Review(
        Guid id,
        Guid vehicleId,
        Guid rentId,
        Guid userId,
        Rating rating,
        Comment comment,
        DateTime? creationDate
    ) : base(id)
    {
        VehicleId = vehicleId;
        RentId = rentId;
        UserId = userId;
        Rating = rating;
        Comment = comment;
        CreationDate = creationDate;
    }

    public Guid VehicleId {get; private set;}
    public Guid RentId {get; private set;}
    public Guid UserId {get; private set;}
    public Rating? Rating {get; private set;}
    public Comment? Comment {get; private set;}
    public DateTime? CreationDate { get; set; }

    public static Result<Review> Create(
        Rent rent,
        Rating rating,
        Comment comment,
        DateTime creationDate
    )
    {
        if(rent.Status != RentalStatus.Completed) {
            return Result.Failure<Review>(ReviewErrors.NotEligible);
        }

        var review = new Review(
            Guid.NewGuid(),
            rent.VehicleId,
            rent.Id,
            rent.UserId,
            rating,
            comment,
            creationDate
        );

        review.RaiseDomainEvent(new CreatedReviewDomainEvent(review.Id));

        return review;
    }

}