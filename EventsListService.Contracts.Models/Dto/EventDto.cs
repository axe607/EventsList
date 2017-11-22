using System;
using System.Runtime.Serialization;

namespace EventsListService.Contracts.Models.Dto
{
    [DataContract]
    public class EventDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public int OrganizerId { get; set; }
        [DataMember]
        public int SubcategoryId { get; set; }
        [DataMember]
        public string ImageUrl { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
