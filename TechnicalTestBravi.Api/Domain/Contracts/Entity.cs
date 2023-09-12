using System.ComponentModel.DataAnnotations;

namespace TechnicalTestBravi.Api.Domain.Contracts;

public abstract class Entity<T>
{
    [Key]
    public T Id { get; set; }
}
