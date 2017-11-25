using EventsListCommon.Models;
using EventsListData.Clients;
using System;
using System.Collections.Generic;

namespace EventsListData.Repositories
{
    public class DataRepository : IDataRepository
    {
        IClient _client;

        public DataRepository(IClient client)
        {
            _client = client;
        }

        public Address GetAddressById(int id)
        {
            return _client.GetAddressById(id);
        }

        public IReadOnlyList<Category> GetCategories()
        {
            return _client.GetCategories();
        }

        public IReadOnlyList<Event> GetEvents()
        {
            return _client.GetEvents();
        }

        public IReadOnlyList<Event> GetEventsBySubcategoryId(int subcategoryId)
        {
            return _client.GetEventsBySubcategoryId(subcategoryId);
        }

        public IReadOnlyList<Event> GetEventsByCategoryId(int categoryId)
        {
            return _client.GetEventsByCategoryId(categoryId);
        }

        public EventDetail GetEventInfoDetailById(int eventId)
        {
           return _client.GetEventInfoDetailById(eventId);
        }

        public Organizer GetOrganizerById(int id)
        {
            return _client.GetOrganizerById(id);
        }

        public IReadOnlyList<Organizer> GetOrganizers()
        {
            return _client.GetOrganizers();
        }

        public IReadOnlyList<Subcategory> GetSubcategories()
        {
            return _client.GetSubcategories();
        }

    }
}
