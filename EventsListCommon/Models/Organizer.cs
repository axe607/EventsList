using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsListCommon.Models
{
    public class Organizer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Phones { get; set; }
        public List<string> Emails { get; set; }
    }
}
