using Events.IO.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Events.IO.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        CommandResponse Commit();
    }
}
