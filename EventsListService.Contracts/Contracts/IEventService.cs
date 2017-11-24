using EventsListService.Contracts.Models.Dto;
using System.Collections.Generic;
using System.ServiceModel;

namespace EventsListService.Contracts.Contracts
{
    [ServiceContract]
    public interface IEventService
    {
        [OperationContract]
        List<EventDto> GetEvents();
        [OperationContract]
        List<CategoryDto> GetCategories();
        [OperationContract]
        List<SubcategoryDto> GetSubcategories();
        [OperationContract]
        List<SubcategoryDto> GetSubcategoriesByCategoryId(int categoryId);
        [OperationContract]
        List<OrganizerDto> GetOrganizers();
        [OperationContract]
        OrganizerDto GetOrganizerById(int id);
        [OperationContract]
        List<EmailDto> GetEmailsByOrganizerId(int organizerId);
        [OperationContract]
        List<PhoneDto> GetPhonesByOrganizerId(int organizerId);
        [OperationContract]
        AddressDto GetAddressById(int id);
    }
}
