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
        IHandler<EventDeleteCommand>,
        IHandler<AddAddressEventCommand>,
        IHandler<UpdateAddressEventCommand>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IBus _bus;
        private readonly IUser _user;

        public CommandEventHandler(IEventRepository eventRepository,
                                   IUnitOfWork uow,
                                   IBus bus,
                                   IDomainNotificationHandler<DomainNotification> notifications,
                                   IUser user) : base(uow, bus, notifications)
        {
            _eventRepository = eventRepository;
            _bus = bus;
            _user = user;
        }

        public void Handle(EventRegistrationCommand message)
        {
            var address = new Address(message.Address.Id, message.Address.Address1, message.Address.Address2, message.Address.ZipCode, message.Address.City, message.Address.Province, message.Id);

            var @event = Event.EventFactory.NewFullEvent(
                message.Id, 
                message.Name,
                message.ShortDescription,
                message.LongDescription,
                message.StartDate,
                message.EndDate,
                message.IsFree,
                message.Price,
                message.Online,
                message.CompanyName,
                message.OrganizerId,
                address,
                message.CategoryId);

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

            var currentEvent = _eventRepository.GetById(message.Id);

            if (currentEvent.OrganizerId != _user.GetUserId())
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "Event does not belong to this organizer."));
                return;
            }

            currentEvent.SetEventExcluded();

            _eventRepository.Update(currentEvent);

            if (Commit())
                _bus.RaiseEvent(new EventDeleteEvent(message.Id));
        }

        public void Handle(EventUpdateCommand message)
        {
            if (!EventExists(message.Id, message.MessageType)) return;

            var currentEvent = _eventRepository.GetById(message.Id);

            if (currentEvent.OrganizerId != _user.GetUserId())
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "Event does not belong to this organizer."));
                return;
            }

            var @event = Event.EventFactory.NewFullEvent(message.Id, message.Name, message.ShortDescription, message.LongDescription, message.StartDate, message.EndDate, message.IsFree, message.Price, message.Online, message.CompanyName, message.OrganizerId, currentEvent.Address, message.CategoryId);

            if (!@event.Online && @event.Address == null)
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "It is not possible to update an event without an address"));
                return;
            }

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

        public void Handle(AddAddressEventCommand message)
        {
            var address = new Address(message.Id, message.Address1, message.Address2, message.ZipCode, message.City, message.Province, message.EventId.Value);

            if (!address.IsValid())
            {
                NotifyValidationError(address.ValidationResult);
                return;
            }

            _eventRepository.AddAddress(address);

            if (Commit())
            {
                _bus.RaiseEvent(new AddressEventAddedEvent(address.Id, address.Address1, address.Address2, address.ZipCode, address.City, address.Province, address.EventId.Value));
            }
        }

        public void Handle(UpdateAddressEventCommand message)
        {
            var address = new Address(message.Id, message.Address1, message.Address2, message.ZipCode, message.City, message.Province, message.EventId.Value);

            if (!address.IsValid())
            {
                NotifyValidationError(address.ValidationResult);
                return;
            }

            _eventRepository.UpdateAddress(address);

            if (Commit())
            {
                _bus.RaiseEvent(new AddressEventUpdatedEvent(address.Id, address.Address1, address.Address2, address.ZipCode, address.City, address.Province, address.EventId.Value));
            }
        }
    }
}
