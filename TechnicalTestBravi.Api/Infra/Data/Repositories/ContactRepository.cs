using Microsoft.EntityFrameworkCore;
using TechnicalTestBravi.Api.Domain.Contracts;
using TechnicalTestBravi.Api.Domain.Entities;
using TechnicalTestBravi.Api.Infra.Data.Context;

namespace TechnicalTestBravi.Api.Infra.Data.Repositories;

public sealed class ContactRepository : BaseRepository<Contact, Guid>, IContactRepository
{
    public ContactRepository(AppDbContext context) 
        : base(context)
    {
    }

    public async Task<IEnumerable<Contact>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken)
    {
        return await Context.Contacts!
            .Where(contact => contact.Person.Id == personId)
            .ToListAsync(cancellationToken);
    }
}