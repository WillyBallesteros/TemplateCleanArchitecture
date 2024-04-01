using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Rents.Events;
public sealed record CompletedRentalDomainEvent(Guid RentId) : IDomainEvent;
