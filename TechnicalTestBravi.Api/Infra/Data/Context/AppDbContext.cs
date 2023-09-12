using Microsoft.EntityFrameworkCore;
using TechnicalTestBravi.Api.Domain.Entities;
using TechnicalTestBravi.Api.Infra.Data.Mapping;

namespace TechnicalTestBravi.Api.Infra.Data.Context;

public sealed class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> contextOptions)
		: base(contextOptions)
	{
	}

	public DbSet<Person>? People { get; set; }

	public DbSet<Contact>? Contacts { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new PersonMap());
		modelBuilder.ApplyConfiguration(new ContactMap());

		base.OnModelCreating(modelBuilder);
	}
}