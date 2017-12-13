using System;

namespace EventsListBL.Services
{
    public interface IEventOperation
    {
        void AddEvent(string name, DateTime date, int organizerId, int categoryId, string imageUrl, string description, int addressId);
        void DeleteEvent(int eventId);
        void EditEvent(int eventId, string name, DateTime date, int categoryId, string imageUrl, string description, int addressId);
    }
}
