using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Rents.Events;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehicles;

namespace CleanArchitecture.Domain.Rents;

public sealed class Rent : Entity
{
    private Rent() 
    {

    }
    private Rent(
        Guid id,
        Guid vehicleId,
        Guid userId,
        DateRange duration,
        Currency pricePerPeriod,
        Currency maintenance,
        Currency accesories,
        Currency totalPrice,
        RentalStatus status,
        DateTime creationDate
        ) : base(id) 
    {
        VehicleId = vehicleId;
        UserId = userId;
        Duration =duration;
        PricePerPeriod = pricePerPeriod;
        Maintenance = maintenance;
        Accesories = accesories;
        TotalPrice = totalPrice;
        Status = status;
        CreationDate = creationDate;
    }
    public Guid VehicleId {get; private set;}
    public Guid UserId {get; private set;}
    public Currency? PricePerPeriod {get; private set;}
    public Currency? Maintenance {get; private set;}
    public Currency? Accesories {get; private set;}
    public Currency? TotalPrice {get; private set;}
    public RentalStatus Status {get; private set;}
    public DateRange? Duration {get; private set;}
    public DateTime? CreationDate {get; private set;}
    public DateTime? ConfirmationDate {get; private set;}
    public DateTime? DenialDate {get; private set;}
    public DateTime? CompletedDate { get; set; }
    public DateTime? CancelationDate { get; set; }

    public static Rent Book(
        Vehicle vehicle,
        Guid userId,
        DateRange duration,
        DateTime creationDate,
        PriceService priceService
    ) {
        var detailPrice = priceService.CalculatePrice(
            vehicle,
            duration
        );

        var rent = new Rent(
            Guid.NewGuid(),
            vehicle.Id,
            userId,
            duration,
            detailPrice.PricePerPeriod,
            detailPrice.Maintenaince,
            detailPrice.Accesories,
            detailPrice.TotalPrice,
            RentalStatus.Reserved,
            creationDate
        );

        rent.RaiseDomainEvent(new ReservedRentalDomainEvent(rent.Id!));
        vehicle.LastRentingDate = creationDate;

        return rent;
    }

    public Result Confirm(DateTime utcNow) 
    {
        if(Status != RentalStatus.Reserved) 
        {
            return Result.Failure(RentalErrors.NotReserved);
        }

        Status = RentalStatus.Confirmed;
        ConfirmationDate = utcNow;

        RaiseDomainEvent(new ConfirmedRentalDomainEvent(Id));

        return Result.Success();
    }

    public Result Reject(DateTime utcNow) {
        if(Status != RentalStatus.Reserved) {
            return Result.Failure(RentalErrors.NotReserved);
        }

        Status = RentalStatus.Rejected;
        DenialDate = utcNow;
        RaiseDomainEvent(new RejectedRentalDomainEvent(Id));

        return Result.Success();

    }

    public Result Cancell(DateTime utcNow) {
        if(Status != RentalStatus.Confirmed) {
            return Result.Failure(RentalErrors.NotConfirm);
        }

        var currentDay = DateOnly.FromDateTime(utcNow);

        if(currentDay > Duration!.Start) {
            return Result.Failure(RentalErrors.AlreadyStarted);
        }

        Status = RentalStatus.Cancelled;
        CancelationDate = utcNow;
        RaiseDomainEvent(new CancelledRentalDomainEvent(Id));

        return Result.Success();

    }

        public Result Completed(DateTime utcNow) {
        if(Status != RentalStatus.Confirmed) {
            return Result.Failure(RentalErrors.NotConfirm);
        }

        Status = RentalStatus.Completed;
        CompletedDate = utcNow;
        RaiseDomainEvent(new CompletedRentalDomainEvent(Id));

        return Result.Success();

    }

}
