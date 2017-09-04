using AutoMapper;
using Events.IO.Application.ViewModels;
using Events.IO.Domain.Events;
using Events.IO.Domain.Organizers;

namespace Events.IO.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Event, EventViewModel>();
            CreateMap<Address, AddressViewModel>();
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Organizer, OrganizerViewModel>();
        }
    }
}
