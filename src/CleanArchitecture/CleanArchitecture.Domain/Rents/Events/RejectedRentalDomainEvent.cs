using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Rents.Events;

public sealed record RejectedRentalDomainEvent(Guid Id) : IDomainEvent;