using EventsListData.Repositories;

namespace EventsListBL.Services
{
    public class UserOperation : IUserOperation
    {
        readonly IDataRepository _dataProvider;

        public UserOperation(IDataRepository dataRepository)
        {
            _dataProvider = dataRepository;
        }

        public void AddUser(string name, string password, string email)
        {
            _dataProvider.AddUser(name, password, email);
        }

        public void EditUserInfo(int userId, string name, string email)
        {
            _dataProvider.EditUserInfo(userId, name, email);
        }

        public void DeleteUser(int userId)
        {
            _dataProvider.DeleteUser(userId);
        }
    }
}
