using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EventsListCommon.Models
{
    public class User
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<Role> Roles { get; set; }
        [DisplayName("Organizer name")]
        public string OrganizerName { get; set; }
        [DisplayName("Organizer emails")]
        public List<Email> OrganizerEmails { get; set; }
        [DisplayName("Organizer phones")]
        public List<Phone> OrganizerPhones { get; set; }
    }
}
