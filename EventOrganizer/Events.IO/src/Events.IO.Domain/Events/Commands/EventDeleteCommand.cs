using System;
using System.Collections.Generic;
using System.Text;

namespace Events.IO.Domain.Events.Commands
{
    public class EventDeleteCommand : BaseEventCommand
    {
        public EventDeleteCommand(Guid id)
        {
            Id = id;
            AggregateId = Id;
        }
    }
}
