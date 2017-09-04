using System;
using Events.IO.Application.ViewModels;

namespace Events.IO.Application.Interfaces
{
    public interface IOrganizerAppService : IDisposable
    {
        void Register(OrganizerViewModel organizerViewModel);
    }
}