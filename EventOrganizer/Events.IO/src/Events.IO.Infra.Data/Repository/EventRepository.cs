using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Events.IO.Domain.Events;
using Events.IO.Domain.Events.Repository;
using Events.IO.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Events.IO.Infra.Data.Repository
{
    public class EventRepository : Repository<Event>, IEventRepository
    {

        public EventRepository(ContextEvents context) 
            : base(context)
        {
            
        }
        public void AddAddress(Address address)
        {
            Db.Addresses.Add(address);
        }

        public Address GetAddressById(Guid id)
        {
            return Db.Addresses.Find(id);
        }

        public IEnumerable<Event> GetEventByOrganizer(Guid idOrganizer)
        {
            return Db.Events.Where(e => e.OrganizerId == idOrganizer);
        }

        public override Event GetById(Guid id)
        {
            return Db.Events.Include(e => e.Address).FirstOrDefault(e => e.Id == id);
        }

        public void UpdateAddress(Address address)
        {
            Db.Addresses.Update(address);
        }
    }
}
