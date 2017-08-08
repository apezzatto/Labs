using Events.IO.Domain.Core.AppEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Events.IO.Domain.Events.ApplicationEvents
{
    public class EventRegistrationEvent : EventBaseEvent
    {
        public EventRegistrationEvent(
            Guid id,
            string name,
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
            StartDate = startDate;
            EndDate = endDate;
            IsFree = isFree;
            Price = price;
            Online = online;
            CompanyName = companyName;
        }
    }
}
