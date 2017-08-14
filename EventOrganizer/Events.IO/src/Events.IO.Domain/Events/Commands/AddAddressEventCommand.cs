using System;
using System.Collections.Generic;
using System.Text;
using Events.IO.Domain.Core.Commands;

namespace Events.IO.Domain.Events.Commands
{
    public class AddAddressEventCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string ZipCode { get; private set; }
        public string City { get; private set; }
        public string Province { get; private set; }
        public Guid? EventId { get; private set; }

        public AddAddressEventCommand(Guid id, string address1, string address2, string zipCode, string city, string province, Guid? eventId)
        {
            Id = id;
            Address1 = address1;
            Address2 = address2;
            ZipCode = zipCode;
            City = city;
            Province = province;
            EventId = eventId;
        }
    }
}
