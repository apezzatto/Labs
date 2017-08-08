using System;
using System.Collections.Generic;
using System.Text;

namespace Events.IO.Domain.Core.AppEvents
{
    public abstract class ApplicationCoreEvent : Message
    {
        public DateTime Timestamp { get; private set; }

        protected ApplicationCoreEvent()
        {
            Timestamp = DateTime.Now;
        }
    }
}
