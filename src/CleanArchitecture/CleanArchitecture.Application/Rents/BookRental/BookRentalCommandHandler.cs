using System.Runtime.InteropServices;
using CleanArchitecture.Application.Abstractions.Clock;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Rents;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Vehicles;

namespace CleanArchitecture.Application.Rents.BookRental;

internal sealed class BookRentalCommandHandler :
  ICommandHandler<BookRentalCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IRentRepository _rentRepository;
    private readonly PriceService _priceService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public BookRentalCommandHandler(
        IUserRepository userRepository, 
        IVehicleRepository vehicleRepository, 
        IRentRepository rentRepository, 
        PriceService priceService, 
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _vehicleRepository = vehicleRepository;
        _rentRepository = rentRepository;
        _priceService = priceService;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Guid>> Handle(
        BookRentalCommand request, 
        CancellationToken cancellationToken
        )
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        if (user is null) {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId, cancellationToken);
        if (vehicle is null) {
            return Result.Failure<Guid>(VehicleErrors.NotFound);
        }

        var duration = DateRange.Create(request.StartDate, request.EndDate);

        if (await _rentRepository.IsOverlappingAsync(vehicle, duration, cancellationToken)) {
            return Result.Failure<Guid>(RentalErrors.Overlap);
        }

        try {
            var rent = Rent.Book(
                vehicle,
                user.Id,
                duration,
                _dateTimeProvider.currentTime,
                _priceService
            );

            _rentRepository.Add(rent);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return rent.Id;
        } catch (ConcurrencyException) {
            return Result.Failure<Guid>(RentalErrors.Overlap);
        }
        

    }
}

