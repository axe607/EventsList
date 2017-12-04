using EventsListCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace EventsListData.Clients
{
    public class Client : IClient
    {
        private List<T> ConvertDataFromService<T, TK>(IEnumerable<TK> dataFromService)
        {
            var result = new List<T>();

            try
            {
                if (typeof(TK) == typeof(EventService.EventDto))
                {
                    List<Event> tempList = dataFromService.Cast<EventService.EventDto>()
                        .Select(x => new Event
                        {
                            Id = x.Id,
                            AddressId = x.AddressId,
                            Date = x.Date,
                            Description = x.Description,
                            ImageUrl = x.ImageUrl,
                            Name = x.Name,
                            OrganizerId = x.OrganizerId,
                            CategoryId = x.CategoryId
                        }).ToList();
                    result = tempList.Cast<T>().ToList();
                }
                else if (typeof(TK) == typeof(EventService.CategoryDto))
                {
                    List<Category> tempList = dataFromService.Cast<EventService.CategoryDto>()
                        .Select(x => new Category
                        {
                            Id = x.Id,
                            Pid = x.Pid,
                            Name = x.Name
                        }).ToList();
                    result = tempList.Cast<T>().ToList();
                }
                else if (typeof(TK) == typeof(EventService.EventDetailDto))
                {
                    List<EventDetail> tempList = dataFromService.Cast<EventService.EventDetailDto>()
                        .Select(x => new EventDetail
                        {
                            EventId = x.EventId,
                            EventName = x.EventName,
                            Date = x.Date,
                            ImageUrl = x.ImageUrl,
                            Description = x.Description,
                            Address = x.Address,
                            CategoryName = x.CategoryName,
                            OrganizerName = x.OrganizerName,
                            OrganizerEmails = x.OrganizerEmails.Select(z =>
                                new Email { Id = z.Id, OrganizerId = z.OrganizerId, EmailString = z.Email }).ToList(),
                            OrganizerPhones = x.OrganizerPhones.Select(z =>
                                    new Phone { Id = z.Id, OrganizerId = z.OrganizerId, PhoneNumber = z.PhoneNumber })
                                .ToList()
                        }).ToList();
                    result = tempList.Cast<T>().ToList();
                }
                else if (typeof(TK) == typeof(EventService.OrganizerDto))
                {
                    List<Organizer> tempList = dataFromService.Cast<EventService.OrganizerDto>()
                        .Select(x => new Organizer
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Emails = x.Emails.Select(z =>
                                new Email { Id = z.Id, OrganizerId = z.OrganizerId, EmailString = z.Email }).ToList(),
                            Phones = x.Phones.Select(z =>
                                    new Phone { Id = z.Id, OrganizerId = z.OrganizerId, PhoneNumber = z.PhoneNumber })
                                .ToList()
                        }).ToList();
                    result = tempList.Cast<T>().ToList();
                }
                else if (typeof(TK) == typeof(EventService.AddressDto))
                {
                    List<Address> tempList = dataFromService.Cast<EventService.AddressDto>()
                        .Select(x => new Address
                        {
                            Id = x.Id,
                            AddressString = x.Address
                        }).ToList();
                    result = tempList.Cast<T>().ToList();
                }
                else if (typeof(TK) == typeof(EventService.UserDto))
                {
                    List<User> tempList = dataFromService.Cast<EventService.UserDto>()
                        .Select(x => new User
                        {
                            UserName = x.UserName,
                            Roles = x.UserRoles.Select(z=>new Role
                            {
                                RoleName = z.RoleName
                            }).ToList()
                        }).ToList();
                    result = tempList.Cast<T>().ToList();
                }
                else if(typeof(TK) == typeof(bool))
                {
                    List<bool> tempList = dataFromService.Cast<bool>().ToList();
                    result = tempList.Cast<T>().ToList();
                }
            }
            catch (FaultException<EventService.ServiceFault> ex)
            {
                throw new Exception(ex.Detail.ErrorMessage);
            }

            return result;
        }

        private List<T> ConvertDataFromService<T, TK>(TK dataFromService) =>
            ConvertDataFromService<T, TK>(new List<TK> { dataFromService });

        public IReadOnlyList<Category> GetCategories()
        {

            var result = new List<Category>();
            var client = new EventService.EventServiceClient();
            try
            {
                client.Open();
                result = ConvertDataFromService<Category, EventService.CategoryDto>(client.GetCategories());
                client.Close();
            }
            catch (EndpointNotFoundException)
            {
                throw new Exception("Сервер не отвечает. Попробуйте позже.");
            }
            finally
            {
                client.Abort();
            }
            return result;
        }

        public IReadOnlyList<Event> GetEvents()
        {
            var result = new List<Event>();
            var client = new EventService.EventServiceClient();

            try
            {
                client.Open();
                result = ConvertDataFromService<Event, EventService.EventDto>(client.GetEvents());
                client.Close();
            }
            catch (EndpointNotFoundException)
            {
                throw new Exception("Сервер не отвечает. Попробуйте позже.");
            }
            finally
            {
                client.Abort();
            }

            return result;
        }

        public IReadOnlyList<Event> GetEventsByCategoryId(int categoryId)
        {
            var result = new List<Event>();
            var client = new EventService.EventServiceClient();
            try
            {
                client.Open();
                result = ConvertDataFromService<Event, EventService.EventDto>(client.GetEvents());
                client.Close();
            }
            catch (EndpointNotFoundException)
            {
                throw new Exception("Сервер не отвечает. Попробуйте позже.");
            }
            finally
            {
                client.Abort();
            }

            return result;
        }

        public EventDetail GetEventInfoDetailById(int eventId)
        {
            var result = new EventDetail();

            var client = new EventService.EventServiceClient();
            try
            {
                client.Open();
                result = ConvertDataFromService<EventDetail, EventService.EventDetailDto>(client.GetEventInfoDetailById(eventId)).First();
                client.Close();
            }
            catch (EndpointNotFoundException)
            {
                throw new Exception("Сервер не отвечает. Попробуйте позже.");
            }
            finally
            {
                client.Abort();
            }

            return result;
        }

        public IReadOnlyList<Organizer> GetOrganizers()
        {
            var result = new List<Organizer>();
            var client = new EventService.EventServiceClient();
            try
            {
                client.Open();
                result = ConvertDataFromService<Organizer, EventService.OrganizerDto>(client.GetOrganizers());
                client.Close();
            }
            catch (EndpointNotFoundException)
            {
                throw new Exception("Сервер не отвечает. Попробуйте позже.");
            }
            finally
            {
                client.Abort();
            }

            return result;
        }

        public Organizer GetOrganizerById(int id)
        {
            var result = new Organizer();
            var client = new EventService.EventServiceClient();

            try
            {
                client.Open();
                result = ConvertDataFromService<Organizer, EventService.OrganizerDto>(client.GetOrganizerById(id)).First();
                client.Close();
            }
            catch (EndpointNotFoundException)
            {
                throw new Exception("Сервер не отвечает. Попробуйте позже.");
            }
            finally
            {
                client.Abort();
            }

            return result;
        }
        
        public Address GetAddressById(int id)
        {
            var result = new Address();
            var client = new EventService.EventServiceClient();
            try
            {
                client.Open();
                result = ConvertDataFromService<Address, EventService.AddressDto>(client.GetAddressById(id)).First();
                client.Close();
            }
            catch (EndpointNotFoundException)
            {
                throw new Exception("Сервер не отвечает. Попробуйте позже.");
            }
            finally
            {
                client.Abort();
            }

            return result;
        }

        public User GetUserByName(string name)
        {
            var result = new User();
            var client = new EventService.EventServiceClient();
            try
            {
                client.Open();
                result = ConvertDataFromService<User, EventService.UserDto>(client.GetUserByName(name)).First();
                client.Close();
            }
            catch (EndpointNotFoundException)
            {
                throw new Exception("Сервер не отвечает. Попробуйте позже.");
            }
            finally
            {
                client.Abort();
            }

            return result;
        }
        
        public bool IsValidUser(string username, string password)
        {
            var result = false;
            var client = new EventService.EventServiceClient();
            try
            {
                client.Open();
                result = ConvertDataFromService<bool, bool>(client.IsValidUser(username,password)).First();
                client.Close();
            }
            catch (EndpointNotFoundException)
            {
                throw new Exception("Сервер не отвечает. Попробуйте позже.");
            }
            finally
            {
                client.Abort();
            }
            
            return result;
        }
    }
}
