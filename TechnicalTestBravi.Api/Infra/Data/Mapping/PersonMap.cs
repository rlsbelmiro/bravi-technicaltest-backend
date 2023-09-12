using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnicalTestBravi.Api.Domain.Entities;

namespace TechnicalTestBravi.Api.Infra.Data.Mapping;

public sealed class PersonMap : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(e => e.Contacts)
            .WithOne(e => e.Person)
            .HasForeignKey(e => e.PersonId);
    }
}
