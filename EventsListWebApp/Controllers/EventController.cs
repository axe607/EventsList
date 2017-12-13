using EventsListBL.Providers;
using EventsListBL.Services;
using EventsListCommon.Models;
using EventsListWebApp.Models;
using log4net;
using System;
using System.Web.Mvc;

namespace EventsListWebApp.Controllers
{
    public class EventController : Controller
    {
        private readonly IBusinessProvider _provider;
        private readonly IEventOperation _eventOperation;
        private static readonly ILog Log = LogManager.GetLogger(typeof(EventController));
        private const string EVENTS_VIEW = "Events";

        public EventController(IBusinessProvider providerInput, IEventOperation eventOperation)
        {
            _provider = providerInput;
            _eventOperation = eventOperation;
        }

        public PartialViewResult EventsByCategory(int categoryId)
        {
            try
            {
                Log.Debug("Try get events by category Id");
                return PartialView(EVENTS_VIEW, _provider.GetEventsByCategoryId(categoryId));
            }
            catch (Exception ex)
            {
                Log.Error("[categoryId = " + categoryId + " ]; " + ex.Message);
                ViewBag.Error = ex.Message;
                return PartialView(EVENTS_VIEW);
            }
        }

        [Ajax]
        public PartialViewResult Events()
        {
            try
            {
                return PartialView(EVENTS_VIEW, _provider.GetEvents());
            }
            catch (Exception ex)
            {
                Log.Error("[Events][_provider.GetEvents()]; " + ex.Message);
                ViewBag.Error = ex.Message;
                return PartialView(EVENTS_VIEW);
            }
        }

        public PartialViewResult DetailEvent(int id)
        {
            try
            {
                return PartialView(_provider.GetEventInfoDetailById(id));
            }
            catch (Exception ex)
            {
                Log.Error("[DetailEvent][_provider.GetEventInfoDetailById(id)][id = " + id + " ]; " + ex.Message);
                ViewBag.Error = ex.Message;
                return PartialView(EVENTS_VIEW);
            }
        }

        [Admin]
        [HttpGet]
        public ViewResult CreateEvent()
        {
            return View(new Event());
        }

        [Admin]
        [HttpPost]
        public ViewResult CreateEvent(Event createdEvent)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _eventOperation.AddEvent(
                        createdEvent.Name,
                        createdEvent.Date,
                        createdEvent.OrganizerId,
                        createdEvent.CategoryId,
                        createdEvent.ImageUrl,
                        createdEvent.Description,
                        createdEvent.AddressId);

                    return View("CreateEvent");
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                }
            }

            return View(createdEvent);
        }

        [Admin]
        public void DeleteEvent(int eventId)
        {
            _eventOperation.DeleteEvent(eventId);
        }

        [HttpGet]
        [Admin]
        public ActionResult EditEvent(int eventId)
        {
            try
            {
                return View(_provider.GetEventById(eventId));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToAction("Index","Home");
            }
        }

        [HttpPost]
        [Admin]
        public ActionResult EditEvent(Event eventModel)
        {
            if (ModelState.IsValid)
            {
                _eventOperation.EditEvent(
                    eventModel.Id,
                    eventModel.Name,
                    eventModel.Date,
                    eventModel.CategoryId,
                    eventModel.ImageUrl,
                    eventModel.Description,
                    eventModel.AddressId);
                return RedirectToAction("Index", "Home");
            }
            return View(eventModel);
        }

        public PartialViewResult SearchBar()
        {
            return PartialView();
        }
        public PartialViewResult EventsBySearch(int? categoryId, DateTime? date, int? state)
        {
            try
            {
                return PartialView(EVENTS_VIEW, _provider.GetEventsBySearchData(categoryId, date, state));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                ViewBag.Error = ex.Message;
                return PartialView(EVENTS_VIEW);
            }
        }
    }
}