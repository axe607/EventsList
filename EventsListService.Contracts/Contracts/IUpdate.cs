using System;
using System.ServiceModel;

namespace EventsListService.Contracts.Contracts
{
    [ServiceContract]
    public interface IUpdate
    {
        [OperationContract]
        void EditEvent(int eventId, string name, DateTime date, int? categoryId, string imageUrl, string description, int? addressId);

        [OperationContract]
        void EditEventByUserId(int eventId, int userId, string name, DateTime date, int? categoryId, string imageUrl, string description, int? addressId);

        [OperationContract]
        void EditUserInfo(int userId, string name, string password, string email);

        [OperationContract]
        void EditOrganizerInfo(int userId, string name);

        [OperationContract]
        void EditRole(int roleId, string roleName);

        [OperationContract]
        void EditAddress(int addressId, string address);

        [OperationContract]
        void EditCategory(int categoryId, int? pid, string categoryName);
    }
}
