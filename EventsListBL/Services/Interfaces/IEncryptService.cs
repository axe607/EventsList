namespace EventsListBL.Services.Interfaces
{
    public interface IEncryptService
    {
        string GetEncryptedPassword(string password);
    }
}
