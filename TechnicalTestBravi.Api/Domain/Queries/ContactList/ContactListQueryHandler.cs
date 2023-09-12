using AutoMapper;
using System.Net;
using TechnicalTestBravi.Api.Domain.Contracts;
using TechnicalTestBravi.Api.Domain.Dtos;
using TechnicalTestBravi.Api.Domain.Entities;

namespace TechnicalTestBravi.Api.Domain.Queries.ContactList;

public sealed class ContactListQueryHandler : IQueryHandler<ContactListQuery, GenericResponseDto<List<Contact>>>
{
    private readonly IContactRepository _contactRepository;
    private readonly ILogger<ContactListQueryHandler> _logger;
    private readonly IMapper _mapper;

    private const string QueryName = nameof(ContactListQueryHandler);

    public ContactListQueryHandler(IContactRepository contactRepository, 
        ILogger<ContactListQueryHandler> logger, 
        IMapper mapper)
    {
        _contactRepository = contactRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<GenericResponseDto<List<Contact>>> HandleAsync(ContactListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Iniciando {QueryName}. {DateTime.Now.ToLongDateString()}");

        var response = new GenericResponseDto<List<Contact>>();
        try
        {
            var contacts = await _contactRepository.GetByPersonIdAsync(request.PersonId, cancellationToken);
            if(contacts is null || !contacts.Any())
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Notifications.Add("Contato não encontrado!");
                return response;
            }
            response.Content = contacts.ToList();
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