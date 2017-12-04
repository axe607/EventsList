using System.Collections.Generic;

namespace EventsListCommon.Models
{
    public class User
    {
        public string UserName { get; set; }
        public List<Role> Roles { get; set; }
    }
}
