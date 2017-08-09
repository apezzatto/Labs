using System;
using System.Collections.Generic;
using Events.IO.Domain.Core.Models;
using Events.IO.Domain.Events;

namespace Events.IO.Domain.Organizers
{
    public class Organizer : Entity<Organizer>
    {
        public string SIN { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        //EF Navigation Property
        public virtual ICollection<Event> Events { get; set; }

        //EF Contructor
        protected Organizer() { }

        public Organizer(Guid id, string sin, string name, string email)
        {
            Id = id;
            SIN = sin;
            Name = name;
            Email = email;  
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}