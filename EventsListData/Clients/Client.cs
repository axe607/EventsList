using EventsListCommon.Models;
using System.Collections.Generic;
using System.Linq;
using System;

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
                    AddressId = x.AddressId,
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

        public Organizer GetOrganizerById(int id)
        {
            var result = new Organizer();
            using (var client = new EventService.EventServiceClient())
            {
                client.Open();
                var data = client.GetOrganizerById(id);

                result = data != null ?
                    new Organizer
                    {
                        Id = data.Id,
                        Name = data.Name,
                        Emails = data.Emails.Select(z => new Email { Id = z.Id, OrganizerId = z.OrganizerId, EmailString = z.Email }).ToList(),
                        Phones = data.Phones.Select(z => new Phone { Id = z.Id, OrganizerId = z.OrganizerId, PhoneNumber = z.PhoneNumber }).ToList()
                    } :
                result;

                client.Close();
            }
            return result;
        }
        public Address GetAddressById(int id)
        {
            Address result = new Address();
            using (var client = new EventService.EventServiceClient())
            {
                client.Open();
                var data = client.GetAddressById(id);

                result = data != null 
                ?new Address
                    {
                        Id = data.Id,
                        AddressString = data.Address
                    }
                :result;

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
                     Emails = x.Emails.Select(z => new Email { Id = z.Id, OrganizerId = z.OrganizerId, EmailString = z.Email }).ToList(),
                     Phones = x.Phones.Select(z => new Phone { Id = z.Id, OrganizerId = z.OrganizerId, PhoneNumber = z.PhoneNumber }).ToList()
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
