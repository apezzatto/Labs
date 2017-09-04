using System;
using Events.IO.Domain.Core.AppEvents;
using Events.IO.Domain.Events;

namespace Events.IO.Domain.Organizers.Events
{
    public class OrganizerRegisteredEvent : ApplicationCoreEvent
    {
        public Guid Id { get; private set; }
        public string SIN { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        public OrganizerRegisteredEvent(Guid id, string sin, string name, string email)
        {
            Id = id;
            SIN = sin;
            Name = name;
            Email = email;  
        }
    }
}