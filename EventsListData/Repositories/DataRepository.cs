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

        public IReadOnlyList<Category> GetCategories()
        {
            return _client.GetCategories();
        }

        public IReadOnlyList<Event> GetEvents()
        {
            return _client.GetEvents();
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
