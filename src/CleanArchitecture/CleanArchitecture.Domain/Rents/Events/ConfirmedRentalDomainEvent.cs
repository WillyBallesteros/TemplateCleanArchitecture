using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Rents.Events;

public sealed record ConfirmedRentalDomainEvent(Guid RentId) : IDomainEvent;

