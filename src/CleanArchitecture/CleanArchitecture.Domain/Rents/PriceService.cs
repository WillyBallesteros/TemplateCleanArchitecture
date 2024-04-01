using System.Security.Cryptography;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehicles;

namespace CleanArchitecture.Domain.Rents;

public class PriceService
{
    public DetailPrice CalculatePrice(Vehicle vehicle, DateRange period) {
        var currencyType = vehicle.Price!.CurrencyType;
        var pricePerPeriod = new Currency(
            period.NumberOfDays*vehicle.Price.Amount,
            currencyType);

        decimal changePercentage = 0;


        foreach(var accesory in vehicle.Accesories) {
            changePercentage += accesory switch 
            {
                Accessory.AppleCar or Accessory.AndroidCar => 0.05m,
                Accessory.AirConditioning => 0.01m,
                Accessory.Maps => 0.01m,
                _ => 0
            };
        }

        var accesoryCharges = Currency.Zero(currencyType);
        if (changePercentage > 0) {
            accesoryCharges = new Currency(
                pricePerPeriod.Amount * changePercentage,
                currencyType
            );
        }

        var totalPrice = Currency.Zero();
        totalPrice += pricePerPeriod;

        if (!vehicle!.Maintenaince!.IsZero()) {
            totalPrice += vehicle.Maintenaince;
        }
        totalPrice += accesoryCharges;

        return new DetailPrice(
            pricePerPeriod, 
            vehicle.Maintenaince, 
            accesoryCharges,
            totalPrice);

    }
}
