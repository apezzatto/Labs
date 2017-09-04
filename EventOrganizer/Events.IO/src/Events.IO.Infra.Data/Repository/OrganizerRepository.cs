using Events.IO.Domain.Organizers;
using Events.IO.Domain.Organizers.Repository;
using Events.IO.Infra.Data.Context;

namespace Events.IO.Infra.Data.Repository
{
    public class OrganizerRepository : Repository<Organizer>, IOrganizerRepository
    {
        public OrganizerRepository(ContextEvents context) : base(context)
        {
        }
    }
}