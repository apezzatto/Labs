using Events.IO.Domain.Core.AppEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Events.IO.Domain.Core.Commands
{
    public class Command : Message
    {
        public DateTime TimeStamp { get; private set; }

        public Command()
        {
            TimeStamp = DateTime.Now;
        }
    }
}
