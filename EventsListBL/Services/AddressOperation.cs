using EventsListBL.Services.Interfaces;
using EventsListData.Repositories;

namespace EventsListBL.Services
{
    public class AddressOperation : IAddressOperation
    {
        private readonly IDataRepository _dataProvider;

        public AddressOperation(IDataRepository dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void AddAddress(string address)
        {
            _dataProvider.AddAddress(address);
        }

        public void EditAddress(int addressId, string address)
        {
            _dataProvider.EditAddress(addressId, address);
        }

        public void DeleteAddress(int addressId)
        {
            _dataProvider.DeleteAddress(addressId);
        }
    }
}
