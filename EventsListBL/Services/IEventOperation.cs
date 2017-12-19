using System;

namespace EventsListBL.Services
{
    public interface IEventOperation
    {
        void AddEvent(string name, DateTime date, int? organizerId, int? categoryId, string imageUrl, string description, int? addressId);
        void AddAddress(string address);
        void AddCategory(string categoryName, int? pid);
        void EditEvent(int eventId, string name, DateTime date, int? categoryId, string imageUrl, string description, int? addressId);
        void EditEventByUserId(int eventId, int userId, string name, DateTime date, int? categoryId, string imageUrl, string description, int? addressId);
        void EditCategory(int categoryId, int? pid, string categoryName);
        void EditAddress(int addressId, string address);
        void DeleteEvent(int eventId);
        void DeleteFutureEventByIdAndUserId(int eventId, int userId);
        void DeleteAddress(int addressId);
        void DeleteCategory(int categoryId);
    }
}
