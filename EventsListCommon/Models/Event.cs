using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsListCommon.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int OrganizerId { get; set; }
        public int SubcategoryId { get; set; }
        public string ImageUrl { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}
