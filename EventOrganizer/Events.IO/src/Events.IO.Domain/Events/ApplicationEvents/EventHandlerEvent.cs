using Events.IO.Domain.Core.AppEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Events.IO.Domain.Events.ApplicationEvents
{
    public class EventHandlerEvent : 
        IHandler<EventRegistrationEvent>,
        IHandler<EventUpdateEvent>,
        IHandler<EventDeleteEvent>
    {
        public void Handle(EventRegistrationEvent Message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Event Registration Succeeded");
        }

        public void Handle(EventDeleteEvent Message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Event Deleted Succeeded");
        }

        public void Handle(EventUpdateEvent Message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Event Updated Succeeded");
        }
    }
}
