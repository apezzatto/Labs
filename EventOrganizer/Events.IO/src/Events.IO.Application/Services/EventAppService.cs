using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Events.IO.Application.Interfaces;
using Events.IO.Application.ViewModels;
using Events.IO.Domain.Core.Bus;
using Events.IO.Domain.Events.Commands;
using Events.IO.Domain.Events.Repository;

namespace Events.IO.Application.Services
{
    public class EventAppService : IEventAppService
    {
        private readonly IBus _bus;
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;

        public EventAppService(IBus bus, IMapper mapper, IEventRepository eventRepository)
        {
            _bus = bus;
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        public void Register(EventViewModel eventViewModel)
        {
            var registerCommand = _mapper.Map<EventRegistrationCommand>(eventViewModel);
            _bus.SendCommand(registerCommand);
        }

        public void Update(EventViewModel eventViewModel)
        {
            //TODO: Validate whether or not the organizer owns the event

            var updateEventCommand = _mapper.Map<EventUpdateCommand>(eventViewModel);
            _bus.SendCommand(updateEventCommand);
        }

        public void Delete(Guid id)
        {
            _bus.SendCommand(new EventDeleteCommand(id));
        }

        public IEnumerable<EventViewModel> GetAll()
        {
            return _mapper.Map<IEnumerable<EventViewModel>>(_eventRepository.GetAll());
        }

        public EventViewModel GetById(Guid id)
        {
            return _mapper.Map<EventViewModel>(_eventRepository.GetById(id));
        }

        public IEnumerable<EventViewModel> GetEventByOrganizer(Guid organizerId)
        {
            return _mapper.Map<IEnumerable<EventViewModel>>(_eventRepository.GetEventByOrganizer(organizerId));
        }

        public void Dispose()
        {
            _eventRepository.Dispose();
        }
    }
}
