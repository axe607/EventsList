using System;
using System.ServiceModel;

namespace EventsListService.Contracts.Contracts
{
    [ServiceContract]
    public interface IAdd
    {
        [OperationContract]
        void AddEvent(string name, DateTime date, int organizerId, int categoryId, string imageUrl, string description,int addressId);

        [OperationContract]
        void AddUser(string name, string password, string email);
    }
}
