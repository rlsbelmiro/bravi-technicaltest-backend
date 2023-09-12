namespace TechnicalTestBravi.Api.Domain.Commands.ContactUpdate;

public sealed class ContactUpdateCommand
{
    public Guid Id { get; set; }
    public int Type { get; set; }
    public string Text { get; set; } = string.Empty;
}