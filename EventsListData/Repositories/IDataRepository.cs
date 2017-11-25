using System.Collections.Generic;
using EventsListCommon.Models;

namespace EventsListData.Repositories
{
    public interface IDataRepository
    {
        IReadOnlyList<Event> GetEvents();
        IReadOnlyList<Event> GetEventsBySubcategoryId(int subcategoryId);
        IReadOnlyList<Event> GetEventsByCategoryId(int categoryId);
        EventDetail GetEventInfoDetailById(int eventId);
        IReadOnlyList<Category> GetCategories();
        IReadOnlyList<Subcategory> GetSubcategories();
        IReadOnlyList<Organizer> GetOrganizers();
        Organizer GetOrganizerById(int id);
        Address GetAddressById(int id);
    }
}
