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
    public class EventService : IGet, IAdd, IDelete, IUpdate
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
                                    OrganizerId = dataRow[3] as int?,
                                    CategoryId = dataRow[4] as int?,
                                    ImageUrl = dataRow[5]?.ToString(),
                                    Description = dataRow[6].ToString(),
                                    AddressId = dataRow[7] as int?
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
                                    OrganizerName = string.IsNullOrEmpty(dataRow[7].ToString())
                                    ? dataRow[9].ToString()
                                    : dataRow[7].ToString(),
                                    OrganizerEmails = (dataRow[8] as int?) != null ? GetEmailsByOrganizerId((int)dataRow[8]) : null,
                                    OrganizerPhones = (dataRow[8] as int?) != null ? GetPhonesByOrganizerId((int)dataRow[8]) : null
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
                    if (typeof(T) == typeof(UserDto))
                    {
                        List<UserDto> tempList = new List<UserDto>();
                        foreach (DataTable dataSetTable in dataSet.Tables)
                        {
                            foreach (DataRow dataRow in dataSetTable.Rows)
                            {
                                tempList.Add(new UserDto
                                {
                                    Id = (int)dataRow[0],
                                    UserName = dataRow[1].ToString(),
                                    Email = dataRow[2].ToString(),
                                    UserRoles = GetRolesByUserId((int)dataRow[0]),
                                    OrganizerName = dataRow[3].ToString(),
                                    OrganizerEmails = GetEmailsByOrganizerId((int)dataRow[0]),
                                    OrganizerPhones = GetPhonesByOrganizerId((int)dataRow[0])
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
                                    Id = (int)dataRow[0],
                                    RoleName = dataRow[1].ToString()
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

        public List<EventDto> GetEventsByUserId(int userId)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@userId",
                Value = userId
            };
            return GetDataFromDb<EventDto>("SelectEventsByUserId", param);
        }

        public EventDetailDto GetEventInfoDetailById(int eventId)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@eventId",
                Value = eventId
            };

            return GetDataFromDb<EventDetailDto>("SelectEventDetailInfoById", param).SingleOrDefault();
        }

        public EventDto GetEventById(int eventId)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@id",
                Value = eventId
            };

            return GetDataFromDb<EventDto>("SelectEventById", param).SingleOrDefault();
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

            return GetDataFromDb<EventDto>("SelectEventsByCategoryIdAndDateAndState", parameters);
        }

        public List<CategoryDto> GetCategories()
        {
            return GetDataFromDb<CategoryDto>("SelectCategories");
        }

        public CategoryDto GetCategoryById(int categoryId)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@categoryId",
                Value = categoryId
            };
            return GetDataFromDb<CategoryDto>("SelectCategoryById", param).SingleOrDefault();
        }

        public List<AddressDto> GetAddresses()
        {
            return GetDataFromDb<AddressDto>("SelectAddresses");
        }

        public AddressDto GetAddressById(int addressId)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@addressId",
                Value = addressId
            };
            return GetDataFromDb<AddressDto>("SelectAddressById", param).SingleOrDefault();
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

        public List<UserDto> GetUsers()
        {
            return GetDataFromDb<UserDto>("SelectUsers");
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

        public List<RoleDto> GetRolesNotInUser(string userName)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.String,
                ParameterName = "@userName",
                Value = userName
            };
            return GetDataFromDb<RoleDto>("SelectRolesNotInUser", param);
        }

        public List<RoleDto> GetRoles()
        {
            return GetDataFromDb<RoleDto>("SelectRoles");
        }

        public RoleDto GetRolesById(int roleId)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@roleId",
                Value = roleId
            };
            return GetDataFromDb<RoleDto>("SelectRoleById", param).SingleOrDefault();
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

        public bool IsUserNameFree(int userId, string name)
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

            bool result = false;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "IsUserNameFree";

                    command.Parameters.AddRange(param);

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

        public bool IsRoleNameFree(int? roleId, string name)
        {
            bool result = false;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "IsRoleNameFree";

                    command.Parameters.AddRange(new[]{
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
                        });

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

        public void AddEvent(string name, DateTime date, int? organizerId, int? categoryId, string imageUrl, string description,
            int? addressId)
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

        public void DeleteEvent(int eventId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "DeleteEventById";

                    command.Parameters.Add(new SqlParameter
                    {
                        DbType = DbType.Int32,
                        ParameterName = "@eventId",
                        Value = eventId
                    });


                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }
            }
        }

        public void DeleteFutureEventByIdAndUserId(int eventId, int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "DeleteFutureEventByIdAndUserId";

                    command.Parameters.AddRange(new[]
                    {
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
                    });


                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }
            }
        }

        public void DeleteUser(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "DeleteUserById";

                    command.Parameters.Add(new SqlParameter
                    {
                        DbType = DbType.Int32,
                        ParameterName = "@userId",
                        Value = userId
                    });


                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }
            }
        }

        public void DeleteRole(int roleId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "DeleteRoleById";

                    command.Parameters.Add(new SqlParameter
                    {
                        DbType = DbType.Int32,
                        ParameterName = "@roleId",
                        Value = roleId
                    });
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void DeleteUserRole(string userName, int roleId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "DeleteUserRole";

                    command.Parameters.AddRange(new[]
                    {
                        new SqlParameter{DbType = DbType.String,ParameterName = "@userName",Value = userName},
                        new SqlParameter{DbType = DbType.Int32,ParameterName = "@roleId",Value = roleId}
                    });
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }
            }
        }

        public void DeleteEmailByUserIdAndEmailId(int userId, int emailId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "DeleteEmailByUserIdAndEmailId";

                    command.Parameters.Add(new SqlParameter
                    {
                        DbType = DbType.Int32,
                        ParameterName = "@userId",
                        Value = userId
                    });
                    command.Parameters.Add(new SqlParameter
                    {
                        DbType = DbType.Int32,
                        ParameterName = "@emailId",
                        Value = emailId
                    });


                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }
            }
        }

        public void DeletePhoneByUserIdAndPhoneId(int userId, int phoneId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "DeletePhoneByUserIdAndPhoneId";

                    command.Parameters.Add(new SqlParameter
                    {
                        DbType = DbType.Int32,
                        ParameterName = "@userId",
                        Value = userId
                    });
                    command.Parameters.Add(new SqlParameter
                    {
                        DbType = DbType.Int32,
                        ParameterName = "@phoneId",
                        Value = phoneId
                    });


                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }
            }
        }

        public void DeleteAddress(int addressId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "DeleteAddressById";

                    command.Parameters.Add(new SqlParameter
                    {
                        DbType = DbType.Int32,
                        ParameterName = "@addressId",
                        Value = addressId
                    });
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void DeleteCategory(int categoryId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "DeleteCategoryById";

                    command.Parameters.Add(new SqlParameter
                    {
                        DbType = DbType.Int32,
                        ParameterName = "@categoryId",
                        Value = categoryId
                    });
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void EditEvent(int eventId, string name, DateTime date, int? categoryId, string imageUrl, string description, int? addressId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "UpdateEvent";

                    command.Parameters.AddRange(new[]
                    {
                        new SqlParameter{DbType = DbType.Int32,ParameterName = "@eventId",Value = eventId},
                        new SqlParameter{DbType = DbType.String,ParameterName = "@name",Value = name},
                        new SqlParameter{DbType = DbType.DateTime,ParameterName = "@date",Value = date},
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

        public void EditEventByUserId(int eventId, int userId, string name, DateTime date, int? categoryId, string imageUrl,
            string description, int? addressId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "UpdateEventByUserId";

                    command.Parameters.AddRange(new[]
                    {
                        new SqlParameter{DbType = DbType.Int32,ParameterName = "@eventId",Value = eventId},
                        new SqlParameter{DbType = DbType.Int32,ParameterName = "@userId",Value = userId},
                        new SqlParameter{DbType = DbType.String,ParameterName = "@name",Value = name},
                        new SqlParameter{DbType = DbType.DateTime,ParameterName = "@date",Value = date},
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

        public void AddUser(string name, string password, string email)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "AddUser";

                    command.Parameters.AddRange(new[]
                    {
                        new SqlParameter{DbType = DbType.String,ParameterName = "@name",Value = name},
                        new SqlParameter{DbType = DbType.String,ParameterName = "@password",Value = password},
                        new SqlParameter{DbType = DbType.String,ParameterName = "@email",Value = email}
                    });
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }
            }
        }

        public void AddRoleToUser(string userName, int roleId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "AddUserRole";

                    command.Parameters.AddRange(new[]
                    {
                        new SqlParameter{DbType = DbType.String,ParameterName = "@userName",Value = userName},
                        new SqlParameter{DbType = DbType.Int32,ParameterName = "@roleId",Value = roleId}
                    });
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }
            }
        }

        public void AddRole(string roleName)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "AddRole";

                    command.Parameters.Add(new SqlParameter
                    {
                        DbType = DbType.String,
                        ParameterName = "@name",
                        Value = roleName
                    });
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }
            }
        }

        public void AddAddress(string address)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "AddAddress";

                    command.Parameters.Add(new SqlParameter
                    {
                        DbType = DbType.String,
                        ParameterName = "@address",
                        Value = address
                    });
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }
            }
        }

        public void AddCategory(string categoryName, int? pid)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "AddCategory";

                    command.Parameters.AddRange(new[]
                    {
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

                    });
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }
            }
        }

        public void EditUserInfo(int userId, string name, string email)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "UpdateUserInfo";

                    command.Parameters.AddRange(new[]
                    {
                        new SqlParameter{DbType = DbType.String,ParameterName = "@userId",Value = userId},
                        new SqlParameter{DbType = DbType.String,ParameterName = "@name",Value = name},
                        new SqlParameter{DbType = DbType.String,ParameterName = "@email",Value = email}
                    });
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }
            }
        }

        public void EditRole(int roleId, string roleName)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "UpdateRole";

                    command.Parameters.AddRange(new[]
                    {
                        new SqlParameter{DbType = DbType.String,ParameterName = "@roleId",Value = roleId},
                        new SqlParameter{DbType = DbType.String,ParameterName = "@name",Value = roleName,IsNullable = true}
                    });
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }
            }
        }

        public void EditAddress(int addressId, string address)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "UpdateAddress";

                    command.Parameters.AddRange(new[]
                    {
                        new SqlParameter{DbType = DbType.String,ParameterName = "@addressId",Value = addressId},
                        new SqlParameter{DbType = DbType.String,ParameterName = "@address",Value = address,IsNullable = true}
                    });
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }
            }
        }

        public void EditCategory(int categoryId, int? pid, string categoryName)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "UpdateCategory";

                    command.Parameters.AddRange(new[]
                    {
                        new SqlParameter{DbType = DbType.Int32,ParameterName = "@categoryId",Value = categoryId},
                        new SqlParameter{DbType = DbType.Int32,ParameterName = "@pid",Value = pid,IsNullable = true},
                        new SqlParameter{DbType = DbType.String,ParameterName = "@categoryName",Value = categoryName}
                    });
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }
            }
        }
    }
}
