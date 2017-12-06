using System;
using System.Collections.Generic;
using EventsListCommon.Models;

namespace EventsListData.Clients
{
    public interface IClient
    {
        IReadOnlyList<Event> GetEvents();
        IReadOnlyList<Event> GetEventsByCategoryId(int categoryId);
        IReadOnlyList<Event> GetEventsBySearchData(int? categoryId, DateTime? date, int? state);
        EventDetail GetEventInfoDetailById(int eventId);
        IReadOnlyList<Category> GetCategories();
        IReadOnlyList<Organizer> GetOrganizers();
        Organizer GetOrganizerById(int id);
        Address GetAddressById(int id);
        User GetUserByName(string name);
        bool IsValidUser(string username, string password);

        void AddEvent(string name, DateTime date, int organizerId, int categoryId, string imageUrl, string description, int addressId);
    }
}
