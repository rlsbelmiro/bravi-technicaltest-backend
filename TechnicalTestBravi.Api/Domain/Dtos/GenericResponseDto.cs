using System.Net;

namespace TechnicalTestBravi.Api.Domain.Dtos;

public record GenericResponseDto<T>
{
    public T? Content { get; set; }
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    public List<string> Notifications { get; set; } = new List<string>();
}
