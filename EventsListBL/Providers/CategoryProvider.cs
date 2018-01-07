using EventsListBL.Providers.Interfaces;
using EventsListCommon.Models;
using EventsListData.Repositories;
using System.Collections.Generic;

namespace EventsListBL.Providers
{
    public class CategoryProvider:ICategoryProvider
    {
        private readonly IDataRepository _dataProvider;

        public CategoryProvider(IDataRepository dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public IReadOnlyList<Category> GetCategories()
        {
            return _dataProvider.GetCategories();
        }

        public Category GetCategoryById(int categoryId)
        {
            return _dataProvider.GetCategoryById(categoryId);
        }
    }
}
