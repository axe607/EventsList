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
        List<EventDto> GetEventsByUserId(int userId);

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
        CategoryDto GetCategoryById(int categoryId);

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        List<AddressDto> GetAddresses();

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        AddressDto GetAddressById(int addressId);

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        List<EmailDto> GetEmailsByOrganizerId(int organizerId);

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        List<PhoneDto> GetPhonesByOrganizerId(int organizerId);

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        List<UserDto> GetUsers();

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        UserDto GetUserByName(string name);

        List<RoleDto> GetRolesByUserId(int id);

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        List<RoleDto> GetRolesNotInUser(string userName);

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        List<RoleDto> GetRoles();

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        RoleDto GetRolesById(int roleId);

        [OperationContract]
        bool IsValidUser(string username, string password);

        [OperationContract]
        bool IsUserNameFree(int userId, string name);

        [OperationContract]
        bool IsRoleNameFree(int? roleId, string name);
    }
}
