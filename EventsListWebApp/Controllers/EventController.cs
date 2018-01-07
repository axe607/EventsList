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
        private const string EDIT_EVENT_VIEW = "EditEvent";

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
                Log.Error(ex.Message);
                ViewBag.Error = ex.Message;
                return PartialView(EVENTS_VIEW);
            }
        }

        [Ajax]
        [Authorize]
        public PartialViewResult MyEvents()
        {
            try
            {
                return PartialView(_provider.GetEventsByUserId(((UserPrincipal)HttpContext.User).UserId));
            }
            catch (Exception ex)
            {
                Log.Error("[Current userId = " + ((UserPrincipal)HttpContext.User).UserId + " ]; " + ex.Message);
                ViewBag.Error = ex.Message;
                return PartialView();
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
                Log.Error("[id = " + id + " ]; " + ex.Message);
                ViewBag.Error = ex.Message;
                return PartialView(EVENTS_VIEW);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateEvent()
        {
            return View(new Event());
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateEvent(Event createdEvent)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _eventOperation.AddEvent(
                        createdEvent.Name,
                        createdEvent.Date,
                        ((UserPrincipal)HttpContext.User).UserId,
                        createdEvent.CategoryId,
                        createdEvent.ImageUrl,
                        createdEvent.Description,
                        createdEvent.AddressId);

                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                }
            }

            return View(createdEvent);
        }

        [Admin]
        public RedirectToRouteResult DeleteEvent(int eventId)
        {
            _eventOperation.DeleteEvent(eventId);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public RedirectToRouteResult DeleteMyEvent(int eventId)
        {
            _eventOperation.DeleteFutureEventByIdAndUserId(eventId, ((UserPrincipal)HttpContext.User).UserId);
            return RedirectToAction("Index", "Account");
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
                return RedirectToAction("Index", "Home");
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

        [HttpGet]
        [Authorize]
        public ActionResult EditMyEvent(int eventId)
        {
            try
            {
                return View(EDIT_EVENT_VIEW, _provider.GetEventById(eventId));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return RedirectToAction("Index", "Account");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditMyEvent(Event eventModel)
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
                return RedirectToAction("Index", "Account");
            }
            return View(EDIT_EVENT_VIEW, eventModel);
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

        [AllowTo(Roles = "Admin,Editor")]
        public ActionResult AddressesList()
        {
            return View(_provider.GetAddresses());
        }

        [AllowTo(Roles = "Admin,Editor")]
        [HttpGet]
        public ActionResult AddAddress()
        {
            return View(new Address());
        }

        [AllowTo(Roles = "Admin,Editor")]
        [HttpPost]
        public ActionResult AddAddress(Address createdAddress)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _eventOperation.AddAddress(createdAddress.AddressString);
                    return RedirectToAction("AddressesList");
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                }
            }
            return View(createdAddress);
        }

        [AllowTo(Roles = "Admin,Editor")]
        [HttpGet]
        public ActionResult EditAddress(int addressId)
        {
            return View(_provider.GetAddressById(addressId));
        }

        [AllowTo(Roles = "Admin,Editor")]
        [HttpPost]
        public ActionResult EditAddress(Address editedAddress)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _eventOperation.EditAddress(
                        editedAddress.Id,
                        editedAddress.AddressString
                    );
                    return RedirectToAction("AddressesList");
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                }
            }
            return View(editedAddress);
        }

        [AllowTo(Roles = "Admin,Editor")]
        public RedirectToRouteResult DeleteAddress(int addressId)
        {
            _eventOperation.DeleteAddress(addressId);
            return RedirectToAction("AddressesList");
        }
    }
}