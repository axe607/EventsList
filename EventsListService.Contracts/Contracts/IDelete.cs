using System.ServiceModel;

namespace EventsListService.Contracts.Contracts
{
    [ServiceContract]
    public interface IDelete
    {
        [OperationContract]
        void DeleteEvent(int eventId);

        [OperationContract]
        void DeleteFutureEventByIdAndUserId(int eventId,int userId);

        [OperationContract]
        void DeleteUser(int userId);

        [OperationContract]
        void DeleteRole(int roleId);

        [OperationContract]
        void DeleteUserRole(string userName, int roleId);

        [OperationContract]
        void DeleteEmailByUserIdAndEmailId(int userId, int emailId);

        [OperationContract]
        void DeletePhoneByUserIdAndPhoneId(int userId, int phoneId);

        [OperationContract]
        void DeleteAddress(int addressId);

        [OperationContract]
        void DeleteCategory(int categoryId);
    }
}
