using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Rents.Events;

public sealed record ReservedRentalDomainEvent(Guid RentId) : IDomainEvent;
