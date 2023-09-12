using AutoMapper;
using System.Net;
using TechnicalTestBravi.Api.Domain.Contracts;
using TechnicalTestBravi.Api.Domain.Dtos;
using PersonEntity = TechnicalTestBravi.Api.Domain.Entities.Person;

namespace TechnicalTestBravi.Api.Domain.Queries.PersonById;

public sealed class PersonByIdQueryHandler : IQueryHandler<PersonByIdQuery, GenericResponseDto<PersonEntity>>
{
    private readonly IPersonRepository _personRepository;
    private readonly ILogger<PersonByIdQueryHandler> _logger;
    private readonly IMapper _mapper;

    private const string QueryName = nameof(PersonByIdQueryHandler);

    public PersonByIdQueryHandler(IPersonRepository personRepository, 
        ILogger<PersonByIdQueryHandler> logger, 
        IMapper mapper)
    {
        _personRepository = personRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<GenericResponseDto<PersonEntity>> HandleAsync(PersonByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Iniciando {QueryName}. {DateTime.Now.ToLongDateString()}");

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
                response.StatusCode = HttpStatusCode.NotFound;
                response.Notifications.Add("Pessoa não encontrada!");
                return response;
            }
            response.Content = person;
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