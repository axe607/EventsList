using EventsListBL.Services.Interfaces;
using EventsListData.Repositories;

namespace EventsListBL.Services
{
    public class CategoryOperation : ICategoryOperation
    {
        private readonly IDataRepository _dataProvider;

        public CategoryOperation(IDataRepository dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void AddCategory(string categoryName, int? pid)
        {
            _dataProvider.AddCategory(categoryName, pid);
        }

        public void EditCategory(int categoryId, int? pid, string categoryName)
        {
            _dataProvider.EditCategory(categoryId, pid, categoryName);
        }

        public void DeleteCategory(int categoryId)
        {
            _dataProvider.DeleteCategory(categoryId);
        }
    }
}
