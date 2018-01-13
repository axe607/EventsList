using EventsListService.Contracts.Models.Dto;
using EventsListService.Contracts.Models.DtoExceptions;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Caching;
using System.ServiceModel;
using System.Web.Configuration;

namespace EventsListService.Contracts.Contracts
{
    public class EventService : IGet, IAdd, IDelete, IUpdate
    {
        private readonly string _connectionString;

        private readonly string _connectionStringDependency;

        private readonly Cache _staticCache;

        private static readonly string DEFAULT_ERROR_MESSAGE_FOR_CLIENT = "Server is unavailable now. Try again later.";

        private static readonly ILog Log = LogManager.GetLogger(typeof(EventService));

        public EventService()
        {
            try
            {
                _connectionString = ConfigurationManager.ConnectionStrings["DefDbConnect"].ConnectionString;
                _connectionStringDependency = WebConfigurationManager.ConnectionStrings["DefDbConnect"].ConnectionString;
                _staticCache = new Cache();
                SqlDependency.Start(_connectionStringDependency);
            }
            catch (NullReferenceException e)
            {
                Log.Error(e.Message);
            }
        }

        private void UpdateCache(string key, string tableName, object value)
        {
            SqlCacheDependency dependency = null;

            try
            {
                dependency = new SqlCacheDependency("EventsDB", tableName);
            }
            catch (DatabaseNotEnabledForNotificationException exDbDis)
            {
                Log.Error(exDbDis.Message);
                try
                {
                    SqlCacheDependencyAdmin.EnableNotifications(_connectionStringDependency);
                    UpdateCache(key, tableName, value);
                }
                catch (UnauthorizedAccessException exPerm)
                {
                    Log.Error(exPerm.Message);
                    throw new FaultException<ServiceFault>(new ServiceFault(DEFAULT_ERROR_MESSAGE_FOR_CLIENT),
                        new FaultReason(DEFAULT_ERROR_MESSAGE_FOR_CLIENT));
                }
            }
            catch (TableNotEnabledForNotificationException exTabDis)
            {
                Log.Error(exTabDis.Message);
                try
                {
                    SqlCacheDependencyAdmin.EnableTableForNotifications(_connectionStringDependency, tableName);
                    UpdateCache(key, tableName, value);
                }
                catch (SqlException exc)
                {
                    Log.Error(exc.Message);
                    throw new FaultException<ServiceFault>(new ServiceFault(DEFAULT_ERROR_MESSAGE_FOR_CLIENT),
                        new FaultReason(DEFAULT_ERROR_MESSAGE_FOR_CLIENT));
                }
            }
            finally
            {
                if (_staticCache[key] == null)
                {
                    _staticCache.Insert(key, value, dependency);
                }
            }
        }

        private List<T> GetDataFromDb<T>(Func<DataRow, T> convertToDtoFunc, string procedureName, string key, string tableName,
            params SqlParameter[] sqlParams)
        {
            List<T> resultList = GetDataFromDb(convertToDtoFunc, procedureName, sqlParams);
            UpdateCache(key, tableName, resultList);
            return resultList;
        }

        private List<T> GetDataFromDb<T>(Func<DataRow, T> convertToDtoFunc, string procedureName, params SqlParameter[] sqlParams)
        {
            if (string.IsNullOrEmpty(procedureName))
            {
                throw new FaultException<ServiceFault>(new ServiceFault(DEFAULT_ERROR_MESSAGE_FOR_CLIENT),
                    new FaultReason(DEFAULT_ERROR_MESSAGE_FOR_CLIENT));
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = procedureName;

                    if (sqlParams != null)
                    {
                        foreach (SqlParameter sqlparam in sqlParams)
                        {
                            command.Parameters.Add(sqlparam);
                        }
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    try
                    {
                        adapter.Fill(dataSet);
                    }
                    catch (SqlException ex)
                    {
                        Log.Error(ex.Message);
                        throw new FaultException<ServiceFault>(new ServiceFault(DEFAULT_ERROR_MESSAGE_FOR_CLIENT),
                            new FaultReason(DEFAULT_ERROR_MESSAGE_FOR_CLIENT));
                    }

                    List<T> resultList = new List<T>();

                    try
                    {
                        foreach (DataTable dataSetTable in dataSet.Tables)
                        {
                            foreach (DataRow dataRow in dataSetTable.Rows)
                            {
                                resultList.Add(convertToDtoFunc(dataRow));
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        Log.Error(ex.Message);
                        throw new FaultException<ServiceFault>(new ServiceFault(DEFAULT_ERROR_MESSAGE_FOR_CLIENT),
                            new FaultReason(DEFAULT_ERROR_MESSAGE_FOR_CLIENT));
                    }
                    catch (FormatException ex)
                    {
                        Log.Error(ex.Message);
                        throw new FaultException<ServiceFault>(new ServiceFault(DEFAULT_ERROR_MESSAGE_FOR_CLIENT),
                            new FaultReason(DEFAULT_ERROR_MESSAGE_FOR_CLIENT));
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                        throw new FaultException<ServiceFault>(new ServiceFault(DEFAULT_ERROR_MESSAGE_FOR_CLIENT),
                            new FaultReason(DEFAULT_ERROR_MESSAGE_FOR_CLIENT));
                    }
                    return resultList;
                }
            }
        }

        private bool GetBoolFromDb(string procedureName, params SqlParameter[] sqlParams)
        {
            if (string.IsNullOrEmpty(procedureName))
            {
                throw new FaultException<ServiceFault>(new ServiceFault(DEFAULT_ERROR_MESSAGE_FOR_CLIENT),
                    new FaultReason(DEFAULT_ERROR_MESSAGE_FOR_CLIENT));
            }
            bool result = false;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = procedureName;

                    if (sqlParams != null)
                    {
                        foreach (SqlParameter sqlparam in sqlParams)
                        {
                            command.Parameters.Add(sqlparam);
                        }
                    }

                    SqlParameter resultParameter = new SqlParameter { Direction = ParameterDirection.ReturnValue };
                    command.Parameters.Add(resultParameter);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();

                    try
                    {
                        adapter.Fill(dataSet);
                    }
                    catch (SqlException ex)
                    {
                        Log.Error(ex.Message);
                        throw new FaultException<ServiceFault>(new ServiceFault(DEFAULT_ERROR_MESSAGE_FOR_CLIENT),
                            new FaultReason(DEFAULT_ERROR_MESSAGE_FOR_CLIENT));
                    }
                    catch (InvalidOperationException ex)
                    {
                        Log.Error(ex.Message);
                        throw new FaultException<ServiceFault>(new ServiceFault(DEFAULT_ERROR_MESSAGE_FOR_CLIENT),
                            new FaultReason(DEFAULT_ERROR_MESSAGE_FOR_CLIENT));
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                        throw new FaultException<ServiceFault>(new ServiceFault(DEFAULT_ERROR_MESSAGE_FOR_CLIENT),
                            new FaultReason(DEFAULT_ERROR_MESSAGE_FOR_CLIENT));
                    }

                    if ((int)resultParameter.Value == 1)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        private void ChangeDbData(string procedureName, params SqlParameter[] sqlParams)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = procedureName;

                    if (sqlParams != null)
                    {
                        foreach (SqlParameter sqlparam in sqlParams)
                        {
                            command.Parameters.Add(sqlparam);
                        }
                    }

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (SqlException ex)
                    {
                        Log.Error(ex.Message);
                        throw new FaultException<ServiceFault>(new ServiceFault(DEFAULT_ERROR_MESSAGE_FOR_CLIENT),
                            new FaultReason(DEFAULT_ERROR_MESSAGE_FOR_CLIENT));
                    }
                    catch (InvalidOperationException ex)
                    {
                        Log.Error(ex.Message);
                        throw new FaultException<ServiceFault>(new ServiceFault(DEFAULT_ERROR_MESSAGE_FOR_CLIENT),
                            new FaultReason(DEFAULT_ERROR_MESSAGE_FOR_CLIENT));
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex.Message);
                        throw new FaultException<ServiceFault>(new ServiceFault(DEFAULT_ERROR_MESSAGE_FOR_CLIENT),
                            new FaultReason(DEFAULT_ERROR_MESSAGE_FOR_CLIENT));
                    }
                }
            }
        }

        private EventDto CreateEventDto(DataRow dataRow)
        {
            return new EventDto
            {
                Id = (int)dataRow[0],
                Name = dataRow[1].ToString(),
                Date = (DateTime)dataRow[2],
                OrganizerId = dataRow[3] as int?,
                CategoryId = dataRow[4] as int?,
                ImageUrl = dataRow[5]?.ToString(),
                Description = dataRow[6].ToString(),
                AddressId = dataRow[7] as int?
            };
        }

        private EventDetailDto CreateEventDetailDto(DataRow dataRow)
        {
            return new EventDetailDto
            {
                EventId = (int)dataRow[0],
                EventName = dataRow[1].ToString(),
                Date = (DateTime)dataRow[2],
                ImageUrl = dataRow[3]?.ToString(),
                Description = dataRow[4].ToString(),
                Address = dataRow[5].ToString(),
                CategoryName = dataRow[6].ToString(),
                OrganizerName = string.IsNullOrEmpty(dataRow[7].ToString())
                    ? dataRow[9].ToString()
                    : dataRow[7].ToString(),
                OrganizerEmails = (dataRow[8] as int?) != null ? GetEmailsByOrganizerId((int)dataRow[8]) : null,
                OrganizerPhones = (dataRow[8] as int?) != null ? GetPhonesByOrganizerId((int)dataRow[8]) : null
            };
        }

        private CategoryDto CreateCategoryDto(DataRow dataRow)
        {
            return new CategoryDto
            {
                Id = (int)dataRow[0],
                Pid = (dataRow[1]) as int?,
                Name = dataRow[2].ToString()
            };
        }

        private OrganizerDto CreateOrganizerDto(DataRow dataRow)
        {
            return new OrganizerDto
            {
                Id = (int)dataRow[0],
                Name = dataRow[1].ToString()
            };
        }

        private AddressDto CreateAddressDto(DataRow dataRow)
        {
            return new AddressDto
            {
                Id = (int)dataRow[0],
                Address = dataRow[1].ToString()
            };
        }

        private EmailDto CreateEmailDto(DataRow dataRow)
        {
            return new EmailDto
            {
                Id = (int)dataRow[0],
                OrganizerId = (int)dataRow[1],
                Email = dataRow[2].ToString()
            };
        }

        private PhoneDto CreatePhoneDto(DataRow dataRow)
        {
            return new PhoneDto
            {
                Id = (int)dataRow[0],
                OrganizerId = (int)dataRow[1],
                PhoneNumber = dataRow[2].ToString()
            };
        }

        private UserDto CreateUserDto(DataRow dataRow)
        {
            return new UserDto
            {
                Id = (int)dataRow[0],
                UserName = dataRow[1].ToString(),
                Email = dataRow[2].ToString(),
                UserRoles = GetRolesByUserId((int)dataRow[0]),
                OrganizerName = dataRow[3].ToString(),
                OrganizerEmails = GetEmailsByOrganizerId((int)dataRow[0]),
                OrganizerPhones = GetPhonesByOrganizerId((int)dataRow[0])
            };
        }

        private RoleDto CreateRoleDto(DataRow dataRow)
        {
            return new RoleDto
            {
                Id = (int)dataRow[0],
                RoleName = dataRow[1].ToString()
            };
        }

        public List<EventDto> GetEvents()
        {
            if (_staticCache["Events"] == null)
            {
                Func<DataRow, EventDto> dt = CreateEventDto;
                return GetDataFromDb(dt, "SelectEvents", "Events", "Events");
            }
            return (List<EventDto>)_staticCache["Events"];
        }

        public List<EventDto> GetEventsByCategoryId(int categoryId)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@categoryId",
                Value = categoryId
            };
            Func<DataRow, EventDto> dt = CreateEventDto;
            return GetDataFromDb(dt, "SelectEventsByCategoryId", param);
        }

        public List<EventDto> GetEventsByUserId(int userId)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@userId",
                Value = userId
            };
            Func<DataRow, EventDto> dt = CreateEventDto;
            return GetDataFromDb(dt, "SelectEventsByUserId", param);
        }

        public List<EventDto> GetEventsBySearchData(int? categoryId, DateTime? date, int? state)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "@categoryId",
                    Value = categoryId,
                    IsNullable = true
                },
                new SqlParameter
                {
                    DbType = DbType.Date,
                    ParameterName = "@date",
                    Value = date,
                    IsNullable = true
                },
                new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "@state",
                    Value = state,
                    IsNullable = true
                }
            };
            Func<DataRow, EventDto> dt = CreateEventDto;
            return GetDataFromDb(dt, "SelectEventsByCategoryIdAndDateAndState", parameters);
        }

        public EventDto GetEventById(int eventId)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@id",
                Value = eventId
            };
            Func<DataRow, EventDto> dt = CreateEventDto;
            return GetDataFromDb(dt, "SelectEventById", param).FirstOrDefault();
        }

        public EventDetailDto GetEventInfoDetailById(int eventId)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@eventId",
                Value = eventId
            };
            Func<DataRow, EventDetailDto> dt = CreateEventDetailDto;
            return GetDataFromDb(dt, "SelectEventDetailInfoById", param).FirstOrDefault();
        }

        public List<CategoryDto> GetCategories()
        {
            if (_staticCache["Categories"] == null)
            {
                Func<DataRow, CategoryDto> dt = CreateCategoryDto;
                return GetDataFromDb(dt, "SelectCategories", "Categories", "Categories");
            }
            return (List<CategoryDto>)_staticCache["Categories"];
        }

        public CategoryDto GetCategoryById(int categoryId)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@categoryId",
                Value = categoryId
            };
            Func<DataRow, CategoryDto> dt = CreateCategoryDto;
            return GetDataFromDb(dt, "SelectCategoryById", param).FirstOrDefault();
        }

        public List<AddressDto> GetAddresses()
        {
            if (_staticCache["Addresses"] == null)
            {
                Func<DataRow, AddressDto> dt = CreateAddressDto;
                return GetDataFromDb(dt, "SelectAddresses", "Addresses", "Addresses");
            }
            return (List<AddressDto>)_staticCache["Addresses"];
        }

        public AddressDto GetAddressById(int addressId)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@addressId",
                Value = addressId
            };
            Func<DataRow, AddressDto> dt = CreateAddressDto;
            return GetDataFromDb(dt, "SelectAddressById", param).FirstOrDefault();
        }

        public List<EmailDto> GetEmailsByOrganizerId(int organizerId)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@organizerId",
                Value = organizerId
            };
            Func<DataRow, EmailDto> dt = CreateEmailDto;
            return GetDataFromDb(dt, "SelectEmailsByOrganizerId", param);
        }

        public List<PhoneDto> GetPhonesByOrganizerId(int organizerId)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@organizerId",
                Value = organizerId
            };
            Func<DataRow, PhoneDto> dt = CreatePhoneDto;
            return GetDataFromDb(dt, "SelectPhonesByOrganizerId", param);
        }

        public List<UserDto> GetUsers()
        {
            Func<DataRow, UserDto> dt = CreateUserDto;
            return GetDataFromDb(dt, "SelectUsers");
        }

        public UserDto GetUserByName(string name)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.String,
                ParameterName = "@name",
                Value = name
            };
            Func<DataRow, UserDto> dt = CreateUserDto;
            return GetDataFromDb(dt, "SelectUserByName", param).FirstOrDefault();
        }

        public List<RoleDto> GetRolesByUserId(int id)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@userId",
                Value = id
            };
            Func<DataRow, RoleDto> dt = CreateRoleDto;
            return GetDataFromDb(dt, "SelectRolesByUserId", param);
        }

        public List<RoleDto> GetRolesNotInUser(string userName)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.String,
                ParameterName = "@userName",
                Value = userName
            };
            Func<DataRow, RoleDto> dt = CreateRoleDto;
            return GetDataFromDb(dt, "SelectRolesNotInUser", param);
        }

        public List<RoleDto> GetRoles()
        {
            Func<DataRow, RoleDto> dt = CreateRoleDto;
            return GetDataFromDb(dt, "SelectRoles");
        }

        public RoleDto GetRolesById(int roleId)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@roleId",
                Value = roleId
            };
            Func<DataRow, RoleDto> dt = CreateRoleDto;
            return GetDataFromDb(dt, "SelectRoleById", param).FirstOrDefault();
        }

        public bool IsValidUser(string username, string password)
        {
            SqlParameter[] param = {
                new SqlParameter
                {
                   DbType = DbType.String,
                   ParameterName = "@name",
                   Value = username
                },
                new SqlParameter
                {
                    DbType = DbType.String,
                    ParameterName = "@password",
                    Value = password
                }
            };
            return GetBoolFromDb("IsValidUser", param);
        }

        public bool IsUserNameFreeForUserId(int userId, string name)
        {
            SqlParameter[] param = {
                new SqlParameter
                {
                    DbType = DbType.String,
                    ParameterName = "@userId",
                    Value = userId
                },
                new SqlParameter
                {
                    DbType = DbType.String,
                    ParameterName = "@name",
                    Value = name
                }
            };
            return GetBoolFromDb("IsUserNameFreeForUserId", param);
        }

        public bool IsUserNameFree(string name)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.String,
                ParameterName = "@name",
                Value = name
            };
            return GetBoolFromDb("IsUserNameFree", param);
        }

        public bool IsRoleNameFree(int? roleId, string name)
        {
            SqlParameter[] param = {
                new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "@roleId",
                    Value = roleId,
                    IsNullable = true
                },
                new SqlParameter
                {
                    DbType = DbType.String,
                    ParameterName = "@name",
                    Value = name
                }
            };
            return GetBoolFromDb("IsRoleNameFree", param);
        }

        public void AddEvent(string name, DateTime date, int? organizerId, int? categoryId, string imageUrl, string description,
            int? addressId)
        {
            SqlParameter[] param = {
                new SqlParameter{DbType = DbType.String,ParameterName = "@name",Value = name},
                new SqlParameter{DbType = DbType.DateTime,ParameterName = "@date",Value = date},
                new SqlParameter{DbType = DbType.Int32,ParameterName = "@organizerId",Value = organizerId},
                new SqlParameter{DbType = DbType.Int32,ParameterName = "@categoryId",Value = categoryId},
                new SqlParameter{DbType = DbType.String,ParameterName = "@imageURL",Value = imageUrl},
                new SqlParameter{DbType = DbType.String,ParameterName = "@description",Value = description},
                new SqlParameter{DbType = DbType.Int32,ParameterName = "@addressId",Value = addressId}
            };
            ChangeDbData("AddEvent", param);
        }

        public void DeleteEvent(int eventId)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@eventId",
                Value = eventId
            };

            ChangeDbData("DeleteEventById", param);
        }

        public void DeleteFutureEventByIdAndUserId(int eventId, int userId)
        {
            SqlParameter[] param = {
                new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "@eventId",
                    Value = eventId
                },
                new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "@userId",
                    Value = userId
                }
            };
            ChangeDbData("DeleteFutureEventByIdAndUserId", param);
        }

        public void DeleteUser(int userId)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@userId",
                Value = userId
            };
            ChangeDbData("DeleteUserById", param);
        }

        public void DeleteRole(int roleId)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@roleId",
                Value = roleId
            };

            ChangeDbData("DeleteRoleById", param);
        }

        public void DeleteUserRole(string userName, int roleId)
        {
            SqlParameter[] param = {
                new SqlParameter
                {
                    DbType = DbType.String,
                    ParameterName = "@userName",
                    Value = userName
                },
                new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "@roleId",
                    Value = roleId
                }
            };
            ChangeDbData("DeleteUserRole", param);
        }

        public void DeleteEmailByUserIdAndEmailId(int userId, int emailId)
        {
            SqlParameter[] param = {
                new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "@userId",
                    Value = userId
                },
                new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "@emailId",
                    Value = emailId
                }
            };
            ChangeDbData("DeleteEmailByUserIdAndEmailId", param);
        }

        public void DeletePhoneByUserIdAndPhoneId(int userId, int phoneId)
        {
            SqlParameter[] param = {
                new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "@userId",
                    Value = userId
                },
                new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "@phoneId",
                    Value = phoneId
                }
            };
            ChangeDbData("DeletePhoneByUserIdAndPhoneId", param);
        }

        public void DeleteAddress(int addressId)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@addressId",
                Value = addressId
            };
            ChangeDbData("DeleteAddressById", param);
        }

        public void DeleteCategory(int categoryId)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@categoryId",
                Value = categoryId
            };
            ChangeDbData("DeleteCategoryById", param);
        }

        public void EditEvent(int eventId, string name, DateTime date, int? categoryId, string imageUrl, string description, int? addressId)
        {
            SqlParameter[] param = {
                new SqlParameter{DbType = DbType.Int32,ParameterName = "@eventId",Value = eventId},
                new SqlParameter{DbType = DbType.String,ParameterName = "@name",Value = name},
                new SqlParameter{DbType = DbType.DateTime,ParameterName = "@date",Value = date},
                new SqlParameter{DbType = DbType.Int32,ParameterName = "@categoryId",Value = categoryId},
                new SqlParameter{DbType = DbType.String,ParameterName = "@imageURL",Value = imageUrl},
                new SqlParameter{DbType = DbType.String,ParameterName = "@description",Value = description},
                new SqlParameter{DbType = DbType.Int32,ParameterName = "@addressId",Value = addressId}
            };
            ChangeDbData("UpdateEvent", param);
        }

        public void EditEventByUserId(int eventId, int userId, string name, DateTime date, int? categoryId, string imageUrl,
            string description, int? addressId)
        {
            SqlParameter[] param = {
                new SqlParameter{DbType = DbType.Int32,ParameterName = "@eventId",Value = eventId},
                new SqlParameter{DbType = DbType.Int32,ParameterName = "@userId",Value = userId},
                new SqlParameter{DbType = DbType.String,ParameterName = "@name",Value = name},
                new SqlParameter{DbType = DbType.DateTime,ParameterName = "@date",Value = date},
                new SqlParameter{DbType = DbType.Int32,ParameterName = "@categoryId",Value = categoryId},
                new SqlParameter{DbType = DbType.String,ParameterName = "@imageURL",Value = imageUrl},
                new SqlParameter{DbType = DbType.String,ParameterName = "@description",Value = description},
                new SqlParameter{DbType = DbType.Int32,ParameterName = "@addressId",Value = addressId}
            };
            ChangeDbData("UpdateEventByUserId", param);
        }

        public void AddUser(string name, string password, string email)
        {
            SqlParameter[] param = {
                new SqlParameter{DbType = DbType.String,ParameterName = "@name",Value = name},
                new SqlParameter{DbType = DbType.String,ParameterName = "@password",Value = password},
                new SqlParameter{DbType = DbType.String,ParameterName = "@email",Value = email}
            };
            ChangeDbData("AddUser", param);
        }

        public void AddRoleToUser(string userName, int roleId)
        {
            SqlParameter[] param = {
                new SqlParameter{
                    DbType = DbType.String,
                    ParameterName = "@userName",
                    Value = userName},
                new SqlParameter{
                    DbType = DbType.Int32,
                    ParameterName = "@roleId",
                    Value = roleId}
            };
            ChangeDbData("AddUserRole", param);
        }

        public void AddRole(string roleName)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.String,
                ParameterName = "@name",
                Value = roleName
            };
            ChangeDbData("AddRole", param);
        }

        public void AddAddress(string address)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.String,
                ParameterName = "@address",
                Value = address
            };
            ChangeDbData("AddAddress", param);
        }

        public void AddCategory(string categoryName, int? pid)
        {
            SqlParameter[] param = {
                new SqlParameter
                {
                    DbType = DbType.String,
                    ParameterName = "@categoryName",
                    Value = categoryName
                },
                new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "@pid",
                    Value = pid,
                    IsNullable = true
                }
            };
            ChangeDbData("AddCategory", param);
        }

        public void AddPhone(int userId, string phoneNumber)
        {
            SqlParameter[] param = {
                new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "@userId",
                    Value = userId
                },
                new SqlParameter
                {
                    DbType = DbType.String,
                    ParameterName = "@phoneNumber",
                    Value = phoneNumber
                }
            };
            ChangeDbData("AddPhone", param);
        }

        public void AddEmail(int userId, string email)
        {
            SqlParameter[] param = {
                new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "@userId",
                    Value = userId
                },
                new SqlParameter
                {
                    DbType = DbType.String,
                    ParameterName = "@email",
                    Value = email
                }
            };
            ChangeDbData("AddEmail", param);
        }

        public void EditUserInfo(int userId, string name, string password, string email)
        {
            SqlParameter[] param = {
                new SqlParameter{DbType = DbType.String,ParameterName = "@userId",Value = userId},
                new SqlParameter{DbType = DbType.String,ParameterName = "@name",Value = name, IsNullable = true},
                new SqlParameter{DbType = DbType.String,ParameterName = "@password",Value = password, IsNullable = true},
                new SqlParameter{DbType = DbType.String,ParameterName = "@email",Value = email, IsNullable = true}
            };
            ChangeDbData("UpdateUserInfo", param);
        }

        public void EditOrganizerInfo(int userId, string name)
        {
            SqlParameter[] param = {
                new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "@organizerId",
                    Value = userId
                },
                new SqlParameter{DbType = DbType.String,
                    ParameterName = "@name",
                    Value = name,
                    IsNullable = true}
            };
            ChangeDbData("UpdateOrganizerInfo", param);
        }

        public void EditRole(int roleId, string roleName)
        {
            SqlParameter[] param = {
                new SqlParameter{DbType = DbType.String,ParameterName = "@roleId",Value = roleId},
                new SqlParameter{DbType = DbType.String,ParameterName = "@name",Value = roleName,IsNullable = true}
            };
            ChangeDbData("UpdateRole", param);
        }

        public void EditAddress(int addressId, string address)
        {
            SqlParameter[] param = {
                new SqlParameter{DbType = DbType.String,ParameterName = "@addressId",Value = addressId},
                new SqlParameter{DbType = DbType.String,ParameterName = "@address",Value = address,IsNullable = true}
            };
            ChangeDbData("UpdateAddress", param);
        }

        public void EditCategory(int categoryId, int? pid, string categoryName)
        {
            SqlParameter[] param = {
                new SqlParameter{DbType = DbType.Int32,ParameterName = "@categoryId",Value = categoryId},
                new SqlParameter{DbType = DbType.Int32,ParameterName = "@pid",Value = pid,IsNullable = true},
                new SqlParameter{DbType = DbType.String,ParameterName = "@categoryName",Value = categoryName}
            };
            ChangeDbData("UpdateCategory", param);
        }
    }
}