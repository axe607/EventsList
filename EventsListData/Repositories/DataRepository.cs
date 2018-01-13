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

        public IReadOnlyList<User> GetUsers()
        {
            return _client.GetUsers();
        }

        public IReadOnlyList<Role> GetRoles()
        {
            return _client.GetRoles();
        }

        public Role GetRolesById(int roleId)
        {
            return _client.GetRolesById(roleId);
        }

        public User GetUserByName(string name)
        {
            return _client.GetUserByName(name);
        }

        public Address GetAddressById(int addressId)
        {
            return _client.GetAddressById(addressId);
        }

        public IReadOnlyList<Role> GetRolesNotInUser(string userName)
        {
            return _client.GetRolesNotInUser(userName);
        }

        public Category GetCategoryById(int categoryId)
        {
            return _client.GetCategoryById(categoryId);
        }

        public bool IsValidUser(string username, string password)
        {
            return _client.IsValidUser(username, password);
        }

        public bool IsUserNameFreeForUserId(int userId, string name)
        {
            return _client.IsUserNameFreeForUserId(userId, name);
        }

        public bool IsUserNameFree(string name)
        {
            return _client.IsUserNameFree(name);
        }

        public bool IsRoleNameFree(int? roleId, string name)
        {
            return _client.IsRoleNameFree(roleId, name);
        }

        public void AddEvent(string name, DateTime date, int? organizerId, int? categoryId, string imageUrl, string description,
            int? addressId)
        {
            _client.AddEvent(name, date, organizerId, categoryId, imageUrl, description, addressId);
        }

        public void EditEventByUserId(int eventId, int userId, string name, DateTime date, int? categoryId, string imageUrl,
            string description, int? addressId)
        {
            _client.EditEventByUserId(eventId, userId, name, date, categoryId, imageUrl, description, addressId);
        }

        public void DeleteEvent(int eventId)
        {
            _client.DeleteEvent(eventId);
        }

        public void DeleteFutureEventByIdAndUserId(int eventId, int userId)
        {
            _client.DeleteFutureEventByIdAndUserId(eventId, userId);
        }

        public void EditEvent(int eventId, string name, DateTime date, int? categoryId, string imageUrl,
            string description, int? addressId)
        {
            _client.EditEvent(eventId, name, date, categoryId, imageUrl, description, addressId);
        }

        public void AddUser(string name, string password, string email)
        {
            _client.AddUser(name, password, email);
        }

        public void EditUserInfo(int userId, string name, string password, string email)
        {
            _client.EditUserInfo(userId, name, password, email);
        }

        public void EditOrganizerInfo(int userId, string name)
        {
            _client.EditOrganizerInfo(userId,name);
        }

        public void DeleteUser(int userId)
        {
            _client.DeleteUser(userId);
        }

        public void AddRoleToUser(string userName, int roleId)
        {
            _client.AddRoleToUser(userName, roleId);
        }

        public void DeleteUserRole(string userName, int roleId)
        {
            _client.DeleteUserRole(userName, roleId);
        }

        public void AddRole(string roleName)
        {
            _client.AddRole(roleName);
        }

        public void EditRole(int roleId, string roleName)
        {
            _client.EditRole(roleId, roleName);
        }

        public void DeleteRole(int roleId)
        {
            _client.DeleteRole(roleId);
        }

        public void AddPhone(int userId, string phoneNumber)
        {
            _client.AddPhone(userId, phoneNumber);
        }

        public void AddEmail(int userId, string email)
        {
            _client.AddEmail(userId, email);
        }

        public void DeleteEmailByUserIdAndEmailId(int userId, int emailId)
        {
            _client.DeleteEmailByUserIdAndEmailId(userId, emailId);
        }

        public void DeletePhoneByUserIdAndPhoneId(int userId, int phoneId)
        {
            _client.DeletePhoneByUserIdAndPhoneId(userId, phoneId);
        }

        public void AddAddress(string address)
        {
            _client.AddAddress(address);
        }

        public void EditAddress(int addressId, string address)
        {
            _client.EditAddress(addressId, address);
        }

        public void DeleteAddress(int addressId)
        {
            _client.DeleteAddress(addressId);
        }

        public void AddCategory(string categoryName, int? pid)
        {
            _client.AddCategory(categoryName, pid);
        }

        public void EditCategory(int categoryId, int? pid, string categoryName)
        {
            _client.EditCategory(categoryId, pid, categoryName);
        }

        public void DeleteCategory(int categoryId)
        {
            _client.DeleteCategory(categoryId);
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

        public IReadOnlyList<Event> GetEventsByUserId(int userId)
        {
            return _client.GetEventsByUserId(userId);
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
