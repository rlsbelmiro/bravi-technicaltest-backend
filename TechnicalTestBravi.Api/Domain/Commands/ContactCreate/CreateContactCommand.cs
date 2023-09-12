namespace TechnicalTestBravi.Api.Domain.Commands.ContactCreate;

public sealed class CreateContactCommand
{
    public Guid PersonId{ get; set; }
    public int Type { get; set; }
    public string Text { get; set; }= string.Empty;
}