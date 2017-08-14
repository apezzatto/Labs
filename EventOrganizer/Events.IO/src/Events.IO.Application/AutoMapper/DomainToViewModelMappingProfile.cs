using AutoMapper;
using Events.IO.Application.ViewModels;
using Events.IO.Domain.Events;

namespace Events.IO.Application.AutoMapper
{
    public partial class AutoMapperConfiguration
    {
        public class DomainToViewModelMappingProfile : Profile
        {
            public DomainToViewModelMappingProfile()
            {
                CreateMap<Event, EventViewModel>();
                CreateMap<Address, AddressViewModel>();
                CreateMap<Category, CategoryViewModel>();
            }
        }
    }
}
