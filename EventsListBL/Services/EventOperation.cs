using System;
using EventsListData.Repositories;

namespace EventsListBL.Services
{
    public class EventOperation : IEventOperation
    {
        readonly IDataRepository _dataProvider;

        public EventOperation(IDataRepository provider)
        {
            _dataProvider = provider;
        }

        public void AddEvent(string name, DateTime date, int? organizerId, int? categoryId, string imageUrl, string description,
            int? addressId)
        {
            _dataProvider.AddEvent(name, date, organizerId, categoryId, imageUrl, description, addressId);
        }

        public void AddAddress(string address)
        {
            _dataProvider.AddAddress(address);
        }

        public void AddCategory(string categoryName, int? pid)
        {
            _dataProvider.AddCategory(categoryName, pid);
        }

        public void EditEventByUserId(int eventId, int userId, string name, DateTime date, int? categoryId, string imageUrl,
            string description, int? addressId)
        {
            _dataProvider.EditEventByUserId(eventId, userId, name, date, categoryId, imageUrl, description, addressId);
        }

        public void EditCategory(int categoryId, int? pid, string categoryName)
        {
            _dataProvider.EditCategory(categoryId, pid, categoryName);
        }

        public void EditAddress(int addressId, string address)
        {
            _dataProvider.EditAddress(addressId, address);
        }

        public void DeleteEvent(int eventId)
        {
            _dataProvider.DeleteEvent(eventId);
        }

        public void DeleteFutureEventByIdAndUserId(int eventId, int userId)
        {
            _dataProvider.DeleteFutureEventByIdAndUserId(eventId, userId);
        }

        public void DeleteAddress(int addressId)
        {
            _dataProvider.DeleteAddress(addressId);
        }

        public void DeleteCategory(int categoryId)
        {
            _dataProvider.DeleteCategory(categoryId);
        }

        public void EditEvent(int eventId, string name, DateTime date, int? categoryId, string imageUrl, string description,
            int? addressId)
        {
            _dataProvider.EditEvent(eventId, name, date, categoryId, imageUrl, description, addressId);
        }
    }
}
