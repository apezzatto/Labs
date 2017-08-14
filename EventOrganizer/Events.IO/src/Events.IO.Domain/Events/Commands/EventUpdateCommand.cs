using System;
using System.Collections.Generic;
using System.Text;

namespace Events.IO.Domain.Events.Commands
{
    public class EventUpdateCommand: BaseEventCommand
    {
        public EventUpdateCommand(
            Guid id,
            string name,
            string shortDescription,
            string longDescription,
            DateTime startDate,
            DateTime endDate,
            bool isFree,
            decimal price,
            bool online,
            string companyName,
            Guid idOrganizer,
            Guid idCategory)
        {
            Id = id;
            Name = name;
            ShortDescription = shortDescription;
            LongDescription = longDescription;
            StartDate = startDate;
            EndDate = endDate;
            IsFree = isFree;
            Price = price;
            Online = online;
            CompanyName = companyName;
            IdOrganizer = idOrganizer;
            IdCategory = idCategory;
        }
    }
}
