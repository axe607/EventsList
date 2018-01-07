using EventsListCommon.Models;
using System.Collections.Generic;

namespace EventsListBL.Providers.Interfaces
{
    public interface ICategoryProvider
    {
        IReadOnlyList<Category> GetCategories();
        Category GetCategoryById(int categoryId);

    }
}
