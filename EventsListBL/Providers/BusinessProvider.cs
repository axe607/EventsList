using System;
using EventsListCommon.Models;
using EventsListData.Repositories;
using System.Collections.Generic;

namespace EventsListBL.Providers
{
    public class BusinessProvider : IBusinessProvider
    {
        readonly IDataRepository _dataProvider;

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
        
        public IReadOnlyList<Address> GetAddresses()
        {
            return _dataProvider.GetAddresses();
        }

        public IReadOnlyList<Event> GetEventsByCategoryId(int categoryId)
        {
            return _dataProvider.GetEventsByCategoryId(categoryId);
        }

        public IReadOnlyList<Event> GetEventsBySearchData(int? categoryId, DateTime? date, int? state)
        {
            return _dataProvider.GetEventsBySearchData(categoryId, date, state);
        }

        public Event GetEventById(int eventId)
        {
            return _dataProvider.GetEventById(eventId);
        }

        public EventDetail GetEventInfoDetailById(int eventId)
        {
           return _dataProvider.GetEventInfoDetailById(eventId);
        }
    }
}
