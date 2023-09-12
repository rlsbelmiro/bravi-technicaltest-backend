using AutoMapper;
using System.Net;
using TechnicalTestBravi.Api.Domain.Contracts;
using TechnicalTestBravi.Api.Domain.Dtos;
using TechnicalTestBravi.Api.Domain.Entities;

namespace TechnicalTestBravi.Api.Domain.Commands.ContactCreate;

public sealed class CreateContactCommandHandler : ICommandHandler<CreateContactCommand, GenericResponseDto<Contact>>
{
    private readonly IContactRepository _contactRepository;
    private readonly IPersonRepository _personRepository;
    private readonly ILogger<CreateContactCommandHandler> _logger;
    private readonly IMapper _mapper;

    private const string CommandName = nameof(CreateContactCommandHandler);

    public CreateContactCommandHandler(IContactRepository contactRepository,
        IPersonRepository personRepository,
        ILogger<CreateContactCommandHandler> logger, 
        IMapper mapper)
    {
        _contactRepository = contactRepository;
        _personRepository = personRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<GenericResponseDto<Contact>> HandleAsync(CreateContactCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Iniciando {CommandName}. {DateTime.Now.ToLongDateString()}");

        var response = new GenericResponseDto<Contact>();
        try
        {
            var person = await _personRepository.GetById(request.PersonId, cancellationToken);
            if(person is null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Notifications.Add("Pessoa não encontrada!");
                return response;
            }
            var contact = _mapper.Map<Contact>(request);
            contact.Person = person;
            response.Content = await _contactRepository.CreateAsync(contact, cancellationToken);
            
        }
        catch(Exception ex)
        {
            _logger.LogError($"Houve um erro no {CommandName}. {DateTime.Now.ToLongDateString()}. StackTrace: {ex.StackTrace}");
            response.StatusCode = HttpStatusCode.BadRequest;
            response.Notifications.Add(ex.Message);
        }

        _logger.LogInformation($"Iniciando {CommandName}. {DateTime.Now.ToLongDateString()}");

        return response;
    }
}