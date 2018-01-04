using System;

namespace EventsListCommon.Models
{
    public class EventDetail
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime Date { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string CategoryName { get; set; }
        public string OrganizerName { get; set; }
        public Phone[] OrganizerPhones { get; set; }
        public Email[] OrganizerEmails { get; set; }
    }
}
