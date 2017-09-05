using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Events.IO.Domain.CommandHandlers;
using Events.IO.Domain.Core.AppEvents;
using Events.IO.Domain.Core.Bus;
using Events.IO.Domain.Core.Notifications;
using Events.IO.Domain.Interfaces;
using Events.IO.Domain.Organizers.Events;
using Events.IO.Domain.Organizers.Repository;

namespace Events.IO.Domain.Organizers.Commands
{
    public class OrganizerCommandHandler : CommandHandler,
        IHandler<OrganizerRegistrationCommand>
    {
        private readonly IBus _bus;
        private readonly IOrganizerRepository _organizerRepository;

        public OrganizerCommandHandler(IUnitOfWork uow, 
            IBus bus, 
            IDomainNotificationHandler<DomainNotification> notifications,
            IOrganizerRepository organizerRepository) : base(uow, bus, notifications)
        {
            _bus = bus;
            _organizerRepository = organizerRepository;
        }

        public void Handle(OrganizerRegistrationCommand message)
        {
            var organizer = new Organizer(message.Id, message.SIN, message.Name, message.Email);

            if (!organizer.IsValid())
            {
                NotifyValidationError(organizer.ValidationResult);
                return;
            }

            var existOrganizer = _organizerRepository.Find(o => o.SIN == organizer.SIN || o.Email == organizer.Email);

            if (existOrganizer.Any())
            {
                _bus.RaiseEvent(new DomainNotification(message.MessageType, "SIN or E-mail already exists"));
            }

            _organizerRepository.Add(organizer);

            //TODO: Add to repository

            if (Commit())
            {
                _bus.RaiseEvent(new OrganizerRegisteredEvent(organizer.Id, organizer.SIN, organizer.Name, organizer.Email));
            }
        }
    }
}
