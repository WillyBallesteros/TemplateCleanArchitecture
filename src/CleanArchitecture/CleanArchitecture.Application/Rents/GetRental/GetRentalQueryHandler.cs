using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using Dapper;

namespace CleanArchitecture.Application.Rents.GetRental;

internal sealed class GetRentalQueryHandler : IQueryHandler<GetRentalQuery, RentalResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetRentalQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<RentalResponse>> Handle(
        GetRentalQuery request, 
        CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        var sql = """
           SELECT 
               id AS Id,
               vehicle_id AS VehicleId,
               user_id AS UserId,
               status AS Status,
               price_per_period AS RentalPrice,
               price_per_period_money_type AS RentalMoneyType,
               maintenaince_price AS MaintenaincePrice,
               maintenaince_money_type AS MaintenainceMoneyType,
               accesories_price AS AccesoriesPrice,
               accesories_money_type AS AccesoriesMoneyType,
               total_price AS TotalPrice,
               total_price_money_type AS TotalPriceMoneyType,
               start_duration AS StartDuration,
               end_duration AS EndDuration,
               creation_date AS CreationDate
           FROM rentals WHERE id=@RentId
        """;

        var rent = await connection.QueryFirstOrDefaultAsync<RentalResponse>(
            sql,
            new {
                request.RentId
            }
        );

        return rent!;
    }
}

