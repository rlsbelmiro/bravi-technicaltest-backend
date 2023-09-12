namespace TechnicalTestBravi.Api.Domain.Commands.PersonUpdate;

public sealed class UpdatePersonCommand
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}