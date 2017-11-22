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
        List<OrganizerDto> GetOrganizers();
    }
}
