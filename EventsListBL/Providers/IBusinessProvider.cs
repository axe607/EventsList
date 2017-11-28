﻿using System.Collections.Generic;
using EventsListCommon.Models;

namespace EventsListBL.Providers
{
    public interface IBusinessProvider
    {
        IReadOnlyList<Event> GetEvents();
        IReadOnlyList<Event> GetEventsBySubcategoryId(int subcategoryId);
        IReadOnlyList<Event> GetEventsByCategoryId(int categoryId);
        EventDetail GetEventInfoDetailById(int eventId);
        IReadOnlyList<Category> GetCategories();
        IReadOnlyList<Subcategory> GetSubcategories();
    }
}