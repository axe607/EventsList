using EventsListCommon.Models;
using System.Collections.Generic;
using System.Linq;

namespace EventsListData.Clients
{
    public class Client : IClient
    {
        public IReadOnlyList<Category> GetCategories()
        {
            var result = new List<Category>();
            using (var client = new EventService.EventServiceClient())
            {
                client.Open();
                var data = client.GetCategories();
                result = data?.Select(x => new Category
                {
                    Id = x.Id,
                    Name = x.Name,
                    Subcategories =
                        x.Subcategories
                            .Select(z => new Subcategory { Id = z.Id, Name = z.Name, CategoryId = z.CategoryId }).ToList()
                }).ToList() ?? result;

                client.Close();
            }
            return result;
        }

        public IReadOnlyList<Event> GetEvents()
        {
            var result = new List<Event>();
            using (var client = new EventService.EventServiceClient())
            {
                client.Open();
                var data = client.GetEvents();

                result = data?.Select(x =>
                new Event
                {
                    Id = x.Id,
                    Address = x.Address,
                    Date = x.Date,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    Name = x.Name,
                    OrganizerId = x.OrganizerId,
                    SubcategoryId = x.SubcategoryId
                }).ToList() ?? result;

                client.Close();
            }
            return result;
        }

        public IReadOnlyList<Organizer> GetOrganizers()
        {
            var result = new List<Organizer>();
            using (var client = new EventService.EventServiceClient())
            {
                client.Open();
                var data = client.GetOrganizers();

                result = data?.Select(x =>
                 new Organizer
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Emails = x.Emails.ToList(),
                     Phones = x.Phones.ToList()
                 }).ToList() ?? result;



                client.Close();
            }
            return result;
        }

        public IReadOnlyList<Subcategory> GetSubcategories()
        {
            var result = new List<Subcategory>();
            using (var client = new EventService.EventServiceClient())
            {
                client.Open();

                var data = client.GetSubcategories();

                result = data?.Select(x =>
                new Subcategory
                {
                    Id = x.Id,
                    Name = x.Name,
                    CategoryId = x.CategoryId
                }).ToList() ?? result;

                client.Close();
            }
            return result;
        }
    }
}
