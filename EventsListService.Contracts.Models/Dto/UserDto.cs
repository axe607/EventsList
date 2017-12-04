using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EventsListService.Contracts.Models.Dto
{
    [DataContract]
    public class UserDto
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public List<RoleDto> UserRoles { get; set; }
    }
}
