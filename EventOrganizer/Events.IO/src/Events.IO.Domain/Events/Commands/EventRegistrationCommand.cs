using System;
using System.Collections.Generic;
using System.Text;

namespace Events.IO.Domain.Events.Commands
{
    public class EventRegistrationCommand : BaseEventCommand
    {
        public AddAddressEventCommand Address { get; private set; }

        public EventRegistrationCommand(
            string name,
            string shortDescription,
            string longDescription,
            DateTime startDate,
            DateTime endDate,
            bool isFree,
            decimal price,
            bool online,
            string companyName,
            Guid organizerId,
            Guid CategoryId,
            AddAddressEventCommand address)
        {
            Id = Guid.NewGuid();
            AggregateId = Id;
            Name = name;
            ShortDescription = shortDescription;
            LongDescription = longDescription;
            StartDate = startDate;
            EndDate = endDate;
            IsFree = isFree;
            Price = price;
            Online = online;
            CompanyName = companyName;
            OrganizerId = organizerId;
            CategoryId = CategoryId;
            Address = address;
        }
    }
}
