using EventsListBL.Providers.Interfaces;
using EventsListCommon.Models;
using EventsListData.Repositories;
using System.Collections.Generic;

namespace EventsListBL.Providers
{
    public class AddressProvider:IAddressProvider
    {
        private readonly IDataRepository _dataProvider;

        public AddressProvider(IDataRepository dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public IReadOnlyList<Address> GetAddresses()
        {
            return _dataProvider.GetAddresses();
        }

        public Address GetAddressById(int addressId)
        {
            return _dataProvider.GetAddressById(addressId);
        }
    }
}
