using System;
using System.ServiceModel;

namespace EventsListService.Contracts.Contracts
{
    [ServiceContract]
    public interface IAdd
    {
        [OperationContract]
        void AddEvent(string name, DateTime date, int? organizerId, int? categoryId, string imageUrl, string description,int? addressId);

        [OperationContract]
        void AddUser(string name, string password, string email);

        [OperationContract]
        void AddRoleToUser(string userName,int roleId);

        [OperationContract]
        void AddRole(string roleName);

        [OperationContract]
        void AddAddress(string address);

        [OperationContract]
        void AddCategory(string categoryName, int? pid);

        [OperationContract]
        void AddPhone(int userId, string phoneNumber);

        [OperationContract]
        void AddEmail(int userId, string email);
    }
}
