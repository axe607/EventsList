using EventsListService.Contracts.Models.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EventsListService.Contracts.Contracts
{
    public class EventService : IEventService
    {


        //new Category {Id = 1, Name = "Автограф-сессия"},
        //new Category {Id = 2, Name = "Благотворительность"},
        //new Category {Id = 3, Name = ""},
        //new Category {Id = 7, Name = "Конкурс"},
        //new Category {Id = 9, Name = "Модный маркет"},
        //new Category {Id = 10, Name = "Награждение"},
        //new Category {Id = 11, Name = "Новогоднее представление"},
        //new Category {Id = 12, Name = "Презентация"},
        //new Category {Id = 16, Name = "Фестиваль"},
        //new Category {Id = 18, Name = "Экскурсия"},
        //new Category {Id = 19, Name = "Ярмарка"}

        public List<EventDto> GetEvents()
        {
            List<EventDto> listResult = new List<EventDto>();
            using (SqlConnection connection = new SqlConnection(@"Data Source=AXE-PC\SQLEXPRESS;Initial Catalog=EventsListDB;User ID=Test;Password=test"))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "SelectEvents";

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);

                    foreach (DataTable dataSetTable in dataSet.Tables)
                    {
                        foreach (DataRow dataRow in dataSetTable.Rows)
                        {
                            listResult.Add(new EventDto { Id = (int)dataRow[0], Name = dataRow[1].ToString(), Date = (DateTime)dataRow[2], OrganizerId = (int)dataRow[3], SubcategoryId = (int)dataRow[4], ImageUrl = dataRow[5]?.ToString(), Description = dataRow[6].ToString(), AddressId = (int)dataRow[7] });
                        }
                    }
                }
            }
            return listResult;
        }

        public List<CategoryDto> GetCategories()
        {
            List<CategoryDto> listResult = new List<CategoryDto>();
            using (SqlConnection connection = new SqlConnection(@"Data Source=AXE-PC\SQLEXPRESS;Initial Catalog=EventsListDB;User ID=Test;Password=test"))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "SelectCategories";

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);

                    foreach (DataTable dataSetTable in dataSet.Tables)
                    {
                        foreach (DataRow dataRow in dataSetTable.Rows)
                        {
                            listResult.Add(new CategoryDto { Id = (int)dataRow[0], Name = dataRow[1].ToString(), Subcategories = GetSubcategoriesByCategoryId((int)dataRow[0]) });
                        }
                    }
                }
            }
            return listResult;
        }

        public List<SubcategoryDto> GetSubcategories()
        {
            List<SubcategoryDto> listResult = new List<SubcategoryDto>();
            using (SqlConnection connection = new SqlConnection(@"Data Source=AXE-PC\SQLEXPRESS;Initial Catalog=EventsListDB;User ID=Test;Password=test"))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "SelectSubcategories";

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);

                    foreach (DataTable dataSetTable in dataSet.Tables)
                    {
                        foreach (DataRow dataRow in dataSetTable.Rows)
                        {
                            listResult.Add(new SubcategoryDto { Id = (int)dataRow[0], CategoryId = (int)dataRow[1], Name = dataRow[2].ToString() });
                        }
                    }
                }
            }
            return listResult;
        }

        public List<SubcategoryDto> GetSubcategoriesByCategoryId(int categoryId)
        {
            List<SubcategoryDto> listResult = new List<SubcategoryDto>();
            using (SqlConnection connection = new SqlConnection(@"Data Source=AXE-PC\SQLEXPRESS;Initial Catalog=EventsListDB;User ID=Test;Password=test"))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "SelectSubcategoriesByCategoryId";

                    SqlParameter param = new SqlParameter { DbType = DbType.Int32, ParameterName = "@categoryId", Value = categoryId };
                    command.Parameters.Add(param);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);

                    foreach (DataTable dataSetTable in dataSet.Tables)
                    {
                        foreach (DataRow dataRow in dataSetTable.Rows)
                        {
                            listResult.Add(new SubcategoryDto { Id = (int)dataRow[0], CategoryId = (int)dataRow[1], Name = dataRow[2].ToString() });
                        }
                    }
                }
            }
            return listResult;
        }

        public List<OrganizerDto> GetOrganizers()
        {
            List<OrganizerDto> listResult = new List<OrganizerDto>();
            using (SqlConnection connection = new SqlConnection(@"Data Source=AXE-PC\SQLEXPRESS;Initial Catalog=EventsListDB;User ID=Test;Password=test"))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "SelectOrganizers";

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);

                    foreach (DataTable dataSetTable in dataSet.Tables)
                    {
                        foreach (DataRow dataRow in dataSetTable.Rows)
                        {
                            listResult.Add(new OrganizerDto { Id = (int)dataRow[0], Name = dataRow[1].ToString(), Emails = GetEmailsByOrganizerId((int)dataRow[0]), Phones = GetPhonesByOrganizerId((int)dataRow[0]) });
                        }
                    }
                }
            }
            return listResult;
        }

        public OrganizerDto GetOrganizerById(int id)
        {
            OrganizerDto result;
            using (SqlConnection connection = new SqlConnection(@"Data Source=AXE-PC\SQLEXPRESS;Initial Catalog=EventsListDB;User ID=Test;Password=test"))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "SelectOrganizerById";

                    SqlParameter param = new SqlParameter { DbType = DbType.Int32, ParameterName = "@id", Value = id };
                    command.Parameters.Add(param);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);

                    result = new OrganizerDto { Id = (int)dataSet.Tables[0].Rows[0][0], Name = dataSet.Tables[0].Rows[0][1].ToString(), Emails = GetEmailsByOrganizerId((int)dataSet.Tables[0].Rows[0][0]), Phones = GetPhonesByOrganizerId((int)dataSet.Tables[0].Rows[0][0]) };

                }
            }
            return result;
        }

        public List<EmailDto> GetEmailsByOrganizerId(int organizerId)
        {
            List<EmailDto> listResult = new List<EmailDto>();
            using (SqlConnection connection = new SqlConnection(@"Data Source=AXE-PC\SQLEXPRESS;Initial Catalog=EventsListDB;User ID=Test;Password=test"))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "SelectEmailsByOrganizerId";

                    SqlParameter param = new SqlParameter { DbType = DbType.Int32, ParameterName = "@organizerId", Value = organizerId };
                    command.Parameters.Add(param);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);

                    foreach (DataTable dataSetTable in dataSet.Tables)
                    {
                        foreach (DataRow dataRow in dataSetTable.Rows)
                        {
                            listResult.Add(new EmailDto { Id = (int)dataRow[0], OrganizerId = (int)dataRow[1], Email = dataRow[2].ToString() });
                        }
                    }
                }
            }
            return listResult;
        }

        public List<PhoneDto> GetPhonesByOrganizerId(int organizerId)
        {
            List<PhoneDto> listResult = new List<PhoneDto>();
            using (SqlConnection connection = new SqlConnection(@"Data Source=AXE-PC\SQLEXPRESS;Initial Catalog=EventsListDB;User ID=Test;Password=test"))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "SelectPhonesByOrganizerId";

                    SqlParameter param = new SqlParameter { DbType = DbType.Int32, ParameterName = "@organizerId", Value = organizerId };
                    command.Parameters.Add(param);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);

                    foreach (DataTable dataSetTable in dataSet.Tables)
                    {
                        foreach (DataRow dataRow in dataSetTable.Rows)
                        {
                            listResult.Add(new PhoneDto { Id = (int)dataRow[0], OrganizerId = (int)dataRow[1], PhoneNumber = dataRow[2].ToString() });
                        }
                    }
                }
            }
            return listResult;
        }

        public AddressDto GetAddressById(int id)
        {
            AddressDto result;
            using (SqlConnection connection = new SqlConnection(@"Data Source=AXE-PC\SQLEXPRESS;Initial Catalog=EventsListDB;User ID=Test;Password=test"))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "SelectAddressById";

                    SqlParameter param = new SqlParameter { DbType = DbType.Int32, ParameterName = "@id", Value = id };
                    command.Parameters.Add(param);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);

                    result = new AddressDto { Id = (int)dataSet.Tables[0].Rows[0][0], Address = dataSet.Tables[0].Rows[0][1].ToString() };

                }
            }
            return result;
        }
    }
}
