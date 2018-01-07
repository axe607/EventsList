using EventsListBL.Services.Interfaces;
using EventsListData.Repositories;
using System;

namespace EventsListBL.Services
{
    public class EventOperation : IEventOperation
    {
        private readonly IDataRepository _dataProvider;

        public EventOperation(IDataRepository provider)
        {
            _dataProvider = provider;
        }

        public void AddEvent(string name, DateTime date, int? organizerId, int? categoryId, string imageUrl, string description,
            int? addressId)
        {
            _dataProvider.AddEvent(name, date, organizerId, categoryId, imageUrl, description, addressId);
        }

        public void EditEvent(int eventId, string name, DateTime date, int? categoryId, string imageUrl, string description,
            int? addressId)
        {
            _dataProvider.EditEvent(eventId, name, date, categoryId, imageUrl, description, addressId);
        }

        public void EditEventByUserId(int eventId, int userId, string name, DateTime date, int? categoryId, string imageUrl,
            string description, int? addressId)
        {
            _dataProvider.EditEventByUserId(eventId, userId, name, date, categoryId, imageUrl, description, addressId);
        }

        public void DeleteEvent(int eventId)
        {
            _dataProvider.DeleteEvent(eventId);
        }

        public void DeleteFutureEventByIdAndUserId(int eventId, int userId)
        {
            _dataProvider.DeleteFutureEventByIdAndUserId(eventId, userId);
        }
    }
}
