using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Vehicles;

namespace CleanArchitecture.Application.Vehicles.SearchVehicles;

public sealed record SearchVehiclesQuery(
    DateOnly startDate,
    DateOnly endDate
) : IQuery<IReadOnlyList<VehicleResponse>>;

