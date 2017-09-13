using System;
using System.Collections.Generic;
using System.Text;
using Events.IO.Application.ViewModels;

namespace Events.IO.Application.Interfaces
{
    public interface IEventAppService : IDisposable
    {
        void Register(EventViewModel eventViewModel);
        IEnumerable<EventViewModel> GetAll();
        IEnumerable<EventViewModel> GetEventByOrganizer(Guid organizerId);
        EventViewModel GetById(Guid id);
        void Update(EventViewModel eventViewModel);
        void Delete(Guid id);
        void AddAddress(AddressViewModel addressViewModel);
        void UpdateAddress(AddressViewModel addressViewModel);
        AddressViewModel GetAddressById(Guid Id);
    }
}
