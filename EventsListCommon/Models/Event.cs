using System;
using System.ComponentModel;

namespace EventsListCommon.Models
{
    public class Event
    {
        public int Id { get; set; }
        [DisplayName("Event name")]
        public string Name { get; set; }
        public DateTime Date { get; set; }
        [DisplayName("Organizer")]
        public int? OrganizerId { get; set; }
        [DisplayName("Category")]
        public int? CategoryId { get; set; }
        public string ImageUrl { get; set; }
        [DisplayName("Address")]
        public int? AddressId { get; set; }
        public string Description { get; set; }
    }
}
