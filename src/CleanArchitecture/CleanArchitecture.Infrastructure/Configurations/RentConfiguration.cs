using CleanArchitecture.Domain.Rents;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

public class RentConfiguration : IEntityTypeConfiguration<Rent>
{
    public void Configure(EntityTypeBuilder<Rent> builder)
    {

        builder.ToTable("rentals");
        builder.HasKey(rent => rent.Id);
        builder.OwnsOne(rent => rent.PricePerPeriod, priceBuilder => {
            priceBuilder.Property(currency => currency.CurrencyType)
            .HasConversion(currencyType => currencyType.Code, code => CurrencyType.FromCode(code!));
        });

        builder.OwnsOne(rent => rent.Maintenance, priceBuilder => {
            priceBuilder.Property(currency => currency.CurrencyType)
            .HasConversion(currencyType => currencyType.Code, code => CurrencyType.FromCode(code!));
        });

        builder.OwnsOne(rent => rent.Accesories, priceBuilder => {
            priceBuilder.Property(currency => currency.CurrencyType)
            .HasConversion(currencyType => currencyType.Code, code => CurrencyType.FromCode(code!));
        });

        builder.OwnsOne(rent => rent.TotalPrice, priceBuilder => {
            priceBuilder.Property(currency => currency.CurrencyType)
            .HasConversion(currencyType => currencyType.Code, code => CurrencyType.FromCode(code!));
        });

        builder.OwnsOne(rent => rent.Duration);
        
        builder.HasOne<Vehicle>()
          .WithMany()
          .HasForeignKey(rent => rent.VehicleId);

        builder.HasOne<User>()
          .WithMany()
          .HasForeignKey(rent => rent.UserId);

    }
}
