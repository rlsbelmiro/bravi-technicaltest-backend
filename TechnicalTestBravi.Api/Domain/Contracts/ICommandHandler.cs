namespace TechnicalTestBravi.Api.Domain.Contracts;

public interface ICommandHandler<TRequest, TResponse>
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}