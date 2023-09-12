using TechnicalTestBravi.Api.Domain.Contracts;
using TechnicalTestBravi.Api.Infra.Data.Context;

namespace TechnicalTestBravi.Api.Infra.Data.Repositories;

public abstract class BaseRepository<T, I> : IRepository<T, I> where T : class
{
    protected readonly AppDbContext Context;

    public BaseRepository(AppDbContext context)
    {
        Context = context;
    }

    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
    {
        await Context
            .Set<T>()
            .AddAsync(entity, cancellationToken);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        Context
            .Set<T>()
            .Remove(entity);

        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task<T?> GetById(I id, CancellationToken cancellationToken)
    {
        return await Context
            .Set<T>()
            .FindAsync(id);
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        Context
            .Set<T>()
            .Update(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}