using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechnicalTestBravi.Api.Domain.Commands.ContactCreate;
using TechnicalTestBravi.Api.Domain.Commands.ContactDelete;
using TechnicalTestBravi.Api.Domain.Commands.ContactUpdate;
using TechnicalTestBravi.Api.Domain.Commands.PersonUpdate;
using TechnicalTestBravi.Api.Domain.Contracts;
using TechnicalTestBravi.Api.Domain.Dtos;
using TechnicalTestBravi.Api.Domain.Entities;
using TechnicalTestBravi.Api.Domain.Queries.ContactById;
using TechnicalTestBravi.Api.Domain.Queries.PersonById;
using TechnicalTestBravi.Api.Domain.Queries.PersonList;

namespace TechnicalTestBravi.Api.Controllers
{
    [ApiController]
    [Route("api/contact")]
    public class ContactController : ControllerBase
    {
        private readonly ILogger<ContactController> _logger;

        public ContactController(ILogger<ContactController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<GenericResponseDto<Contact>>> CreateAsync(
            [FromServices] ICommandHandler<CreateContactCommand, GenericResponseDto<Contact>> commandHandler,
            [FromBody] CreateContactCommand request,
            CancellationToken cancellationToken)
        {
            var result = await commandHandler.HandleAsync(request, cancellationToken);
            if (result.StatusCode == HttpStatusCode.OK)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<GenericResponseDto<Contact>>> UpdateAsync(
            [FromServices] ICommandHandler<ContactUpdateCommand, GenericResponseDto<Contact>> commandHandler,
            [FromBody] ContactUpdateCommand request,
            [FromRoute] Guid id,
            CancellationToken cancellationToken
            )
        {
            var commandRequest = new ContactUpdateCommand()
            {
                Id = id,
                Type = request.Type,
                Text = request.Text
            };
            var result = await commandHandler.HandleAsync(commandRequest, cancellationToken);
            if (result.StatusCode == HttpStatusCode.OK)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<GenericResponseDto<Contact>>> DeleteAsync(
            [FromServices] ICommandHandler<ContactDeleteCommand, GenericResponseDto<Contact>> commandHandler,
            [FromRoute] Guid id,
            CancellationToken cancellationToken
            )
        {
            var request = new ContactDeleteCommand()
            {
                Id = id
            };
            var result = await commandHandler.HandleAsync(request, cancellationToken);
            if (result.StatusCode == HttpStatusCode.OK)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GenericResponseDto<Contact>>> GetByIdAsync(
            [FromServices] IQueryHandler<ContactByIdQuery, GenericResponseDto<Contact>> queryHandler,
            [FromRoute] Guid id,
            CancellationToken cancellationToken
            )
        {
            var request = new ContactByIdQuery()
            {
                Id = id
            };
            var result = await queryHandler.HandleAsync(request, cancellationToken);
            if (result.StatusCode == HttpStatusCode.OK)
                return Ok(result);
            else if (result.StatusCode == HttpStatusCode.NotFound)
                return NotFound(result);
            else
                return BadRequest(result);
        }
    }
}