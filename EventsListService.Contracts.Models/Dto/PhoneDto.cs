using System.Runtime.Serialization;

namespace EventsListService.Contracts.Models.Dto
{
    [DataContract]
    public class PhoneDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int OrganizerId { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
    }
}
