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
                            Address = string.IsNullOrEmpty(x.Address) ? "DELETED" : x.Address,
                            CategoryName = string.IsNullOrEmpty(x.CategoryName) ? "DELETED" : x.CategoryName,
                            OrganizerName = string.IsNullOrEmpty(x.OrganizerName) ? "DELETED" : x.OrganizerName,
                            OrganizerEmails = x.OrganizerEmails?.Select(z =>
                                new Email { Id = z.Id, OrganizerId = z.OrganizerId, EmailString = z.Email }).ToList(),
                            OrganizerPhones = x.OrganizerPhones?.Select(z =>
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
                            Id = x.Id,
                            UserName = x.UserName,
                            Email = x.Email,
                            Roles = x.UserRoles.Select(z => new Role
                            {
                                Id = z.Id,
                                RoleName = z.RoleName
                            }).ToList(),
                            OrganizerName = x.OrganizerName,
                            OrganizerEmails = x.OrganizerEmails?.Select(z => new Email
                            {
                                Id = z.Id,
                                OrganizerId = z.OrganizerId,
                                EmailString = z.Email
                            }).ToList(),
                            OrganizerPhones = x.OrganizerPhones?.Select(z => new Phone
                            {
                                Id = z.Id,
                                OrganizerId = z.OrganizerId,
                                PhoneNumber = z.PhoneNumber
                            }).ToList()
                        }).ToList();
                    result = tempList.Cast<T>().ToList();
                }
                else if (typeof(TK) == typeof(EventService.RoleDto))
                {
                    List<Role> tempList = dataFromService.Cast<EventService.RoleDto>()
                        .Select(x => new Role
                        {
                            Id = x.Id,
                            RoleName = x.RoleName
                        }).ToList();
                    result = tempList.Cast<T>().ToList();
                }
                else if (typeof(TK) == typeof(bool))
                {
                    List<bool> tempList = dataFromService.Cast<bool>().ToList();
                    result = tempList.Cast<T>().ToList();
                }
            }
            catch (FaultException<EventService.ServiceFault> ex)
            {
                throw new Exception(ex.Detail.ErrorMessage);
            }
            catch (NullReferenceException)
            {
                throw new Exception($"Get non of {typeof(TK)}");
            }

            return result;
        }

        private List<T> ConvertDataFromService<T, TK>(TK dataFromService) =>
            ConvertDataFromService<T, TK>(new List<TK> { dataFromService });

        public Event GetEventById(int eventId)
        {
            var result = new Event();

            var client = new EventService.GetClient();
            try
            {
                client.Open();
                result = ConvertDataFromService<Event, EventService.EventDto>(client.GetEventById(eventId)).First();
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

        public IReadOnlyList<Category> GetCategories()
        {

            var result = new List<Category>();
            var client = new EventService.GetClient();
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
            var client = new EventService.GetClient();

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
            var client = new EventService.GetClient();
            try
            {
                client.Open();
                result = ConvertDataFromService<Event, EventService.EventDto>(client.GetEventsByCategoryId(categoryId));
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

        public IReadOnlyList<Event> GetEventsByUserId(int userId)
        {
            var result = new List<Event>();
            var client = new EventService.GetClient();
            try
            {
                client.Open();
                result = ConvertDataFromService<Event, EventService.EventDto>(client.GetEventsByUserId(userId));
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

        public IReadOnlyList<Event> GetEventsBySearchData(int? categoryId, DateTime? date, int? state)
        {
            var result = new List<Event>();
            var client = new EventService.GetClient();
            try
            {
                client.Open();
                result = ConvertDataFromService<Event, EventService.EventDto>(client.GetEventsBySearchData(categoryId, date, state));
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

        public Category GetCategoryById(int categoryId)
        {
            var result = new Category();

            var client = new EventService.GetClient();
            try
            {
                client.Open();
                result = ConvertDataFromService<Category, EventService.CategoryDto>(client.GetCategoryById(categoryId)).First();
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

            var client = new EventService.GetClient();
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

        public IReadOnlyList<Address> GetAddresses()
        {
            var result = new List<Address>();
            var client = new EventService.GetClient();
            try
            {
                client.Open();
                result = ConvertDataFromService<Address, EventService.AddressDto>(client.GetAddresses());
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

        public IReadOnlyList<User> GetUsers()
        {
            var result = new List<User>();
            var client = new EventService.GetClient();
            try
            {
                client.Open();
                result = ConvertDataFromService<User, EventService.UserDto>(client.GetUsers());
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

        public IReadOnlyList<Role> GetRoles()
        {
            var result = new List<Role>();
            var client = new EventService.GetClient();
            try
            {
                client.Open();
                result = ConvertDataFromService<Role, EventService.RoleDto>(client.GetRoles());
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

        public Role GetRolesById(int roleId)
        {
            var result = new Role();
            var client = new EventService.GetClient();
            try
            {
                client.Open();
                result = ConvertDataFromService<Role, EventService.RoleDto>(client.GetRolesById(roleId)).First();
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
            var client = new EventService.GetClient();
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

        public Address GetAddressById(int addressId)
        {
            var result = new Address();
            var client = new EventService.GetClient();
            try
            {
                client.Open();
                result = ConvertDataFromService<Address, EventService.AddressDto>(client.GetAddressById(addressId)).First();
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

        public IReadOnlyList<Role> GetRolesNotInUser(string userName)
        {
            var result = new List<Role>();
            var client = new EventService.GetClient();
            try
            {
                client.Open();
                result = ConvertDataFromService<Role, EventService.RoleDto>(client.GetRolesNotInUser(userName));
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
            var client = new EventService.GetClient();
            try
            {
                client.Open();
                result = ConvertDataFromService<bool, bool>(client.IsValidUser(username, password)).First();
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

        public bool IsUserNameFree(int userId, string name)
        {
            var result = false;
            var client = new EventService.GetClient();
            try
            {
                client.Open();
                result = ConvertDataFromService<bool, bool>(client.IsUserNameFree(userId, name)).First();
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

        public bool IsRoleNameFree(int? roleId, string name)
        {
            var result = false;
            var client = new EventService.GetClient();
            try
            {
                client.Open();
                result = ConvertDataFromService<bool, bool>(client.IsRoleNameFree(roleId, name)).First();
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

        public void AddEvent(string name, DateTime date, int? organizerId, int? categoryId, string imageUrl, string description,
            int? addressId)
        {
            var client = new EventService.AddClient();
            try
            {
                client.Open();
                client.AddEvent(name, date, organizerId, categoryId, imageUrl, description, addressId);
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
        }

        public void EditEventByUserId(int eventId, int userId, string name, DateTime date, int? categoryId, string imageUrl,
            string description, int? addressId)
        {
            var client = new EventService.UpdateClient();
            try
            {
                client.Open();
                client.EditEventByUserId(eventId, userId, name, date, categoryId, imageUrl, description, addressId);
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
        }

        public void DeleteEvent(int eventId)
        {
            var client = new EventService.DeleteClient();
            try
            {
                client.Open();
                client.DeleteEvent(eventId);
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
        }

        public void DeleteFutureEventByIdAndUserId(int eventId, int userId)
        {
            var client = new EventService.DeleteClient();
            try
            {
                client.Open();
                client.DeleteFutureEventByIdAndUserId(eventId,userId);
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
        }

        public void EditEvent(int eventId, string name, DateTime date, int? categoryId, string imageUrl,
            string description, int? addressId)
        {
            var client = new EventService.UpdateClient();
            try
            {
                client.Open();
                client.EditEvent(eventId, name, date, categoryId, imageUrl, description, addressId);
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
        }

        public void AddUser(string name, string password, string email)
        {
            var client = new EventService.AddClient();
            try
            {
                client.Open();
                client.AddUser(name, password, email);
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
        }

        public void EditUserInfo(int userId, string name, string email)
        {
            var client = new EventService.UpdateClient();
            try
            {
                client.Open();
                client.EditUserInfo(userId, name, email);
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
        }

        public void DeleteUser(int userId)
        {
            var client = new EventService.DeleteClient();
            try
            {
                client.Open();
                client.DeleteUser(userId);
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
        }

        public void AddRoleToUser(string userName, int roleId)
        {
            var client = new EventService.AddClient();
            try
            {
                client.Open();
                client.AddRoleToUser(userName, roleId);
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
        }

        public void DeleteUserRole(string userName, int roleId)
        {
            var client = new EventService.DeleteClient();
            try
            {
                client.Open();
                client.DeleteUserRole(userName, roleId);
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
        }

        public void AddRole(string roleName)
        {
            var client = new EventService.AddClient();
            try
            {
                client.Open();
                client.AddRole(roleName);
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
        }

        public void EditRole(int roleId, string roleName)
        {
            var client = new EventService.UpdateClient();
            try
            {
                client.Open();
                client.EditRole(roleId, roleName);
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
        }

        public void DeleteRole(int roleId)
        {
            var client = new EventService.DeleteClient();
            try
            {
                client.Open();
                client.DeleteRole(roleId);
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
        }

        public void DeleteEmailByUserIdAndEmailId(int userId, int emailId)
        {
            var client = new EventService.DeleteClient();
            try
            {
                client.Open();
                client.DeleteEmailByUserIdAndEmailId(userId, emailId);
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
        }

        public void DeletePhoneByUserIdAndPhoneId(int userId, int phoneId)
        {
            var client = new EventService.DeleteClient();
            try
            {
                client.Open();
                client.DeletePhoneByUserIdAndPhoneId(userId, phoneId);
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
        }

        public void AddAddress(string address)
        {
            var client = new EventService.AddClient();
            try
            {
                client.Open();
                client.AddAddress(address);
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
        }

        public void EditAddress(int addressId, string address)
        {
            var client = new EventService.UpdateClient();
            try
            {
                client.Open();
                client.EditAddress(addressId, address);
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
        }

        public void DeleteAddress(int addressId)
        {
            var client = new EventService.DeleteClient();
            try
            {
                client.Open();
                client.DeleteAddress(addressId);
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
        }

        public void AddCategory(string categoryName, int? pid)
        {
            var client = new EventService.AddClient();
            try
            {
                client.Open();
                client.AddCategory(categoryName,pid);
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
        }

        public void EditCategory(int categoryId, int? pid, string categoryName)
        {
            var client = new EventService.UpdateClient();
            try
            {
                client.Open();
                client.EditCategory(categoryId,pid,categoryName);
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
        }

        public void DeleteCategory(int categoryId)
        {
            var client = new EventService.DeleteClient();
            try
            {
                client.Open();
                client.DeleteCategory(categoryId);
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
        }
    }
}
