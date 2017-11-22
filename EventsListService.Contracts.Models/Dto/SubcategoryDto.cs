using System.Runtime.Serialization;

namespace EventsListService.Contracts.Models.Dto
{
    [DataContract]
    public class SubcategoryDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int CategoryId { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}
