using System;
using System.ServiceModel;
using EventsListService.Contracts.Models.Dto;

namespace EventsListService.Contracts.Contracts
{
    [ServiceContract]
    public interface IUpdate
    {
        [OperationContract]
        void EditEvent(int eventId,string name, DateTime date, int categoryId, string imageUrl, string description, int addressId);

        [OperationContract]
        void EditUserInfo(int userId, string name, string email);
    }
}
