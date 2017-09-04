using System;
using System.Collections.Generic;
using System.Text;
using Events.IO.Domain.Core.Commands;

namespace Events.IO.Domain.Organizers.Commands
{
    public class OrganizerRegistrationCommand : Command
    {
        public Guid Id { get; private set; }
        public string SIN { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        public OrganizerRegistrationCommand(Guid id, string sin, string name, string email)
        {
            Id = id;
            SIN = sin;
            Name = name;
            Email = email;
        }
    }
}
