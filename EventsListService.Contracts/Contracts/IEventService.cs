using EventsListService.Contracts.Models.Dto;
using EventsListService.Contracts.Models.DtoExceptions;
using System.Collections.Generic;
using System.ServiceModel;

namespace EventsListService.Contracts.Contracts
{
    [ServiceContract]
    public interface IEventService
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
        List<CategoryDto> GetCategories();

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        List<OrganizerDto> GetOrganizers();

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        OrganizerDto GetOrganizerById(int id);

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        List<EmailDto> GetEmailsByOrganizerId(int organizerId);

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        List<PhoneDto> GetPhonesByOrganizerId(int organizerId);

        [OperationContract]
        [FaultContract(typeof(ServiceFault))]
        AddressDto GetAddressById(int id);
    }
}
