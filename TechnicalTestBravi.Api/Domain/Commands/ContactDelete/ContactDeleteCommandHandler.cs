using AutoMapper;
using System.Net;
using TechnicalTestBravi.Api.Domain.Contracts;
using TechnicalTestBravi.Api.Domain.Dtos;
using TechnicalTestBravi.Api.Domain.Entities;

namespace TechnicalTestBravi.Api.Domain.Commands.ContactDelete;

public sealed class ContactDeleteCommandHandler : ICommandHandler<ContactDeleteCommand, GenericResponseDto<Contact>>
{
    private readonly IContactRepository _contactRepository;
    private readonly ILogger<ContactDeleteCommandHandler> _logger;

    private const string CommandName = nameof(ContactDeleteCommandHandler);

    public ContactDeleteCommandHandler(IContactRepository contactRepository,
        ILogger<ContactDeleteCommandHandler> logger)
    {
        _contactRepository = contactRepository;
        _logger = logger;
    }

    public async Task<GenericResponseDto<Contact>> HandleAsync(ContactDeleteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Iniciando {CommandName}. {DateTime.Now.ToLongDateString()}");

        var response = new GenericResponseDto<Contact>();
        try
        {
            var contact = await _contactRepository.GetById(request.Id, cancellationToken);
            if(contact is null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Notifications.Add("Contato não encontrado");
            }
            await _contactRepository.DeleteAsync(contact, cancellationToken);
            
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