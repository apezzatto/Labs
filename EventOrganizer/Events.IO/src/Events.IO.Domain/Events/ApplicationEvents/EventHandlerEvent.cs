using Events.IO.Domain.Core.AppEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Events.IO.Domain.Events.ApplicationEvents
{
    public class EventHandlerEvent : 
        IHandler<EventRegistrationEvent>,
        IHandler<EventUpdateEvent>,
        IHandler<EventDeleteEvent>,
        IHandler<AddressEventAddedEvent>,
        IHandler<AddressEventUpdatedEvent>
    {
        public void Handle(EventRegistrationEvent message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Event registered successfuly");
        }

        public void Handle(EventDeleteEvent message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Event deleted successfuly");
        }

        public void Handle(EventUpdateEvent message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Event updated successfuly");
        }

        public void Handle(AddressEventAddedEvent message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Event address added successfuly");
        }

        public void Handle(AddressEventUpdatedEvent message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Event address updated successfuly");
        }
    }
}
