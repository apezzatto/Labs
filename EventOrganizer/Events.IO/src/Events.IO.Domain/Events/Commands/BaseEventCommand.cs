using System;
using System.Collections.Generic;
using System.Text;
using Events.IO.Domain.Core.Commands;

namespace Events.IO.Domain.Events.Commands
{
    public abstract class BaseEventCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string ShortDescription { get; protected set; }
        public string LongDescription { get; protected set; }
        public DateTime StartDate { get; protected set; }
        public DateTime EndDate { get; protected set; }
        public bool IsFree { get; protected set; }
        public decimal Price { get; protected set; }
        public bool Online { get; protected set; }
        public string CompanyName { get; protected set; }
        public Guid IdOrganizer { get; protected set; }
        public Guid IdCategory { get; protected set; }
    }
}
