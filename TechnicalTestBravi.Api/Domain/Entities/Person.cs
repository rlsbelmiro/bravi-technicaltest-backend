using System.Text.Json.Serialization;
using TechnicalTestBravi.Api.Domain.Contracts;

namespace TechnicalTestBravi.Api.Domain.Entities;

public sealed class Person : Entity<Guid>
{
    public Person()
    {
        Id = Guid.NewGuid();
    }

    public string Name { get; set; } = string.Empty;

    [JsonIgnore]
    public ICollection<Contact> Contacts { get; set; } = new HashSet<Contact>();
}