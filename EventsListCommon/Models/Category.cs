using System.ComponentModel;

namespace EventsListCommon.Models
{
    public class Category
    {
        public int Id { get; set; }
        [DisplayName("Parent")]
        public int? Pid { get; set; }
        public string Name { get; set; }
    }
}
