using EventsListService.Contracts.Models.Dto;
using EventsListService.Contracts.Models.DtoExceptions;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;

namespace EventsListService.Contracts.Contracts
{
    public class EventService : IEventService,IAddService
    {
        private readonly string _connectionString;

        private static readonly ILog Log = LogManager.GetLogger(typeof(EventService));

        public EventService()
        {
            try
            {
                _connectionString = ConfigurationManager.ConnectionStrings["DefDbConnect"].ConnectionString;
            }
            catch (NullReferenceException e)
            {
                Log.Error(e.Message);
            }
        }

        private List<T> GetDataFromDb<T>(string procedureName, params SqlParameter[] sqlParams)
        {
            if (string.IsNullOrEmpty(procedureName))
            {
                throw new FaultException<ServiceFault>(new ServiceFault("Empty procedure name"), new FaultReason("Empty procedure name"));
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
                        throw new FaultException<ServiceFault>(new ServiceFault(ex.Message), new FaultReason(ex.Message));
                    }


                    if (typeof(T) == typeof(EventDto))
                    {
                        List<EventDto> tempList = new List<EventDto>();
                        foreach (DataTable dataSetTable in dataSet.Tables)
                        {
                            foreach (DataRow dataRow in dataSetTable.Rows)
                            {
                                tempList.Add(new EventDto
                                {
                                    Id = (int)dataRow[0],
                                    Name = dataRow[1].ToString(),
                                    Date = (DateTime)dataRow[2],
                                    OrganizerId = (int)dataRow[3],
                                    CategoryId = (int)dataRow[4],
                                    ImageUrl = dataRow[5]?.ToString(),
                                    Description = dataRow[6].ToString(),
                                    AddressId = (int)dataRow[7]
                                });
                            }
                        }
                        return tempList.Cast<T>().ToList();
                    }
                    if (typeof(T) == typeof(EventDetailDto))
                    {
                        List<EventDetailDto> tempList = new List<EventDetailDto>();
                        foreach (DataTable dataSetTable in dataSet.Tables)
                        {
                            foreach (DataRow dataRow in dataSetTable.Rows)
                            {
                                tempList.Add(new EventDetailDto
                                {
                                    EventId = (int)dataRow[0],
                                    EventName = dataRow[1].ToString(),
                                    Date = (DateTime)dataRow[2],
                                    ImageUrl = dataRow[3]?.ToString(),
                                    Description = dataRow[4].ToString(),
                                    Address = dataRow[5].ToString(),
                                    CategoryName = dataRow[6].ToString(),
                                    OrganizerName = dataRow[7].ToString(),
                                    OrganizerId = (int)dataRow[8]
                                });
                            }
                        }
                        return tempList.Cast<T>().ToList();
                    }
                    if (typeof(T) == typeof(CategoryDto))
                    {
                        List<CategoryDto> tempList = new List<CategoryDto>();
                        foreach (DataTable dataSetTable in dataSet.Tables)
                        {
                            foreach (DataRow dataRow in dataSetTable.Rows)
                            {
                                tempList.Add(new CategoryDto
                                {
                                    Id = (int)dataRow[0],
                                    Pid = (dataRow[1]) as int?,
                                    Name = dataRow[2].ToString(),
                                });
                            }
                        }
                        return tempList.Cast<T>().ToList();
                    }
                    if (typeof(T) == typeof(OrganizerDto))
                    {
                        List<OrganizerDto> tempList = new List<OrganizerDto>();
                        foreach (DataTable dataSetTable in dataSet.Tables)
                        {
                            foreach (DataRow dataRow in dataSetTable.Rows)
                            {
                                tempList.Add(new OrganizerDto
                                {
                                    Id = (int)dataRow[0],
                                    Name = dataRow[1].ToString()
                                });
                            }
                        }
                        return tempList.Cast<T>().ToList();
                    }
                    if (typeof(T) == typeof(EmailDto))
                    {
                        List<EmailDto> tempList = new List<EmailDto>();
                        foreach (DataTable dataSetTable in dataSet.Tables)
                        {
                            foreach (DataRow dataRow in dataSetTable.Rows)
                            {
                                tempList.Add(new EmailDto
                                {
                                    Id = (int)dataRow[0],
                                    OrganizerId = (int)dataRow[1],
                                    Email = dataRow[2].ToString()
                                });
                            }
                        }
                        return tempList.Cast<T>().ToList();
                    }
                    if (typeof(T) == typeof(PhoneDto))
                    {
                        List<PhoneDto> tempList = new List<PhoneDto>();
                        foreach (DataTable dataSetTable in dataSet.Tables)
                        {
                            foreach (DataRow dataRow in dataSetTable.Rows)
                            {
                                tempList.Add(new PhoneDto
                                {
                                    Id = (int)dataRow[0],
                                    OrganizerId = (int)dataRow[1],
                                    PhoneNumber = dataRow[2].ToString()
                                });
                            }
                        }
                        return tempList.Cast<T>().ToList();
                    }
                    if (typeof(T) == typeof(AddressDto))
                    {
                        List<AddressDto> tempList = new List<AddressDto>();
                        foreach (DataTable dataSetTable in dataSet.Tables)
                        {
                            foreach (DataRow dataRow in dataSetTable.Rows)
                            {
                                tempList.Add(new AddressDto
                                {
                                    Id = (int)dataRow[0],
                                    Address = dataRow[1].ToString()
                                });
                            }
                        }
                        return tempList.Cast<T>().ToList();
                    }
                    if (typeof(T) == typeof(UserDto))
                    {
                        List<UserDto> tempList = new List<UserDto>();
                        foreach (DataTable dataSetTable in dataSet.Tables)
                        {
                            foreach (DataRow dataRow in dataSetTable.Rows)
                            {
                                tempList.Add(new UserDto
                                {
                                    UserName = dataRow[1].ToString(),
                                    UserRoles = GetRolesByUserId((int)dataRow[0])
                                });
                            }
                        }
                        return tempList.Cast<T>().ToList();
                    }
                    if (typeof(T) == typeof(RoleDto))
                    {
                        List<RoleDto> tempList = new List<RoleDto>();
                        foreach (DataTable dataSetTable in dataSet.Tables)
                        {
                            foreach (DataRow dataRow in dataSetTable.Rows)
                            {
                                tempList.Add(new RoleDto
                                {
                                    RoleName = dataRow[0].ToString()
                                });
                            }
                        }
                        return tempList.Cast<T>().ToList();
                    }
                }
            }

            return new List<T>();
        }

        public List<EventDto> GetEvents()
        {
            return GetDataFromDb<EventDto>("SelectEvents");
        }


        public List<EventDto> GetEventsByCategoryId(int categoryId)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@categoryId",
                Value = categoryId
            };
            return GetDataFromDb<EventDto>("SelectEventsByCategoryId", param);
        }

        public EventDetailDto GetEventInfoDetailById(int eventId)
        {
            SqlParameter param = new SqlParameter { DbType = DbType.Int32, ParameterName = "@eventId", Value = eventId };
            EventDetailDto result = GetDataFromDb<EventDetailDto>("SelectEventDetailInfoById", param).SingleOrDefault();
            if (result != null)
            {
                result.OrganizerEmails = GetEmailsByOrganizerId(result.OrganizerId);
                result.OrganizerPhones = GetPhonesByOrganizerId(result.OrganizerId);
            }
            return result;
        }

        public List<EventDto> GetEventsBySearchData(int? categoryId, DateTime? date, int? state)
        {
            SqlParameter[] parameters = new []
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

            return GetDataFromDb<EventDto>("SelectEventsByCategoryIdAndDateAndState", parameters);
        }

        public List<CategoryDto> GetCategories()
        {
            return GetDataFromDb<CategoryDto>("SelectCategories", null);
        }

        public List<OrganizerDto> GetOrganizers()
        {
            return GetDataFromDb<OrganizerDto>("SelectOrganizers", null);
        }

        public OrganizerDto GetOrganizerById(int id)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@id",
                Value = id
            };
            return GetDataFromDb<OrganizerDto>("SelectOrganizerById", param).SingleOrDefault();
        }

        public List<EmailDto> GetEmailsByOrganizerId(int organizerId)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@organizerId",
                Value = organizerId
            };
            return GetDataFromDb<EmailDto>("SelectEmailsByOrganizerId", param);
        }

        public List<PhoneDto> GetPhonesByOrganizerId(int organizerId)
        {
            SqlParameter param = new SqlParameter { DbType = DbType.Int32, ParameterName = "@organizerId", Value = organizerId };
            return GetDataFromDb<PhoneDto>("SelectPhonesByOrganizerId", param);
        }

        public AddressDto GetAddressById(int id)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@id",
                Value = id
            };
            return GetDataFromDb<AddressDto>("SelectAddressById", param).SingleOrDefault();
        }

        public UserDto GetUserByName(string name)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.String,
                ParameterName = "@name",
                Value = name
            };
            return GetDataFromDb<UserDto>("SelectUserByName", param).SingleOrDefault();
        }

        public List<RoleDto> GetRolesByUserId(int id)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@userId",
                Value = id
            };
            return GetDataFromDb<RoleDto>("SelectRolesByUserId", param);
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

            bool result = false;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "IsValidUser";

                    command.Parameters.AddRange(param);

                    SqlParameter resultParameter = new SqlParameter();
                    resultParameter.Direction = ParameterDirection.ReturnValue;
                    command.Parameters.Add(resultParameter);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();

                    try
                    {
                        adapter.Fill(dataSet);
                    }
                    catch (SqlException ex)
                    {
                        throw new FaultException<ServiceFault>(new ServiceFault(ex.Message), new FaultReason(ex.Message));
                    }

                    if ((int)resultParameter.Value == 1)
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        public void AddEvent(string name, DateTime date, int organizerId, int categoryId, string imageUrl, string description,
            int addressId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "AddEvent";

                    command.Parameters.AddRange(new[]
                    {
                        new SqlParameter{DbType = DbType.String,ParameterName = "@name",Value = name},
                        new SqlParameter{DbType = DbType.DateTime,ParameterName = "@date",Value = date},
                        new SqlParameter{DbType = DbType.Int32,ParameterName = "@organizerId",Value = organizerId},
                        new SqlParameter{DbType = DbType.Int32,ParameterName = "@categoryId",Value = categoryId},
                        new SqlParameter{DbType = DbType.String,ParameterName = "@imageURL",Value = imageUrl},
                        new SqlParameter{DbType = DbType.String,ParameterName = "@description",Value = description},
                        new SqlParameter{DbType = DbType.Int32,ParameterName = "@addressId",Value = addressId}
                    });
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }
            }

        }
    }
}
