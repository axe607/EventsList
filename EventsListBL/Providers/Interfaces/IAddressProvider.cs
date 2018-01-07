using EventsListCommon.Models;
using System.Collections.Generic;

namespace EventsListBL.Providers.Interfaces
{
    public interface IAddressProvider
    {
        IReadOnlyList<Address> GetAddresses();
        Address GetAddressById(int addressId);
    }
}
