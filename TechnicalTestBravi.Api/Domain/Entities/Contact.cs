using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TechnicalTestBravi.Api.Domain.Contracts;

namespace TechnicalTestBravi.Api.Domain.Entities;

public class Contact : Entity<Guid>
{
    public Contact()
    {
        Id = Guid.NewGuid();
    }

    public int Type { get; set; } = 0;

    public string Text { get; set; } = string.Empty;

    public Guid PersonId { get; set; }

    [JsonIgnore]
    public Person Person { get; set; } = new Person();
}
