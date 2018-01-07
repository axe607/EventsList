using EventsListBL.Providers.Interfaces;
using EventsListBL.Services.Interfaces;
using EventsListCommon.Models;
using EventsListWebApp.Models;
using log4net;
using System;
using System.Web.Mvc;

namespace EventsListWebApp.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressProvider _addressProvider;
        private readonly IAddressOperation _addressOperation;
        private static readonly ILog Log = LogManager.GetLogger(typeof(AddressController));

        public AddressController(IAddressProvider addressProvider, IAddressOperation addressOperation)
        {
            _addressProvider = addressProvider;
            _addressOperation = addressOperation;
        }

        [AllowTo(Roles = "Admin,Editor")]
        public ActionResult Index()
        {
            return View(_addressProvider.GetAddresses());
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
                    _addressOperation.AddAddress(createdAddress.AddressString);
                    return RedirectToAction("Index");
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
            return View(_addressProvider.GetAddressById(addressId));
        }

        [AllowTo(Roles = "Admin,Editor")]
        [HttpPost]
        public ActionResult EditAddress(Address editedAddress)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _addressOperation.EditAddress(
                        editedAddress.Id,
                        editedAddress.AddressString
                    );
                    return RedirectToAction("Index");
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
            _addressOperation.DeleteAddress(addressId);
            return RedirectToAction("Index");
        }
    }
}