using System;
using EventsListService.Contracts.Models.Dto;
using EventsListService.Contracts.Models.DtoExceptions;
using System.Collections.Generic;
using System.ServiceModel;

namespace EventsListService.Contracts.Contracts
{
    [ServiceContract]
    public interface IGet
    {
        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        List<EventDto> GetEvents();

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        List<EventDto> GetEventsByCategoryId(int categoryId);

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        EventDetailDto GetEventInfoDetailById(int eventId);

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        EventDto GetEventById(int eventId);

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        List<EventDto> GetEventsBySearchData(int? categoryId, DateTime? date, int? state);

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        List<CategoryDto> GetCategories();

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        List<AddressDto> GetAddresses();

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        List<EmailDto> GetEmailsByOrganizerId(int organizerId);

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        List<PhoneDto> GetPhonesByOrganizerId(int organizerId);

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        UserDto GetUserByName(string name);

        List<RoleDto> GetRolesByUserId(int id);

        [OperationContract]
        bool IsValidUser(string username, string password);

        [OperationContract]
        bool IsNameFree(int userId, string name);
    }
}
