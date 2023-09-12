using AutoMapper;
using System.Net;
using TechnicalTestBravi.Api.Domain.Contracts;
using TechnicalTestBravi.Api.Domain.Dtos;
using TechnicalTestBravi.Api.Domain.Helpers;
using PersonEntity = TechnicalTestBravi.Api.Domain.Entities.Person;

namespace TechnicalTestBravi.Api.Domain.Commands.PersonDelete;

public sealed class DeletePersonCommandHandler : ICommandHandler<DeletePersonCommand, GenericResponseDto<PersonEntity>>
{
    private readonly IPersonRepository _personRepository;
    private readonly ILogger<DeletePersonCommandHandler> _logger;
    private readonly IMapper _mapper;

    private const string CommandName = nameof(DeletePersonCommandHandler);

    public DeletePersonCommandHandler(IPersonRepository personRepository, 
        ILogger<DeletePersonCommandHandler> logger, 
        IMapper mapper)
    {
        _personRepository = personRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<GenericResponseDto<PersonEntity>> HandleAsync(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Iniciando {CommandName}. {DateTime.Now.ToLongDateString()}");

        var response = new GenericResponseDto<PersonEntity>();
        try
        {
            if(request.Id == Guid.Empty)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Notifications.Add("Informe o id da pessoa");
                return response;
            }
            var person = await _personRepository.GetById(request.Id, cancellationToken);
            if(person is null)
            {
                response.StatusCode = HttpStatusCode.BadGateway;
                response.Notifications.Add("Pessoa não encontrada!");
                return response;
            }
            await _personRepository.DeleteAsync(person, cancellationToken);
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