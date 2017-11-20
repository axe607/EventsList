using System.Collections.Generic;

namespace EventsListCommon.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Subcategory> Subcategories { get; set; }

        public Category()
        {
            Subcategories = new List<Subcategory>();
        }
    }
}
