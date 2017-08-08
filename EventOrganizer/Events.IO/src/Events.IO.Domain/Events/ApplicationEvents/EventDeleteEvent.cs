using Events.IO.Domain.Core.AppEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Events.IO.Domain.Events.ApplicationEvents
{
    public class EventDeleteEvent : EventBaseEvent
    {
        public EventDeleteEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
