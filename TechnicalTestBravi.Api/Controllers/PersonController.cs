using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechnicalTestBravi.Api.Domain.Commands.PersonCreate;
using TechnicalTestBravi.Api.Domain.Commands.PersonDelete;
using TechnicalTestBravi.Api.Domain.Commands.PersonUpdate;
using TechnicalTestBravi.Api.Domain.Contracts;
using TechnicalTestBravi.Api.Domain.Dtos;
using TechnicalTestBravi.Api.Domain.Entities;
using TechnicalTestBravi.Api.Domain.Queries.ContactList;
using TechnicalTestBravi.Api.Domain.Queries.PersonById;
using TechnicalTestBravi.Api.Domain.Queries.PersonList;
using PersonEntity = TechnicalTestBravi.Api.Domain.Entities.Person;

namespace TechnicalTestBravi.Api.Controllers
{
    [ApiController]
    [Route("api/person")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<GenericResponseDto<PersonEntity>>> CreateAsync(
            [FromServices] ICommandHandler<CreatePersonCommand, GenericResponseDto<PersonEntity>> commandHandler,
            [FromBody] CreatePersonCommand request,
            CancellationToken cancellationToken
            )
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
        public async Task<ActionResult<GenericResponseDto<PersonEntity>>> UpdateAsync(
            [FromServices] ICommandHandler<UpdatePersonCommand, GenericResponseDto<PersonEntity>> commandHandler,
            [FromBody] CreatePersonCommand request,
            [FromRoute] Guid id,
            CancellationToken cancellationToken
            )
        {
            var commandRequest = new UpdatePersonCommand()
            {
                Id = id,
                Name = request.Name
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
        public async Task<ActionResult<GenericResponseDto<PersonEntity>>> DeleteAsync(
            [FromServices] ICommandHandler<DeletePersonCommand, GenericResponseDto<PersonEntity>> commandHandler,
            [FromRoute] Guid id,
            CancellationToken cancellationToken
            )
        {
            var request = new DeletePersonCommand()
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
        public async Task<ActionResult<GenericResponseDto<PersonEntity>>> GetByIdAsync(
            [FromServices] IQueryHandler<PersonByIdQuery, GenericResponseDto<PersonEntity>> queryHandler,
            [FromRoute] Guid id,
            CancellationToken cancellationToken
            )
        {
            var request = new PersonByIdQuery()
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

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GenericResponseDto<List<PersonEntity>>>> GetAllAsync(
            [FromServices] IQueryHandler<PersonListQuery, GenericResponseDto<List<PersonEntity>>> queryHandler,
            CancellationToken cancellationToken
            )
        {
            var result = await queryHandler.HandleAsync(new PersonListQuery(),cancellationToken);
            if (result.StatusCode == HttpStatusCode.OK)
                return Ok(result);
            else if (result.StatusCode == HttpStatusCode.NotFound)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpGet("{id}/contacts")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GenericResponseDto<List<Contact>>>> GetContactsAsync(
            [FromServices] IQueryHandler<ContactListQuery, GenericResponseDto<List<Contact>>> queryHandler,
            [FromRoute] Guid id,
            CancellationToken cancellationToken
            )
        {
            var request = new ContactListQuery()
            {
                PersonId = id
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