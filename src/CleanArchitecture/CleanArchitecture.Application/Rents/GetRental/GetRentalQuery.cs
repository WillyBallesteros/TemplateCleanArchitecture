using CleanArchitecture.Application.Abstractions.Messaging;

namespace CleanArchitecture.Application.Rents.GetRental;

public sealed record GetRentalQuery(Guid RentId) : IQuery<RentalResponse>;
