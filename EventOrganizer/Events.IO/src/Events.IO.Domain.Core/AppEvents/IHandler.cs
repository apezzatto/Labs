using System;
using System.Collections.Generic;
using System.Text;

namespace Events.IO.Domain.Core.AppEvents
{
    public interface IHandler<in T> where T : Message
    {
        void Handle(T Message);
    }
}
