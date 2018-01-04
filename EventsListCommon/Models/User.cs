using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EventsListCommon.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public Role[] Roles { get; set; }
        [DisplayName("Organizer name")]
        public string OrganizerName { get; set; }
        [DisplayName("Organizer emails")]
        public Email[] OrganizerEmails { get; set; }
        [DisplayName("Organizer phones")]
        public Phone[] OrganizerPhones { get; set; }
    }
}
