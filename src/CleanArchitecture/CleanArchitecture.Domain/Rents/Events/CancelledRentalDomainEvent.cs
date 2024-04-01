using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Rents.Events;

public sealed record CancelledRentalDomainEvent(Guid RentId) : IDomainEvent;
