using AutoMapper;
using System.Net;
using TechnicalTestBravi.Api.Domain.Contracts;
using TechnicalTestBravi.Api.Domain.Dtos;
using TechnicalTestBravi.Api.Domain.Entities;

namespace TechnicalTestBravi.Api.Domain.Commands.ContactUpdate;

public sealed class ContactUpdateCommandHandler : ICommandHandler<ContactUpdateCommand, GenericResponseDto<Contact>>
{
    private readonly IContactRepository _contactRepository;
    private readonly ILogger<ContactUpdateCommandHandler> _logger;
    private readonly IMapper _mapper;

    private const string CommandName = nameof(ContactUpdateCommandHandler);

    public ContactUpdateCommandHandler(IContactRepository contactRepository,
        ILogger<ContactUpdateCommandHandler> logger, 
        IMapper mapper)
    {
        _contactRepository = contactRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<GenericResponseDto<Contact>> HandleAsync(ContactUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Iniciando {CommandName}. {DateTime.Now.ToLongDateString()}");

        var response = new GenericResponseDto<Contact>();
        try
        {
            var contact = _mapper.Map<Contact>(request);
            response.Content = await _contactRepository.UpdateAsync(contact, cancellationToken);
            
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