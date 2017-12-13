using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EventsListService.Contracts.Models.Dto
{
    [DataContract]
    public class UserDto
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public List<RoleDto> UserRoles { get; set; }

        [DataMember]
        public string OrganizerName { get; set; }

        [DataMember]
        public List<EmailDto> OrganizerEmails { get; set; }

        [DataMember]
        public List<PhoneDto> OrganizerPhones { get; set; }
    }
}
