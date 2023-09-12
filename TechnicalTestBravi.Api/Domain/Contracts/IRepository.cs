namespace TechnicalTestBravi.Api.Domain.Contracts;

public interface IRepository<T, I> where T : class
{
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken);
    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(T entity, CancellationToken cancellationToken);
    Task<T?> GetById(I id, CancellationToken cancellationToken);
}
