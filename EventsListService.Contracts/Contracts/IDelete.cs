using System.ServiceModel;

namespace EventsListService.Contracts.Contracts
{
    [ServiceContract]
    public interface IDelete
    {
        [OperationContract]
        void DeleteEvent(int eventId);

        [OperationContract]
        void DeleteUser(int userId);
    }
}
