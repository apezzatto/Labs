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

        [Authorize]
        public IActionResult MyEvents()
        {
            return View(_eventAppService.GetEventByOrganizer(OrganizerId));
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
                return NotFound();

            if (ValidateEventAuthority(eventViewModel))
                return RedirectToAction("MyEvents", _eventAppService.GetEventByOrganizer(OrganizerId));

            return View(eventViewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EventViewModel eventViewModel)
        {
            if (ValidateEventAuthority(eventViewModel))
                return RedirectToAction("MyEvents", _eventAppService.GetEventByOrganizer(OrganizerId));

            if (!ModelState.IsValid) return View(eventViewModel);

            eventViewModel.OrganizerId = OrganizerId;

            _eventAppService.Update(eventViewModel);

            ViewBag.OperationResult = ValidOperation() ? "success,Event update successfully" : "error,Event update failed. Check messages for details.";

            if (_eventAppService.GetById(eventViewModel.Id).Online)
                eventViewModel.Address = null;
            else
                eventViewModel = _eventAppService.GetById(eventViewModel.Id);

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
            if (ValidateEventAuthority(_eventAppService.GetById(id)))
                return RedirectToAction("MyEvents", _eventAppService.GetEventByOrganizer(OrganizerId));

            _eventAppService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddAddress(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventViewModel = _eventAppService.GetById(id.Value);
            return PartialView("_AddAddress", eventViewModel);
        }

        public IActionResult UpdateAddress(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventViewModel = _eventAppService.GetById(id.Value);
            return PartialView("_UpdateAddress", eventViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAddress(EventViewModel eventViewModel)
        {
            ModelState.Clear();
            eventViewModel.Address.EventId = eventViewModel.Id;

            _eventAppService.AddAddress(eventViewModel.Address);

            if (ValidOperation())
            {
                string url = Url.Action("GetAddress", "Events", new {id = eventViewModel.Id});
                return Json(new {success = true, url = url});
            }

            return PartialView("_AddAddress", eventViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateAddress(EventViewModel eventViewModel)
        {
            ModelState.Clear();
            _eventAppService.UpdateAddress(eventViewModel.Address);

            if (ValidOperation())
            {
                string url = Url.Action("GetAddress", "Events", new { id = eventViewModel.Id });
                return Json(new { success = true, url = url });
            }

            return PartialView("_UpdateAddress", eventViewModel);
        }

        public IActionResult GetAddress(Guid id)
        {
            return PartialView("_AddressDetails", _eventAppService.GetById(id));
        }

        private bool ValidateEventAuthority(EventViewModel eventViewModel)
        {
            return (eventViewModel.OrganizerId != OrganizerId);
        }
    }
}
