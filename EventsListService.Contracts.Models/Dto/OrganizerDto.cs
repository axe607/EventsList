using System.Runtime.Serialization;

namespace EventsListService.Contracts.Models.Dto
{
    [DataContract]
   public class OrganizerDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}
