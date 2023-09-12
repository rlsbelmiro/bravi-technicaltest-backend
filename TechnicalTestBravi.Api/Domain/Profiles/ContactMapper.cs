using AutoMapper;
using TechnicalTestBravi.Api.Domain.Commands.ContactCreate;
using TechnicalTestBravi.Api.Domain.Commands.ContactUpdate;
using TechnicalTestBravi.Api.Domain.Entities;

namespace TechnicalTestBravi.Api.Domain.Profiles;

public class ContactMapper : Profile
{
	public ContactMapper()
	{
		CreateMap<CreateContactCommand, Contact>();
		CreateMap<ContactUpdateCommand, Contact>();
	}
}