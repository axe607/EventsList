using System;
using System.Collections.Generic;
using EventsListCommon.Models;

namespace EventsListBL.Providers.Interfaces
{
    public interface IEventProvider
    {
        IReadOnlyList<Event> GetEvents();
        IReadOnlyList<Event> GetEventsByCategoryId(int categoryId);
        IReadOnlyList<Event> GetEventsByUserId(int userId);
        IReadOnlyList<Event> GetEventsBySearchData(int? categoryId, DateTime? date, int? state);
        Event GetEventById(int eventId);
        EventDetail GetEventInfoDetailById(int eventId);
    }
}
