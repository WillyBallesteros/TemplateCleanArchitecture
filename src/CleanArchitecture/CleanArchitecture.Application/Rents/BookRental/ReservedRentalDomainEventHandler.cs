using CleanArchitecture.Application.Abstractions.Email;
using CleanArchitecture.Domain.Rents;
using CleanArchitecture.Domain.Rents.Events;
using CleanArchitecture.Domain.Users;
using MediatR;

namespace CleanArchitecture.Application.Rents.BookRental;

internal sealed class ReservedRentalDomainEventHandler
: INotificationHandler<ReservedRentalDomainEvent>
{

    private readonly IRentRepository _rentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public ReservedRentalDomainEventHandler(
        IRentRepository rentRepository, 
        IUserRepository userRepository, 
        IEmailService emailService)
    {
        _rentRepository = rentRepository;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task Handle(
        ReservedRentalDomainEvent notification, 
        CancellationToken cancellationToken)
    {
        var rent = await _rentRepository.GetByIdAsync(
            notification.RentId,
            cancellationToken
        );

        if(rent is null) {
            return;
        }

        var user = await _userRepository.GetByIdAsync(
            rent.UserId,
            cancellationToken
        );

        if(user is null) {
            return;
        }

        await _emailService.SendAsync(
            user.Email!,
            "Reserved Rental",
            "Please confirm your reservation."
        );
    }
}