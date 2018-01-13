using EventsListCommon.Models;
using System;
using System.Collections.Generic;

namespace EventsListData.Repositories
{
    public interface IDataRepository
    {
        IReadOnlyList<Event> GetEvents();
        IReadOnlyList<Event> GetEventsByCategoryId(int categoryId);
        IReadOnlyList<Event> GetEventsByUserId(int userId);
        IReadOnlyList<Event> GetEventsBySearchData(int? categoryId, DateTime? date, int? state);
        IReadOnlyList<Category> GetCategories();
        IReadOnlyList<Address> GetAddresses();
        IReadOnlyList<User> GetUsers();
        IReadOnlyList<Role> GetRoles();
        IReadOnlyList<Role> GetRolesNotInUser(string userName);
        Category GetCategoryById(int categoryId);
        EventDetail GetEventInfoDetailById(int eventId);
        Event GetEventById(int eventId);
        Role GetRolesById(int roleId);
        User GetUserByName(string name);
        Address GetAddressById(int addressId);
        bool IsValidUser(string username, string password);
        bool IsUserNameFreeForUserId(int userId, string name);
        bool IsUserNameFree(string name);
        bool IsRoleNameFree(int? roleId, string name);

        void AddEvent(string name, DateTime date, int? organizerId, int? categoryId, string imageUrl, string description, int? addressId);
        void EditEvent(int eventId, string name, DateTime date, int? categoryId, string imageUrl, string description, int? addressId);
        void EditEventByUserId(int eventId, int userId, string name, DateTime date, int? categoryId, string imageUrl, string description, int? addressId);
        void DeleteEvent(int eventId);
        void DeleteFutureEventByIdAndUserId(int eventId, int userId);

        void AddUser(string name, string password, string email);
        void AddRoleToUser(string userName, int roleId);
        void EditUserInfo(int userId, string name, string password, string email);
        void EditOrganizerInfo(int userId, string name);
        void DeleteUser(int userId);
        void DeleteUserRole(string userName, int roleId);

        void AddRole(string roleName);
        void EditRole(int roleId, string roleName);
        void DeleteRole(int roleId);

        void AddPhone(int userId, string phoneNumber);
        void AddEmail(int userId, string email);
        void DeletePhoneByUserIdAndPhoneId(int userId, int phoneId);
        void DeleteEmailByUserIdAndEmailId(int userId, int emailId);

        void AddAddress(string address);
        void EditAddress(int addressId, string address);
        void DeleteAddress(int addressId);

        void AddCategory(string categoryName, int? pid);
        void EditCategory(int categoryId, int? pid, string categoryName);
        void DeleteCategory(int categoryId);
    }
}
