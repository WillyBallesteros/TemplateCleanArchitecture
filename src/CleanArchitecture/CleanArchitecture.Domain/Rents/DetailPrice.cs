using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Rents;

    public record DetailPrice
    (
        Currency PricePerPeriod,
        Currency  Maintenaince,
        Currency Accesories,
        Currency TotalPrice
    );
