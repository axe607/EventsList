using System.Collections.Generic;
using EventsListCommon.Models;

namespace EventsListBL.Providers
{
    public interface IProvider
    {
        IReadOnlyList<Event> GetEvents();
        IReadOnlyList<Event> GetEventsBySubcategoryId(int id);
        IReadOnlyList<Event> GetEventsByCategoryId(int id);
        Event GetEventById(int id);
        IReadOnlyList<Category> GetCategories();
        IReadOnlyList<Subcategory> GetSubcategories();
        Category GetCategoryBySubcategoryId(int id);
        Subcategory GetSubcategoryBySubcategoryId(int id);

        Organizer GetOrganizerById(int id);
    }
}
