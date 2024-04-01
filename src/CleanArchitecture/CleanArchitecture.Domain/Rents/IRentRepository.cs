using CleanArchitecture.Domain.Vehicles;

namespace CleanArchitecture.Domain.Rents;

public interface IRentRepository {
    Task<Rent?> GetByIdAsync(Guid id, CancellationToken cancellationToken=default);
    Task<bool> IsOverlappingAsync(
        Vehicle vehicle,
        DateRange duracion,
        CancellationToken cancellationToken = default
    );

    void Add(Rent rent);
}
    
        
    
