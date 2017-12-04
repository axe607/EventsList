using System.Runtime.Serialization;

namespace EventsListService.Contracts.Models.Dto
{
    [DataContract]
    public class AddressDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Address { get; set; }
    }
}
