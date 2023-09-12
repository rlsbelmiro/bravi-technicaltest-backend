using TechnicalTestBravi.Api.Domain.Entities;

namespace TechnicalTestBravi.Api.Domain.Contracts;

public interface IContactRepository : IRepository<Contact, Guid>
{
    Task<IEnumerable<Contact>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken);
}