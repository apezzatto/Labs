using Events.IO.Domain.CommandHandlers;
using Events.IO.Domain.Core.AppEvents;
using Events.IO.Domain.Core.Bus;
using Events.IO.Domain.Core.Notifications;
using Events.IO.Domain.Events.ApplicationEvents;
using Events.IO.Domain.Events.Repository;
using Events.IO.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Events.IO.Domain.Events.Commands
{
    public class CommandEventHandler : CommandHandler,
        IHandler<EventRegistrationCommand>,
        IHandler<EventUpdateCommand>,
        IHandler<EventDeleteCommand>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IBus _bus;

        public CommandEventHandler(IEventRepository eventRepository,
                                   IUnitOfWork uow,
                                   IBus bus,
                                   IDomainNotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _eventRepository = eventRepository;
            _bus = bus;
        }

        public void Handle(EventRegistrationCommand message)
        {
            var address = new Address(message.Address.Id, message.Address.Address1, message.Address.Address2, message.Address.ZipCode, message.Address.City, message.Address.Province, message.Id);

            var @event = Event.EventFactory.NewFullEvent(
                message.Id, 
                message.Name,
                message.StartDate,
                message.EndDate,
                message.IsFree,
                message.Price,
                message.Online,
                message.CompanyName,
                message.IdOrganizer,
                address,
                message.IdCategory);

            if (!IsValidEvent(@event)) return;

            //Business validation
            //====================//

            //Persistence
            _eventRepository.Add(@event);
            //===========//

            if (Commit())
            {
                //Notify process succeded
                Console.WriteLine("Event registered successfully");
                _bus.RaiseEvent(new EventRegistrationEvent(@event.Id, @event.Name, @event.StartDate, @event.EndDate, @event.IsFree, @event.Price, @event.Online, @event.CompanyName));
            }
        }

        public void Handle(EventDeleteCommand message)
        {
            if (!EventExists(message.Id, message.MessageType)) return;

            _eventRepository.Delete(message.Id);

            if (Commit())
                _bus.RaiseEvent(new EventDeleteEvent(message.Id));
        }

        public void Handle(EventUpdateCommand message)
        {
            if (!EventExists(message.Id, message.MessageType)) return;

            //TODO: Validate whether the event belongs to the person who is actually editing it or not.

            var currentEvent = _eventRepository.GetById(message.Id);

            var @event = Event.EventFactory.NewFullEvent(message.Id, message.Name, message.StartDate, message.EndDate, message.IsFree, message.Price, message.Online, message.CompanyName, message.IdOrganizer, currentEvent.Address, message.IdCategory);

            if (!IsValidEvent(@event)) return;

            _eventRepository.Update(@event);

            if (Commit())
                _bus.RaiseEvent(new EventUpdateEvent(@event.Id, @event.Name, @event.ShortDescription, @event.LongDescription, @event.StartDate, @event.EndDate, @event.IsFree, @event.Price, @event.Online, @event.CompanyName));
        }

        private bool IsValidEvent(Event @event)
        {
            if (@event.IsValid()) return true;

            NotifyValidationError(@event.ValidationResult);
            return false;
        }

        private bool EventExists(Guid id, string messageType)
        {
            var @event = _eventRepository.GetById(id);

            if (@event != null) return true;

            _bus.RaiseEvent(new DomainNotification(messageType, "Event not found"));

            return false;
        }
    }
}
