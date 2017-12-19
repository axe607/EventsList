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

        public void AddRoleToUser(string userName, int roleId)
        {
            _dataProvider.AddRoleToUser(userName, roleId);
        }

        public void DeleteUserRole(string userName, int roleId)
        {
            _dataProvider.DeleteUserRole(userName, roleId);
        }

        public void AddRole(string roleName)
        {
            _dataProvider.AddRole(roleName);
        }

        public void EditRole(int roleId, string roleName)
        {
            _dataProvider.EditRole(roleId, roleName);
        }

        public void DeleteRole(int roleId)
        {
            _dataProvider.DeleteRole(roleId);
        }

        public void DeleteEmailByUserIdAndEmailId(int userId, int emailId)
        {
            _dataProvider.DeleteEmailByUserIdAndEmailId(userId, emailId);
        }

        public void DeletePhoneByUserIdAndPhoneId(int userId, int phoneId)
        {
            _dataProvider.DeletePhoneByUserIdAndPhoneId(userId, phoneId);
        }
    }
}
