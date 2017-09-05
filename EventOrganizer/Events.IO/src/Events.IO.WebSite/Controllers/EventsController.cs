using System;
using Microsoft.AspNetCore.Mvc;
using Events.IO.Application.ViewModels;
using Events.IO.Application.Interfaces;
using Events.IO.Domain.Core.Notifications;
using Events.IO.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Events.IO.WebSite.Controllers
{
    public class EventsController : BaseController
    {
        private readonly IEventAppService _eventAppService;

        public EventsController(IEventAppService eventAppService, 
                                IDomainNotificationHandler<DomainNotification> notifications,
                                IUser user) : base(notifications, user)
        {
            _eventAppService = eventAppService;
        }

        public IActionResult Index()
        {
            return View(_eventAppService.GetAll());
        }

        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventViewModel = _eventAppService.GetById(id.Value);
            if (eventViewModel == null)
            {
                return NotFound();
            }

            return View(eventViewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EventViewModel eventViewModel)
        {
            if (!ModelState.IsValid) return View(eventViewModel);

            eventViewModel.OrganizerId = OrganizerId;

            _eventAppService.Register(eventViewModel);

            ViewBag.OperationResult = ValidOperation() ? "success,Event registration successfully" : "error,Event registration failed. Check messages for details.";

            return View(eventViewModel);
        }

        [Authorize]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventViewModel = _eventAppService.GetById(id.Value);

            if (eventViewModel == null)
            {
                return NotFound();
            }
            return View(eventViewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EventViewModel eventViewModel)
        {
            if (!ModelState.IsValid) return View(eventViewModel);

            _eventAppService.Update(eventViewModel);

            ViewBag.OperationResult = ValidOperation() ? "success,Event update successfully" : "error,Event update failed. Check messages for details.";

            return View(eventViewModel);
        }

        [Authorize]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventViewModel = _eventAppService.GetById(id.Value);
                
            if (eventViewModel == null)
            {
                return NotFound();
            }

            return View(eventViewModel);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _eventAppService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
