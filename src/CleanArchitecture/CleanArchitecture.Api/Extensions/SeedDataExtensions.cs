using Bogus;
using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Domain.Vehicles;
using Dapper;

namespace CleanArchitecture.Api.Extensions;

public static class SeedDataExtensions {
    public static void SeedData(this IApplicationBuilder app) {
        using var scope = app.ApplicationServices.CreateScope();
        var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
        using var connection = sqlConnectionFactory.CreateConnection();

        var faker = new Faker();

        List<object> vehicles = new();

        for(var i = 0; i < 100; i++) {
            vehicles.Add(new {
                Id = Guid.NewGuid(),
                Vin = faker.Vehicle.Vin(),
                Model = faker.Vehicle.Model(),
                Country = faker.Address.Country(),
                Department = faker.Address.State(),
                Neighborhood = faker.Address.County(),
                City = faker.Address.City(),
                Street = faker.Address.StreetAddress(),
                Price = faker.Random.Decimal(1000, 2000),
                MoneyType = "USD",
                MaintenaincePrice = faker.Random.Decimal(100, 200),
                MaintenainceMoneyType = "USD",
                Accesories = new List<int>{ (int)Accessory.Wifi, (int)Accessory.AppleCar },
                EndDuration = DateTime.MinValue
            });
        }

        const string sql = """
          INSERT INTO public.vehicles
           (id, model, vin, address_country, address_department, address_neighborhood, address_city, 
           address_street, price_amount, price_currency_type, maintenaince_amount, 
           maintenaince_currency_type, accesories, last_renting_date)
           values
           (@Id, @Model, @Vin, @Country, @Department, @Neighborhood, @City,
           @Street, @Price, @MoneyType, @MaintenaincePrice,
           @MaintenainceMoneyType, @Accesories, @EndDuration)
        """;

        connection.Execute(sql, vehicles);

    }
}

