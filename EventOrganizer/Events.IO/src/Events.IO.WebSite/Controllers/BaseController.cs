using System;
using Microsoft.AspNetCore.Mvc;
using Events.IO.Domain.Core.Notifications;
using Events.IO.Domain.Interfaces;

namespace Events.IO.WebSite.Controllers
{
    public class BaseController : Controller
    {
        private readonly IDomainNotificationHandler<DomainNotification> _notifications;
        private readonly IUser _user;
        public Guid OrganizerId { get; set; }

        public BaseController(IDomainNotificationHandler<DomainNotification> notifications, 
            IUser user)
        {
            _notifications = notifications;
            _user = user;

            if (user.IsAuthenticated())
            {
                OrganizerId = _user.GetUserId();
            }
        }

        protected bool ValidOperation()
        {
            return (!_notifications.HasNotifications());
        }
    }
}
