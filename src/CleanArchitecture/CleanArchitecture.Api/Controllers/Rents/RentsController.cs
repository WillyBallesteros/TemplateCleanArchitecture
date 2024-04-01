using CleanArchitecture.Application.Rents.BookRental;
using CleanArchitecture.Application.Rents.GetRental;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers.Rents;

[ApiController]
[Route("api/rents")]
public class RentsController : ControllerBase
{
    private readonly ISender _sender;

    public RentsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRent(
        Guid id,
        CancellationToken cancellationToken
    )
    {
        var query = new GetRentalQuery(id);
        var result = await _sender.Send(query, cancellationToken);
        
        return result.IsSuccess ? Ok(result.Value) : NotFound(); 
    }

    [HttpPost]
    public async Task<IActionResult> BookRental(
        Guid id,
        BookRentalRequest request,
        CancellationToken cancellationToken
    )
    {
        var command = new BookRentalCommand(
            request.VehicleId,
            request.UserId,
            request.StartDate,
            request.EndDate
        );

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure) {
            return BadRequest(result.Error);
        }

        return CreatedAtAction(nameof(GetRent), new {id = result.Value}, result.Value);
        
    }
}
