using EventsListService.Contracts.Models.Dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace EventsListService.Contracts.Contracts
{
    public class EventService : IEventService
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefDbConnect"].ConnectionString;

        private List<T> GetDataFromDb<T>(string procedureName, params SqlParameter[] sqlParams)
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

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);

                    if (typeof(List<T>) == typeof(List<EventDto>))
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
                                    SubcategoryId = (int)dataRow[4],
                                    ImageUrl = dataRow[5]?.ToString(),
                                    Description = dataRow[6].ToString(),
                                    AddressId = (int)dataRow[7]
                                });
                            }
                        }
                        return tempList.Cast<T>().ToList();
                    }
                    if (typeof(List<T>) == typeof(List<EventDetailDto>))
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
                                    SubcategoryName = dataRow[7].ToString(),
                                    OrganizerName = dataRow[8].ToString(),
                                    OrganizerId = (int)dataRow[9]
                                });
                            }
                        }
                        return tempList.Cast<T>().ToList();
                    }
                    if (typeof(List<T>) == typeof(List<CategoryDto>))
                    {
                        List<CategoryDto> tempList = new List<CategoryDto>();
                        foreach (DataTable dataSetTable in dataSet.Tables)
                        {
                            foreach (DataRow dataRow in dataSetTable.Rows)
                            {
                                tempList.Add(new CategoryDto
                                {
                                    Id = (int)dataRow[0],
                                    Name = dataRow[1].ToString(),
                                    Subcategories = GetSubcategoriesByCategoryId((int)dataRow[0])
                                });
                            }
                        }
                        return tempList.Cast<T>().ToList();
                    }
                    if (typeof(List<T>) == typeof(List<SubcategoryDto>))
                    {
                        List<SubcategoryDto> tempList = new List<SubcategoryDto>();
                        foreach (DataTable dataSetTable in dataSet.Tables)
                        {
                            foreach (DataRow dataRow in dataSetTable.Rows)
                            {
                                tempList.Add(new SubcategoryDto
                                {
                                    Id = (int)dataRow[0],
                                    CategoryId = (int)dataRow[1],
                                    Name = dataRow[2].ToString()
                                });
                            }
                        }
                        return tempList.Cast<T>().ToList();
                    }
                    if (typeof(List<T>) == typeof(List<OrganizerDto>))
                    {
                        List<OrganizerDto> tempList = new List<OrganizerDto>();
                        foreach (DataTable dataSetTable in dataSet.Tables)
                        {
                            foreach (DataRow dataRow in dataSetTable.Rows)
                            {
                                tempList.Add(new OrganizerDto
                                {
                                    Id = (int)dataRow[0],
                                    Name = dataRow[1].ToString(),
                                    Emails = GetEmailsByOrganizerId((int)dataRow[0]),
                                    Phones = GetPhonesByOrganizerId((int)dataRow[0])
                                });
                            }
                        }
                        return tempList.Cast<T>().ToList();
                    }
                    if (typeof(List<T>) == typeof(List<EmailDto>))
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
                    if (typeof(List<T>) == typeof(List<PhoneDto>))
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
                    if (typeof(List<T>) == typeof(List<AddressDto>))
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
                }
            }

            return new List<T>();
        }

        public List<EventDto> GetEvents()
        {
            return GetDataFromDb<EventDto>("SelectEvents");
        }

        public List<EventDto> GetEventsBySubcategoryId(int subcategoryId)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@subcategoryId",
                Value = subcategoryId
            };
            return GetDataFromDb<EventDto>("SelectEventsBySubcategoryId", param);

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

        public List<CategoryDto> GetCategories()
        {
            return GetDataFromDb<CategoryDto>("SelectCategories", null);
        }

        public List<SubcategoryDto> GetSubcategories()
        {
            return GetDataFromDb<SubcategoryDto>("SelectSubcategories", null);
        }

        public List<SubcategoryDto> GetSubcategoriesByCategoryId(int categoryId)
        {
            SqlParameter param = new SqlParameter
            {
                DbType = DbType.Int32,
                ParameterName = "@categoryId",
                Value = categoryId
            };
            return GetDataFromDb<SubcategoryDto>("SelectSubcategoriesByCategoryId", param);
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


    }
}
