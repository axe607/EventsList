namespace EventsListBL.Services
{
    public interface IUserOperation
    {
        void AddUser(string name, string password, string email);
        void EditUserInfo(int userId, string name, string email);
        void DeleteUser(int userId);
    }
}
