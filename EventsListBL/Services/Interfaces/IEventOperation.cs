using System;

namespace EventsListBL.Services.Interfaces
{
    public interface IEventOperation
    {
        void AddEvent(string name, DateTime date, int? organizerId, int? categoryId, string imageUrl, string description, int? addressId);
        void EditEvent(int eventId, string name, DateTime date, int? categoryId, string imageUrl, string description, int? addressId);
        void EditEventByUserId(int eventId, int userId, string name, DateTime date, int? categoryId, string imageUrl, string description, int? addressId);
        void DeleteEvent(int eventId);
        void DeleteFutureEventByIdAndUserId(int eventId, int userId);
    }
}
