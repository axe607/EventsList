using EventsListBL.Providers;
using log4net;
using System;
using System.Web.Mvc;

namespace EventsListWebApp.Controllers
{
    public class EventController : Controller
    {
        private readonly IBusinessProvider _provider;
        private static readonly ILog Log = LogManager.GetLogger("HomeController");
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
                Log.Error("[EventsByCategory][_provider.GetEventsByCategoryId(categoryId)][categoryId = "+ categoryId + " ]; "+ex.Message);
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
    }
}