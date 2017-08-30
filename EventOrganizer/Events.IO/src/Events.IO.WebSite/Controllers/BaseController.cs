using Microsoft.AspNetCore.Mvc;
using Events.IO.Domain.Core.Notifications;

namespace Events.IO.WebSite.Controllers
{
    public class BaseController : Controller
    {
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;

        public BaseController(IDomainNotificationHandler<DomainNotification> notifications)
        {
            _notifications = notifications;
        }

        protected bool ValidOperation()
        {
            return (!_notifications.HasNotifications());
        }
    }
}
