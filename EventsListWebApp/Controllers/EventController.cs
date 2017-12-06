using EventsListBL.Providers;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EventsListCommon.Models;
using EventsListWebApp.Models;

namespace EventsListWebApp.Controllers
{
    public class EventController : Controller
    {
        private readonly IBusinessProvider _provider;
        private static readonly ILog Log = LogManager.GetLogger(typeof(EventController));
        private const string EVENTS_VIEW = "Events";

        public EventController(IBusinessProvider providerInput)
        {
            _provider = providerInput;
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

        [Admin]
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
                    _provider.AddEvent(
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

        public PartialViewResult SearchBar()
        {
            return PartialView();
        }
        public PartialViewResult EventsBySearch(int? categoryId, DateTime? date, int? state)
        {
            try
            {
                return PartialView(EVENTS_VIEW,_provider.GetEventsBySearchData(categoryId,date,state));
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