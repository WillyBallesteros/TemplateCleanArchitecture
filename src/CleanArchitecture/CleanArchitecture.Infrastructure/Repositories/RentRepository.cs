using CleanArchitecture.Domain.Rents;
using CleanArchitecture.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories;

internal sealed class RentRepository : Repository<Rent>, IRentRepository
{

    private static readonly RentalStatus[] ActiveRentalStatuses = {
        RentalStatus.Reserved,
        RentalStatus.Confirmed,
        RentalStatus.Completed
    };
    public RentRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsOverlappingAsync(
        Vehicle vehicle, 
        DateRange duration, 
        CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Rent>()
          .AnyAsync(
            rent => 
              rent.VehicleId == vehicle.Id && 
              rent.Duration!.Start <= duration.End &&
              rent.Duration!.End >= duration.Start &&
              ActiveRentalStatuses.Contains(rent.Status),
              cancellationToken
          );
    }
}
