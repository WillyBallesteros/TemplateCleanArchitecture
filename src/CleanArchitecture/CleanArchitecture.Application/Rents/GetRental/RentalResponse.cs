namespace CleanArchitecture.Application.Rents.GetRental;

public sealed class RentalResponse
{
    public Guid  Id {get; init;}
    public Guid UserId {get; init;}
    public Guid VehicleId {get; init;}
    public int Status {get; init;}
    public decimal RentalPrice {get; init;}
    public string? RentalMoneyType {get; init;}
    public decimal MaintenaincePrice {get; init;}
    public string? MaintenainceMoneyType {get; init;}
    public decimal AccesoriesPrice {get; init;}
    public string? AccesoriesMoneyType {get; init;}
    public decimal TotalPrice  {get; init;}
    public string? TotalPriceMoneyType {get; init;}
    public DateOnly StartDuration {get; init;}
    public DateOnly EndDuration {get; init;}
    public DateTime CreationDate {get; init;}
}
