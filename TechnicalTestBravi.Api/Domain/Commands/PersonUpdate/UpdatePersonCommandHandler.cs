﻿using AutoMapper;
using System.Net;
using TechnicalTestBravi.Api.Domain.Contracts;
using TechnicalTestBravi.Api.Domain.Dtos;
using TechnicalTestBravi.Api.Domain.Helpers;
using PersonEntity = TechnicalTestBravi.Api.Domain.Entities.Person;

namespace TechnicalTestBravi.Api.Domain.Commands.PersonUpdate;

public sealed class UpdatePersonCommandHandler : ICommandHandler<UpdatePersonCommand, GenericResponseDto<PersonEntity>>
{
    private readonly IPersonRepository _personRepository;
    private readonly ILogger<UpdatePersonCommandHandler> _logger;
    private readonly IMapper _mapper;

    private const string CommandName = nameof(UpdatePersonCommandHandler);

    public UpdatePersonCommandHandler(IPersonRepository personRepository, 
        ILogger<UpdatePersonCommandHandler> logger, 
        IMapper mapper)
    {
        _personRepository = personRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<GenericResponseDto<PersonEntity>> HandleAsync(UpdatePersonCommand request, CancellationToken cancellationToken)
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
            if(request.Name!.IsNullOrEmpty())
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Notifications.Add("Informe o nome da pesssoa");
                return response;
            }

            var person = _mapper.Map<PersonEntity>(request);
            response.Content = await _personRepository.UpdateAsync(person, cancellationToken);
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