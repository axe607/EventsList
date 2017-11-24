using System.Collections.Generic;
using EventsListCommon.Models;

namespace EventsListData.Clients
{
    public interface IClient
    {
        IReadOnlyList<Event> GetEvents();
        IReadOnlyList<Category> GetCategories();
        IReadOnlyList<Subcategory> GetSubcategories();
        IReadOnlyList<Organizer> GetOrganizers();
        Organizer GetOrganizerById(int id);
        Address GetAddressById(int id);
    }
}
