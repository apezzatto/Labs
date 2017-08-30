using AutoMapper;
using Events.IO.Application.ViewModels;
using Events.IO.Domain.Events.Commands;
using System;

namespace Events.IO.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<EventViewModel, EventRegistrationCommand>()
                .ConstructUsing(c => new EventRegistrationCommand(c.Name, c.ShortDescription, c.LongDescription, c.StartDate, c.EndDate, c.IsFree, c.Price, c.Online, c.CompanyName, c.OrganizerId, c.CategoryId,
                    new AddAddressEventCommand(c.Address.Id, c.Address.Address1, c.Address.Address2, c.Address.ZipCode, c.Address.City, c.Address.Province, c.Id)));

            CreateMap<AddressViewModel, AddAddressEventCommand>()
                .ConstructUsing(c => new AddAddressEventCommand(Guid.NewGuid(), c.Address1, c.Address2, c.ZipCode, c.City, c.Province, c.EventId));
                
            CreateMap<EventViewModel, EventUpdateCommand>()
                .ConstructUsing(c => new EventUpdateCommand(c.Id, c.Name, c.ShortDescription, c.LongDescription, c.StartDate, c.EndDate, c.IsFree, c.Price, c.Online, c.CompanyName, c.OrganizerId, c.CategoryId));

            CreateMap<EventViewModel, EventDeleteCommand>()
                .ConstructUsing(c => new EventDeleteCommand(c.Id));
        }
    }
}
