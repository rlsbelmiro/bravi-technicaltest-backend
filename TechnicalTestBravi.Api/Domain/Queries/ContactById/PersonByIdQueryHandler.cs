using AutoMapper;
using System.Net;
using TechnicalTestBravi.Api.Domain.Contracts;
using TechnicalTestBravi.Api.Domain.Dtos;
using TechnicalTestBravi.Api.Domain.Entities;
using PersonEntity = TechnicalTestBravi.Api.Domain.Entities.Person;

namespace TechnicalTestBravi.Api.Domain.Queries.ContactById;

public sealed class ContactByIdQueryHandler : IQueryHandler<ContactByIdQuery, GenericResponseDto<Contact>>
{
    private readonly IContactRepository _contactRepository;
    private readonly ILogger<ContactByIdQueryHandler> _logger;
    private readonly IMapper _mapper;

    private const string QueryName = nameof(ContactByIdQueryHandler);

    public ContactByIdQueryHandler(IContactRepository contactRepository, 
        ILogger<ContactByIdQueryHandler> logger, 
        IMapper mapper)
    {
        _contactRepository = contactRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<GenericResponseDto<Contact>> HandleAsync(ContactByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Iniciando {QueryName}. {DateTime.Now.ToLongDateString()}");

        var response = new GenericResponseDto<Contact>();
        try
        {
            if(request.Id == Guid.Empty)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Notifications.Add("Informe o id do contato");
                return response;
            }
            var contact = await _contactRepository.GetById(request.Id, cancellationToken);
            if(contact is null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Notifications.Add("Contato não encontrado!");
                return response;
            }
            response.Content = contact;
        }
        catch(Exception ex)
        {
            _logger.LogError($"Houve um erro no {QueryName}. {DateTime.Now.ToLongDateString()}. StackTrace: {ex.StackTrace}");
            response.StatusCode = HttpStatusCode.BadRequest;
            response.Notifications.Add(ex.Message);
        }

        _logger.LogInformation($"Iniciando {QueryName}. {DateTime.Now.ToLongDateString()}");

        return response;
    }
}