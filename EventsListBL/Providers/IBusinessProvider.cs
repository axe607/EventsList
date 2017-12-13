using System;
using System.Collections.Generic;
using EventsListCommon.Models;

namespace EventsListBL.Providers
{
    public interface IBusinessProvider
    {
        IReadOnlyList<Event> GetEvents();
        IReadOnlyList<Event> GetEventsByCategoryId(int categoryId);
        IReadOnlyList<Event> GetEventsBySearchData(int? categoryId, DateTime? date, int? state);
        EventDetail GetEventInfoDetailById(int eventId);
        Event GetEventById(int eventId);
        IReadOnlyList<Category> GetCategories();
        IReadOnlyList<Address> GetAddresses();
    }
}
