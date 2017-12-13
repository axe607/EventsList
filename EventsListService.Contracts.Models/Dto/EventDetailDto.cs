using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EventsListService.Contracts.Models.Dto
{
    [DataContract]
    public class EventDetailDto
    {
        [DataMember]
        public int EventId { get; set; }
        [DataMember]
        public string EventName { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public string ImageUrl { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string CategoryName { get; set; }
        [DataMember]
        public string OrganizerName { get; set; }
        [DataMember]
        public List<PhoneDto> OrganizerPhones { get; set; }
        [DataMember]
        public List<EmailDto> OrganizerEmails { get; set; }
    }
}
