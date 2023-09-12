namespace TechnicalTestBravi.Api.Domain.Contracts;

public interface IQueryHandler<TRequest, TResponse>
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}