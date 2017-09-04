using Events.IO.Domain.Core.AppEvents;

namespace Events.IO.Domain.Organizers.Events
{
    public class OrganizerHandlerEvent : IHandler<OrganizerRegisteredEvent>
    {
        public void Handle(OrganizerRegisteredEvent Message)
        {
            //TODO: Send Email
        }
    }
}