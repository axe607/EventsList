﻿using EventsListCommon.Models;
using EventsListData.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace EventsListBL.Providers
{
    public class Provider : IProvider
    {
        IDataProvider _dataProvider;

        public Provider(IDataProvider provider)
        {
            _dataProvider = provider;
        }

        public IReadOnlyList<Subcategory> GetSubcategories()
        {
            return _dataProvider.GetSubcategories();
        }

        public Category GetCategoryBySubcategoryId(int id)
        {
            return _dataProvider.GetCategories().SingleOrDefault(x => x.Subcategories.Exists(z => z.Id == id));
        }

        public Subcategory GetSubcategoryBySubcategoryId(int id)
        {
            return _dataProvider.GetSubcategories().SingleOrDefault(x => x.Id == id);
        }

        public Organizer GetOrganizerById(int id)
        {
            return _dataProvider.GetOrganizers().SingleOrDefault(x => x.Id == id);
        }

        public IReadOnlyList<Event> GetEvents()
        {
            return _dataProvider.GetEvents();
        }

        public IReadOnlyList<Category> GetCategories()
        {
            return _dataProvider.GetCategories();
        }

        public IReadOnlyList<Event> GetEventsBySubcategoryId(int id)
        {
            return _dataProvider.GetEvents().Where(x=>x.SubcategoryId==id).ToList();
        }

        public IReadOnlyList<Event> GetEventsByCategoryId(int id)
        {
            return _dataProvider.GetEvents().Where(x =>
                _dataProvider.GetCategories().SingleOrDefault(z => z.Id == id).Subcategories.Exists(y => y.Id == x.SubcategoryId)).ToList();
        }

        public Event GetEventById(int id)
        {
            return _dataProvider.GetEvents().SingleOrDefault(x => x.Id == id);
        }
    }
}