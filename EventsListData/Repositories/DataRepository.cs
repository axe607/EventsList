using EventsListCommon.Models;
using EventsListData.Clients;
using System;
using System.Collections.Generic;

namespace EventsListData.Repositories
{
    public class DataRepository : IDataRepository
    {
        readonly IClient _client;

        public DataRepository(IClient client)
        {
            _client = client;
        }

        public User GetUserByName(string name)
        {
            return _client.GetUserByName(name);
        }

        public bool IsValidUser(string username, string password)
        {
            return _client.IsValidUser(username, password);
        }

        public bool IsNameFree(int userId, string name)
        {
            return _client.IsNameFree(userId, name);
        }

        public void AddEvent(string name, DateTime date, int organizerId, int categoryId, string imageUrl, string description,
            int addressId)
        {
            _client.AddEvent(name, date, organizerId, categoryId, imageUrl, description, addressId);
        }

        public void DeleteEvent(int eventId)
        {
            _client.DeleteEvent(eventId);
        }

        public void EditEvent(int eventId, string name, DateTime date, int categoryId, string imageUrl,
            string description, int addressId)
        {
            _client.EditEvent(eventId, name, date, categoryId, imageUrl, description, addressId);
        }

        public void AddUser(string name, string password, string email)
        {
            _client.AddUser(name, password, email);
        }

        public void EditUserInfo(int userId, string name, string email)
        {
            _client.EditUserInfo(userId, name, email);
        }

        public void DeleteUser(int userId)
        {
            _client.DeleteUser(userId);
        }

        public Event GetEventById(int eventId)
        {
            return _client.GetEventById(eventId);
        }

        public IReadOnlyList<Category> GetCategories()
        {
            return _client.GetCategories();
        }

        public IReadOnlyList<Address> GetAddresses()
        {
            return _client.GetAddresses();
        }

        public IReadOnlyList<Event> GetEvents()
        {
            return _client.GetEvents();
        }

        public IReadOnlyList<Event> GetEventsByCategoryId(int categoryId)
        {
            return _client.GetEventsByCategoryId(categoryId);
        }

        public IReadOnlyList<Event> GetEventsBySearchData(int? categoryId, DateTime? date, int? state)
        {
            return _client.GetEventsBySearchData(categoryId, date, state);
        }

        public EventDetail GetEventInfoDetailById(int eventId)
        {
            return _client.GetEventInfoDetailById(eventId);
        }


    }
}
