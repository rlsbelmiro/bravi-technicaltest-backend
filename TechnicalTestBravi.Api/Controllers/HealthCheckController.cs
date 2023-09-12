using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechnicalTestBravi.Api.Domain.Contracts;
using TechnicalTestBravi.Api.Domain.Dtos;
using TechnicalTestBravi.Api.Domain.Queries.PersonList;

namespace TechnicalTestBravi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class HealthCheckController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public ActionResult GetAllAsync()
    {
        return Ok();
    }
}
