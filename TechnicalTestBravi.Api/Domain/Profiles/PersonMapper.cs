using AutoMapper;
using TechnicalTestBravi.Api.Domain.Commands.PersonCreate;
using TechnicalTestBravi.Api.Domain.Commands.PersonUpdate;
using TechnicalTestBravi.Api.Domain.Entities;

namespace TechnicalTestBravi.Api.Domain.Profiles;

public class PersonMapper : Profile
{
	public PersonMapper()
	{
		CreateMap<CreatePersonCommand, Person>();
		CreateMap<UpdatePersonCommand, Person>();
	}
}