using AutoMapper;
using System.Net;
using TechnicalTestBravi.Api.Domain.Contracts;
using TechnicalTestBravi.Api.Domain.Dtos;
using PersonEntity = TechnicalTestBravi.Api.Domain.Entities.Person;

namespace TechnicalTestBravi.Api.Domain.Queries.PersonList;

public sealed class PersonListQueryHandler : IQueryHandler<PersonListQuery, GenericResponseDto<List<PersonEntity>>>
{
    private readonly IPersonRepository _personRepository;
    private readonly ILogger<PersonListQueryHandler> _logger;
    private readonly IMapper _mapper;

    private const string QueryName = nameof(PersonListQueryHandler);

    public PersonListQueryHandler(IPersonRepository personRepository, 
        ILogger<PersonListQueryHandler> logger, 
        IMapper mapper)
    {
        _personRepository = personRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<GenericResponseDto<List<PersonEntity>>> HandleAsync(PersonListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Iniciando {QueryName}. {DateTime.Now.ToLongDateString()}");

        var response = new GenericResponseDto<List<PersonEntity>>();
        try
        {
            var persons = await _personRepository.GetAllAsync(cancellationToken);
            if(persons is null || !persons.Any())
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Notifications.Add("Nenhuma pessoa cadastrada!");
                return response;
            }
            response.Content = persons;
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