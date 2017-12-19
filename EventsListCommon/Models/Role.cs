using System.ComponentModel;

namespace EventsListCommon.Models
{
    public class Role
    {
        public int Id { get; set; }
        [DisplayName("Role name")]
        public string RoleName { get; set; }
    }
}
