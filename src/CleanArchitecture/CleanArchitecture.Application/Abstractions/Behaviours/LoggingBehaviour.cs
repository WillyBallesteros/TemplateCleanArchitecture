using System.Runtime.InteropServices.Marshalling;
using CleanArchitecture.Application.Abstractions.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Abstractions.Behaviours;

public class LoggingBehaviour<TRequest, TResponse>
: IPipelineBehavior<TRequest, TResponse>
where TRequest : IBaseCommand
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        //what is the command
        var name = request.GetType().Name;

        try
        {
            _logger.LogInformation($"Executing the command request: {name}");
            var result = await next();
            _logger.LogInformation($"Command {name} was executed successfully");
            return result;
        }
        catch(Exception exception) 
        {
            _logger.LogError(exception, $"Command {name} was errors");
            throw;
        }
    }
}
