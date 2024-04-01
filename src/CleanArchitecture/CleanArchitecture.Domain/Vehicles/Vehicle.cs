using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Vehicles
{
    public sealed class Vehicle : Entity
    {
         private Vehicle(){}
        public Vehicle(
            Guid id,
            Model model,
            Vin vin,
            Currency price,
            Currency maintenaince,
            DateTime? lastRentingDate,
            List<Accessory> accessories,
            Address? address
            ) : base(id)
        {
            Model = model;
            Vin = vin;
            Price = price;
            Maintenaince = maintenaince;
            LastRentingDate = lastRentingDate;
            Accesories = accessories;
            Address = address;
        }
        public Model? Model {get; private set;}
        public Vin? Vin {get; private set;}
        public Address? Address {get; private set;}
        public Currency? Price {get; private set;}
        public Currency? Maintenaince {get; private set;}
        public DateTime? LastRentingDate {get; internal set;}
        public List<Accessory> Accesories {get; private set;} = new();

    }
}