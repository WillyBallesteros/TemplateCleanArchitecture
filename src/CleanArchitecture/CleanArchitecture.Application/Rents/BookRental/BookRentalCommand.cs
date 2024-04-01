namespace CleanArchitecture.Application.Rents.BookRental;

using CleanArchitecture.Application.Abstractions.Messaging;



public record BookRentalCommand(
    Guid VehicleId,
    Guid UserId,
    DateOnly StartDate,
    DateOnly EndDate
) : ICommand<Guid>;

