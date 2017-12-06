using System;
using EventsListCommon.Models;
using EventsListData.Repositories;
using System.Collections.Generic;

namespace EventsListBL.Providers
{
    public class BusinessProvider : IBusinessProvider
    {
        IDataRepository _dataProvider;

        public BusinessProvider(IDataRepository provider)
        {
            _dataProvider = provider;
        }

        public IReadOnlyList<Event> GetEvents()
        {
            return _dataProvider.GetEvents();
        }

        public IReadOnlyList<Category> GetCategories()
        {
            return _dataProvider.GetCategories();
        }

        public IReadOnlyList<Organizer> GetOrganizers()
        {
           return _dataProvider.GetOrganizers();
        }

        public void AddEvent(string name, DateTime date, int organizerId, int categoryId, string imageUrl, string description,
            int addressId)
        {
            _dataProvider.AddEvent(name, date, organizerId, categoryId, imageUrl, description, addressId);
        }

        public IReadOnlyList<Event> GetEventsByCategoryId(int categoryId)
        {
            return _dataProvider.GetEventsByCategoryId(categoryId);
        }

        public IReadOnlyList<Event> GetEventsBySearchData(int? categoryId, DateTime? date, int? state)
        {
            return _dataProvider.GetEventsBySearchData(categoryId, date, state);
        }
        

        public EventDetail GetEventInfoDetailById(int eventId)
        {
           return _dataProvider.GetEventInfoDetailById(eventId);
        }
    }
}
