using CleanArchitecture.Domain.Rents;

namespace CleanArchitecture.Domain.Vehicles;

public class VehicleErrors
{
    public static Error NotFound = new(
        "Vehicle.Found",
        "There is no vehicle with that ID."
    );
}
