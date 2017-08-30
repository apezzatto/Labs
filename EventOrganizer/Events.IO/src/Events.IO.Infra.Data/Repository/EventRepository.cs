using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
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

        public override IEnumerable<Event> GetAll()
        {
            var sql = "SELECT * FROM Events E " +
                      "WHERE E.Excluded = 0" +
                      "ORDER BY E.EndDate DESC";

            return Db.Database.GetDbConnection().Query<Event>(sql);
        }

        public void AddAddress(Address address)
        {
            Db.Addresses.Add(address);
        }

        public Address GetAddressById(Guid id)
        {
            //return Db.Addresses.Find(id);

            var sql = "SELECT * FROM Address A " +
                "WHERE A.Id = @aid";

            var address = Db.Database.GetDbConnection().Query<Address>(sql, 
                new { aid = id });

            return address.SingleOrDefault();
        }

        public IEnumerable<Event> GetEventByOrganizer(Guid organizerId)
        {
            //return Db.Events.Where(e => e.OrganizerId == organizerId);

            var sql = "SELECT * FROM Events E " +
                "WHERE E.Excluded = 0 " +
                "AND E.OrganizerId = @oid " +
                "ORDER BY E.EndDate Desc";

            return Db.Database.GetDbConnection().Query<Event>(sql, 
                new { oid = organizerId });
        }

        public override Event GetById(Guid id)
        {
            //EF Implementation
            //return Db.Events.Include(e => e.Address).FirstOrDefault(e => e.Id == id);

            //Dapper Implementation
            var sql = @"SELECT * FROM Events E " +
                "LEFT JOIN Addresses A " +
                "ON E.Id = A.EventId " +
                "WHERE E.Id = @uid";

            var @event = Db.Database.GetDbConnection().Query<Event, Address, Event>(sql,
                (e, a) =>
                {
                    if (a != null)
                        e.SetAddress(a);

                    return e;
                }, new { uid = id });

            return @event.FirstOrDefault();
        }

        public void UpdateAddress(Address address)
        {
            Db.Addresses.Update(address);
        }
    }
}
