using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnicalTestBravi.Api.Domain.Entities;

namespace TechnicalTestBravi.Api.Infra.Data.Mapping;

public sealed class ContactMap : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(e => e.Person)
            .WithMany(e => e.Contacts)
            .HasForeignKey(e => e.PersonId);
    }
}
