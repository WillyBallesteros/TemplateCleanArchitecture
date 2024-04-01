using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Rents;
using Dapper;

namespace CleanArchitecture.Application.Vehicles.SearchVehicles;

internal sealed class SearchVehiclesQueryHandler
: IQueryHandler<SearchVehiclesQuery, IReadOnlyList<VehicleResponse>>
{
    private static readonly int[] ActiveRentStatuses =
    {
        (int)RentalStatus.Reserved,
        (int)RentalStatus.Confirmed,
        (int)RentalStatus.Completed
    };

    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public SearchVehiclesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<VehicleResponse>>> Handle(
        SearchVehiclesQuery request, 
        CancellationToken cancellationToken)
    {
        if(request.startDate > request.endDate) {
            return new List<VehicleResponse>();
        }

        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT 
                a.Id as Id,
                a.model as Model,
                a.price_amount as Price,
                a.price_currency_type as MoneyType,
                a.address_country as Country,
                a.address_department as Department,
                a.address_neighborhood as Neighborhood,
                a.address_city as City,
                a.address_street as Street
            FROM vehicles AS a
            WHERE NOT EXISTS
            (
                SELECT 1
                FROM rentals AS b
                WHERE 
                    b.vehicle_id = a.id AND
                    b.duration_start <= @EndDate AND
                    b.duration_end >= @StartDate AND
                    b.status = ANY(@ActiveRentStatuses)
            )
        """;
    
        var vehicles = await connection.QueryAsync<VehicleResponse, AddressResponse, VehicleResponse> 
        (
            sql,
            (vehicle, address) => {
                vehicle.Address = address;
                return vehicle;
            },
            new {
                StartDate = request.startDate,
                EndDate = request.endDate,
                ActiveRentStatuses
            },
            splitOn: "Country"
        );

        return vehicles.ToList();
    }
}
