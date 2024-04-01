using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Reviews.Events;
public sealed record CreatedReviewDomainEvent(Guid RentId) : IDomainEvent;

