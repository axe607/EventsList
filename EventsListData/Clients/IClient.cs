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
        Event GetEventById(int eventId);
        IReadOnlyList<Category> GetCategories();
        IReadOnlyList<Address> GetAddresses();
        User GetUserByName(string name);
        bool IsValidUser(string username, string password);
        bool IsNameFree(int userId, string name);

        void AddEvent(string name, DateTime date, int organizerId, int categoryId, string imageUrl, string description, int addressId);
        void DeleteEvent(int eventId);
        void EditEvent(int eventId, string name, DateTime date, int categoryId, string imageUrl, string description, int addressId);

        void AddUser(string name, string password, string email);
        void EditUserInfo(int userId, string name, string email);
        void DeleteUser(int userId);
    }
}
