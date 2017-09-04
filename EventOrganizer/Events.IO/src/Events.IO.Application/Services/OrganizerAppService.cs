using AutoMapper;
using Events.IO.Application.Interfaces;
using Events.IO.Application.ViewModels;
using Events.IO.Domain.Core.Bus;
using Events.IO.Domain.Organizers.Commands;
using Events.IO.Domain.Organizers.Repository;

namespace Events.IO.Application.Services
{
    public class OrganizerAppService : IOrganizerAppService
    {
        private readonly IMapper _mapper;
        private readonly IOrganizerRepository _organizerRepository;
        private readonly IBus _bus;

        public OrganizerAppService(IMapper mapper, IOrganizerRepository organizerRepository, IBus bus)
        {
            _mapper = mapper;
            _organizerRepository = organizerRepository;
            _bus = bus;
        }

        public void Register(OrganizerViewModel organizerViewModel)
        {
            var registerCommand = _mapper.Map<OrganizerRegistrationCommand>(organizerViewModel);
            _bus.SendCommand(registerCommand);
        }

        public void Dispose()
        {
            _organizerRepository.Dispose();
        }
    }
}