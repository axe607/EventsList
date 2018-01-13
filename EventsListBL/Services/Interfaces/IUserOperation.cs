namespace EventsListBL.Services.Interfaces
{
    public interface IUserOperation
    {
        void AddUser(string name, string password, string email);
        void EditUserInfo(int userId, string name, string password, string email);
        void EditOrganizerInfo(int userId, string name);
        void DeleteUser(int userId);
        void AddRoleToUser(string userName, int roleId);
        void DeleteUserRole(string userName, int roleId);
        void AddRole(string roleName);
        void EditRole(int roleId, string roleName);
        void DeleteRole(int roleId);
        void AddPhone(int userId, string phoneNumber);
        void AddEmail(int userId, string email);
        void DeletePhoneByUserIdAndPhoneId(int userId, int phoneId);
        void DeleteEmailByUserIdAndEmailId(int userId, int emailId);
    }
}
