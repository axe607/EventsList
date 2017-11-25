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

        public IReadOnlyList<Subcategory> GetSubcategories()
        {
            return _dataProvider.GetSubcategories();
        }

        public IReadOnlyList<Event> GetEvents()
        {
            return _dataProvider.GetEvents();
        }

        public IReadOnlyList<Category> GetCategories()
        {
            return _dataProvider.GetCategories();
        }

        public IReadOnlyList<Event> GetEventsBySubcategoryId(int subcategoryId)
        {
            return _dataProvider.GetEventsBySubcategoryId(subcategoryId);
        }

        public IReadOnlyList<Event> GetEventsByCategoryId(int categoryId)
        {
            return _dataProvider.GetEventsByCategoryId(categoryId);
        }
        
        public EventDetail GetEventInfoDetailById(int eventId)
        {
           return _dataProvider.GetEventInfoDetailById(eventId);
        }
    }
}
