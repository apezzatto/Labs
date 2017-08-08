using System;
using System.Collections.Generic;
using System.Text;

namespace Events.IO.Domain.Events.ApplicationEvents
{
    public class EventUpdateEvent : EventBaseEvent
    {
        public EventUpdateEvent(
            Guid id,
            string name,
            string shortDescription,
            string longDescription,
            DateTime startDate,
            DateTime endDate,
            bool isFree,
            decimal price,
            bool online,
            string companyName)
        {
            Id = id;
            AggregateId = id;
            Name = name;
            ShortDescription = shortDescription;
            LongDescription = longDescription;
            StartDate = startDate;
            EndDate = endDate;
            IsFree = isFree;
            Price = price;
            Online = online;
            CompanyName = companyName;
        }
    }
}
