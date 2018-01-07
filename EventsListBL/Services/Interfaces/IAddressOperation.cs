namespace EventsListBL.Services.Interfaces
{
    public interface IAddressOperation
    {
        void AddAddress(string address);
        void EditAddress(int addressId, string address);
        void DeleteAddress(int addressId);
    }
}
