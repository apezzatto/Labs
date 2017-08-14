using AutoMapper;
using Events.IO.Application.ViewModels;
using Events.IO.Domain.Events.Commands;

namespace Events.IO.Application.AutoMapper
{
    public partial class AutoMapperConfiguration
    {
        public class ViewModelToDomainMappingProfile : Profile
        {
            public ViewModelToDomainMappingProfile()
            {
                CreateMap<EventViewModel, EventRegistrationCommand>()
                    .ConstructUsing(c => new EventRegistrationCommand(c.Name, c.ShortDescription, c.LongDescription, c.StartDate, c.EndDate, c.IsFree, c.Price, c.Online, c.CompanyName, c.IdOrganizer, c.IdCategory,
                        new AddAddressEventCommand(c.Address.Id, c.Address.Address1, c.Address.Address2, c.Address.ZipCode, c.Address.City, c.Address.Province, c.Id)));

                CreateMap<EventViewModel, EventUpdateCommand>()
                    .ConstructUsing(c => new EventUpdateCommand(c.Id, c.Name, c.ShortDescription, c.LongDescription, c.StartDate, c.EndDate, c.IsFree, c.Price, c.Online, c.CompanyName, c.IdOrganizer, c.IdCategory));

                CreateMap<EventViewModel, EventDeleteCommand>()
                    .ConstructUsing(c => new EventDeleteCommand(c.Id));
            }
        }
    }
}
