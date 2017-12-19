using System.ComponentModel;

namespace EventsListCommon.Models
{
    public class Address
    {
        public int Id { get; set; }
        [DisplayName("Address")]
        public string AddressString { get; set; }
    }
}
