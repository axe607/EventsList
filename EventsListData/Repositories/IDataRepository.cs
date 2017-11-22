using System.Collections.Generic;
using EventsListCommon.Models;

namespace EventsListData.Repositories
{
    public interface IDataRepository
    {
        IReadOnlyList<Event> GetEvents();
        IReadOnlyList<Category> GetCategories();
        IReadOnlyList<Subcategory> GetSubcategories();
        IReadOnlyList<Organizer> GetOrganizers();
    }
}
