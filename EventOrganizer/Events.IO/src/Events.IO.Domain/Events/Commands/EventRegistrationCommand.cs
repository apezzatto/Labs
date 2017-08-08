using System;
using System.Collections.Generic;
using System.Text;

namespace Events.IO.Domain.Events.Commands
{
    public class EventRegistrationCommand : BaseEventCommand
    {
        public EventRegistrationCommand(
            string name,
            DateTime startDate,
            DateTime endDate,
            bool isFree,
            decimal price,
            bool online,
            string companyName,
            Guid idOrganizer,
            Address address,
            Category category)
        {
            Id = Guid.NewGuid();
            AggregateId = Id;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            IsFree = isFree;
            Price = price;
            Online = online;
            CompanyName = companyName;
            IdOrganizer = idOrganizer;
            Address = address;
            Category = category;
        }
    }
}
