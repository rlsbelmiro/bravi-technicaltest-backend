using TechnicalTestBravi.Api.Domain.Entities;

namespace TechnicalTestBravi.Api.Domain.Contracts;

public interface IPersonRepository : IRepository<Person, Guid>
{
    Task<List<Person>> GetAllAsync(CancellationToken cancellationToken);
}