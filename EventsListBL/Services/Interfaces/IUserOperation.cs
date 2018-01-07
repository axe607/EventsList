namespace EventsListBL.Services.Interfaces
{
    public interface IUserOperation
    {
        void AddUser(string name, string password, string email);
        void EditUserInfo(int userId, string name, string email);
        void DeleteUser(int userId);
        void AddRoleToUser(string userName, int roleId);
        void DeleteUserRole(string userName, int roleId);
        void AddRole(string roleName);
        void EditRole(int roleId, string roleName);
        void DeleteRole(int roleId);
        void DeleteEmailByUserIdAndEmailId(int userId, int emailId);
        void DeletePhoneByUserIdAndPhoneId(int userId, int phoneId);
    }
}
