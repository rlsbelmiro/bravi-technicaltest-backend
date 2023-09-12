using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TechnicalTestBravi.Api.Domain.Commands.ContactCreate;
using TechnicalTestBravi.Api.Domain.Commands.ContactDelete;
using TechnicalTestBravi.Api.Domain.Commands.ContactUpdate;
using TechnicalTestBravi.Api.Domain.Commands.PersonCreate;
using TechnicalTestBravi.Api.Domain.Commands.PersonDelete;
using TechnicalTestBravi.Api.Domain.Commands.PersonUpdate;
using TechnicalTestBravi.Api.Domain.Contracts;
using TechnicalTestBravi.Api.Domain.Dtos;
using TechnicalTestBravi.Api.Domain.Entities;
using TechnicalTestBravi.Api.Domain.Queries.ContactById;
using TechnicalTestBravi.Api.Domain.Queries.ContactList;
using TechnicalTestBravi.Api.Domain.Queries.PersonById;
using TechnicalTestBravi.Api.Domain.Queries.PersonList;
using TechnicalTestBravi.Api.Infra.Data.Context;
using TechnicalTestBravi.Api.Infra.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("bravi"));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//add repositories
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();

//add commandHandler
builder.Services.AddScoped<ICommandHandler<CreatePersonCommand, GenericResponseDto<Person>>, CreatePersonCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdatePersonCommand, GenericResponseDto<Person>>, UpdatePersonCommandHandler>();
builder.Services.AddScoped<ICommandHandler<DeletePersonCommand, GenericResponseDto<Person>>, DeletePersonCommandHandler>();

builder.Services.AddScoped<ICommandHandler<CreateContactCommand, GenericResponseDto<Contact>>, CreateContactCommandHandler>();
builder.Services.AddScoped<ICommandHandler<ContactUpdateCommand, GenericResponseDto<Contact>>, ContactUpdateCommandHandler>();
builder.Services.AddScoped<ICommandHandler<ContactDeleteCommand, GenericResponseDto<Contact>>, ContactDeleteCommandHandler>();

//add query handler
builder.Services.AddScoped<IQueryHandler<PersonByIdQuery, GenericResponseDto<Person>>, PersonByIdQueryHandler>();
builder.Services.AddScoped<IQueryHandler<PersonListQuery, GenericResponseDto<List<Person>>>, PersonListQueryHandler>();
builder.Services.AddScoped<IQueryHandler<ContactByIdQuery, GenericResponseDto<Contact>>, ContactByIdQueryHandler>();
builder.Services.AddScoped<IQueryHandler<ContactListQuery, GenericResponseDto<List<Contact>>>, ContactListQueryHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(opt =>
{
    opt.AllowAnyOrigin();
    opt.AllowAnyMethod();
    opt.AllowAnyHeader();
});

app.Run();