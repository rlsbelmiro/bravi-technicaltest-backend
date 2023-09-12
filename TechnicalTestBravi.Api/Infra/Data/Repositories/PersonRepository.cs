using Microsoft.EntityFrameworkCore;
using TechnicalTestBravi.Api.Domain.Contracts;
using TechnicalTestBravi.Api.Domain.Entities;
using TechnicalTestBravi.Api.Infra.Data.Context;

namespace TechnicalTestBravi.Api.Infra.Data.Repositories;

public sealed class PersonRepository : BaseRepository<Person, Guid>, IPersonRepository
{
    public PersonRepository(AppDbContext context) 
        : base(context)
    {
    }

    public async Task<List<Person>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await Context.People!.ToListAsync(cancellationToken);
    }
}