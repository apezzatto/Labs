using System;
using System.Collections.Generic;
using System.Text;
using Events.IO.Domain.Core.Commands;
using Events.IO.Domain.Core.AppEvents;

namespace Events.IO.Domain.Core.Bus
{
    public interface IBus
    {
        void SendCommand<T>(T theCommand) where T : Command;
        void RaiseEvent<T>(T theEvent) where T : ApplicationCoreEvent;
    }
}
