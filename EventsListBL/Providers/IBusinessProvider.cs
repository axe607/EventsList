using System;
using System.Collections.Generic;
using EventsListCommon.Models;

namespace EventsListBL.Providers
{
    public interface IBusinessProvider
    {
        IReadOnlyList<Event> GetEvents();
        IReadOnlyList<Event> GetEventsByCategoryId(int categoryId);
        IReadOnlyList<Event> GetEventsByUserId(int userId);
        IReadOnlyList<Event> GetEventsBySearchData(int? categoryId, DateTime? date, int? state);
        IReadOnlyList<Category> GetCategories();
        IReadOnlyList<Address> GetAddresses();
        Category GetCategoryById(int categoryId);
        Event GetEventById(int eventId);
        EventDetail GetEventInfoDetailById(int eventId);
        Address GetAddressById(int addressId);
    }
}
