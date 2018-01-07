namespace EventsListBL.Services.Interfaces
{
    public interface ICategoryOperation
    {
        void AddCategory(string categoryName, int? pid);
        void EditCategory(int categoryId, int? pid, string categoryName);
        void DeleteCategory(int categoryId);
    }
}
